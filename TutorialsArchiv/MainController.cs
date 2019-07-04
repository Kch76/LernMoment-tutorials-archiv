using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialsArchiv
{
    class MainController
    {
        enum EditingMode
        {
            EmptyDatabase,
            NothingSelected,
            UserSelectedExistingResource,
            UserEditsExistingResource,
            UserEditsNewResource,
            UserEditsFirstNewResource
        }

        private readonly FileDatabase _db = null;
        private readonly List<TeachingResource> _allResources = null;
        private EditingMode _mode = EditingMode.NothingSelected;
        private TeachingResource _activeResource = null;
        private readonly List<string> _invalidResourceProperties = new List<string>();

        private readonly MainForm _view = null;

        public MainController(MainForm view)
        {
            _view = view;
            _allResources = new List<TeachingResource>();
            _db = new FileDatabase("file-database.csv");

            _allResources.AddRange(_db.LoadAllEntries());

            _view.SetupUI();

            if (_allResources.Count == 0)
            {
                _mode = EditingMode.EmptyDatabase;
                _view.EnterNoResourcesMode();
            }
            else
            {
                EnterInitMode();
                _view.UpdateResourceCollectionView(_allResources);
            }

            _view.ResourceEdited += new EventHandler(ResourceGetsEdited);
            _view.ResourceEditCompleted += new EventHandler(ResourceGetsUpdated);
            _view.ResourceCreationRequested += new EventHandler(ResourceGetsCreated);
            _view.ResourceDeletionRequested += new EventHandler(ResourceGetsDeleted);
            _view.ResourceSelected += new MainForm.TeachingResourceHandler(ResourceGetsSelected);
            _view.Canceled += new EventHandler(CurrentActivityCanceled);
            _view.ValidationStateChanged += new MainForm.ValidationChangedHandler(ResourceValidationChanged);
            _view.FormCloseRequested += new MainForm.CloseFormHandler(FormGetsClosed);
        }

        private void ResourceGetsEdited(object sender, EventArgs args)
        {
            // editing a new Resource is different than editing an existing Resource
            if (IsUserEditing())
            {
                // editing allowed without further intervention
                return;
            }
            else if (_mode == EditingMode.UserSelectedExistingResource)
            {
                if (_activeResource == null)
                {
                    _view.ShowMessageToUser("Es wurde noch kein Eintrag zum Ändern ausgewählt!");
                    EnterInitMode();
                }
                else
                {
                    _mode = EditingMode.UserEditsExistingResource;
                    _view.EnterEditExistingMode();
                }
            }
            else
            {
                throw new InvalidOperationException($"UI ist im {_mode} Modus in dem Editieren nicht erlaubt ist!");
            }
        }

        private void ResourceGetsUpdated(object sender, EventArgs args)
        {
            if (_mode == EditingMode.UserEditsNewResource || _mode == EditingMode.UserEditsExistingResource)
            {
                _db.Save(_allResources);

                _view.UpdateResourceCollectionView(_allResources);
                EnterInitMode();
            }
            else if (_mode == EditingMode.UserEditsFirstNewResource)
            {
                _db.Save(_allResources);

                _view.LeaveNoResourcesMode();
                _view.UpdateResourceCollectionView(_allResources);
                EnterInitMode();
            }
            else
            {
                throw new InvalidOperationException($"UI ist im {_mode} Modus und somit können Änderungen nicht übernommen werden!");
            }

        }

        private void ResourceGetsCreated(object sender, EventArgs args)
        {
            if (_mode == EditingMode.UserEditsExistingResource)
            {
                _view.ShowMessageToUser("Bitte erst die Änderungen speichern oder verwerfen!");
                return;
            }

            TeachingResource newResource = new TeachingResource("Neue Ressource", "Bitte ausfüllen", MediumType.Buch, TargetAudience.beginner);
            _activeResource = newResource;
            _allResources.Add(newResource);

            if (_mode == EditingMode.EmptyDatabase)
            {
                _mode = EditingMode.UserEditsFirstNewResource;
            }
            else
            {
                _mode = EditingMode.UserEditsNewResource;
                _view.UpdateResourceCollectionView(_allResources);
                _view.HighlightLatestResource();
            }

            _view.EnterEditNewMode(newResource);
        }

        private void ResourceGetsDeleted(object sender, EventArgs args)
        {
            if (_mode == EditingMode.UserSelectedExistingResource || _mode == EditingMode.UserEditsExistingResource)
            {
                _allResources.Remove(_activeResource);
                _db.Save(_allResources);

                if (_allResources.Count == 0)
                {
                    _mode = EditingMode.EmptyDatabase;
                    _view.EnterNoResourcesMode();
                }
                else
                {
                    _view.UpdateResourceCollectionView(_allResources);
                    EnterInitMode();
                }
            }
            else
            {
                throw new InvalidOperationException($"UI ist im {_mode} Modus! Darin kann nicht gelöscht werden!");
            }
        }

        private void ResourceGetsSelected(TeachingResource selectedResource)
        {
            if (selectedResource==null)
            {
                return;
            }

            if (_mode == EditingMode.NothingSelected || _mode == EditingMode.UserSelectedExistingResource)
            {
                _mode = EditingMode.UserSelectedExistingResource;
                _activeResource = selectedResource;
                _view.EnterSelectExistingMode(selectedResource);
            }
            else if (_mode == EditingMode.UserEditsNewResource)
            {
                // resource gets programmatically selected, but we ignore it while creation!
                return;
            }
            else
            {
                throw new InvalidOperationException($"UI ist im {_mode} Modus! Darin kann nicht selektiert werden!");
            }
        }

        private void ResourceValidationChanged(object sender, ValidationChangedEventArgs args)
        {
            if (args.IsValid)
            {
                _invalidResourceProperties.Remove(args.PropertyName);
            }
            else
            {
                _invalidResourceProperties.Add(args.PropertyName);
            }

            if (IsUserEditing())
            {
                if (_invalidResourceProperties.Count == 0)
                {
                    _view.LeaveValidationFailedMode();
                }
                else
                {
                    _view.EnterValidationFailedMode();
                }
            }
        }

        private void CurrentActivityCanceled(object sender, EventArgs args)
        {
            if (_mode == EditingMode.UserEditsNewResource)
            {
                // TODO: JS, should we ask user whether he is sure to delete the input?
                _allResources.RemoveAt(_allResources.Count - 1);
                _view.UpdateResourceCollectionView(_allResources);

                EnterInitMode();
            }
            else if (_mode == EditingMode.UserEditsFirstNewResource)
            {
                // TODO: JS, should we ask user whether he is sure to delete the input?
                _allResources.RemoveAt(_allResources.Count - 1);

                _mode = EditingMode.EmptyDatabase;
                _view.EnterNoResourcesMode();
            }
            else if (_mode == EditingMode.UserEditsExistingResource)
            {
                EnterInitMode();
            }
            else
            {
                throw new InvalidOperationException($"UI ist im {_mode} Modus. Darin kann nicht abgebrochen werden!");
            }

        }

        private void FormGetsClosed(object sender, CloseRequestedEventArgs args)
        {
            if (IsUserEditing())
            {
                if (_view.ShowOkCancelMessageToUser("Die Änderungen am aktuellen Datensatz gehen verloren."))
                {
                    args.ForceClose = true;
                }
                else
                {
                    args.ForceClose = false;
                }
            }
            else
            {
                args.ForceClose = true;
            }
        }

        private void EnterInitMode()
        {
            _mode = EditingMode.NothingSelected;
            _view.EnterInitMode();
        }

        private bool IsUserEditing()
        {
            return _mode == EditingMode.UserEditsNewResource
                            || _mode == EditingMode.UserEditsExistingResource
                            || _mode == EditingMode.UserEditsFirstNewResource;
        }


    }
}
