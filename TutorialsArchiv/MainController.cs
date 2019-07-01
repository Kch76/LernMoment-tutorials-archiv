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
            Init = 0,
            UserSelectedExistingResource,
            UserEditsExistingResource,
            UserEditsNewResource
        }

        private readonly FileDatabase _db = null;
        private readonly List<TeachingResource> _allResources = null;
        private EditingMode _mode = EditingMode.Init;
        private TeachingResource _activeResource = null;

        private readonly MainForm _view = null;

        public MainController(MainForm view)
        {
            _view = view;
            _allResources = new List<TeachingResource>();
            _db = new FileDatabase("file-database.csv");

            _allResources.AddRange(_db.LoadAllEntries());

            EnterInitMode();
            _view.UpdateResourceCollectionView(_allResources);

            _view.ResourceEdited += new EventHandler(ResourceGetsEdited);
            _view.ResourceEditCompleted += new EventHandler(ResourceGetsUpdated);
            _view.ResourceCreationRequested += new EventHandler(ResourceGetsCreated);
            _view.ResourceDeletionRequested += new EventHandler(ResourceGetsDeleted);
            _view.ResourceSelected += new MainForm.TeachingResourceHandler(ResourceGetsSelected);
            _view.Canceled += new EventHandler(CurrentActivityCanceled);
        }

        private void ResourceGetsEdited(object sender, EventArgs args)
        {
            // editing a new Resource is different than editing an existing Resource
            if (_mode == EditingMode.UserEditsNewResource || _mode == EditingMode.UserEditsExistingResource)
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
                throw new InvalidOperationException($"UI ist im {_mode} Modus im dem editieren nicht erlaubt ist!");
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
            else
            {
                throw new InvalidOperationException("UI ist nicht im Edit-Modus und somit kann dieser auch nicht angebrochen werden!");
            }

        }

        private void ResourceGetsCreated(object sender, EventArgs args)
        {
            if (_mode == EditingMode.UserEditsExistingResource)
            {
                _view.ShowMessageToUser("Bitte erst die Änderungen speichern oder verwerfen!");
            }
            else
            {
                _mode = EditingMode.UserEditsNewResource;

                TeachingResource newResource = new TeachingResource("Neue Ressource", "bitte ausfüllen");
                _activeResource = newResource;
                _allResources.Add(newResource);

                _view.UpdateResourceCollectionView(_allResources);
                _view.HighlightLatestResource();

                _view.EnterEditNewMode(newResource);
            }
        }

        private void ResourceGetsDeleted(object sender, EventArgs args)
        {
            if (_mode == EditingMode.UserSelectedExistingResource || _mode == EditingMode.UserEditsExistingResource)
            {
                _allResources.Remove(_activeResource);
                _db.Save(_allResources);
                _view.UpdateResourceCollectionView(_allResources);

                EnterInitMode();
            }
            else
            {
                throw new InvalidOperationException("UI ist nicht im Resource-Ausgewählt-Modus! Nur darin kann gelöscht werden!");
            }
        }

        private void ResourceGetsSelected(TeachingResource selectedResource)
        {
            if (_mode == EditingMode.Init || _mode == EditingMode.UserSelectedExistingResource)
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
                throw new InvalidOperationException("UI ist nicht im Init-Modus! Nur darin kann selektiert werden!");
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
            else if (_mode == EditingMode.UserEditsExistingResource)
            {
                EnterInitMode();
            }
            else
            {
                throw new InvalidOperationException("UI ist nicht im Edit-Modus und somit kann dieser auch nicht angebrochen werden!");
            }

        }

        private void EnterInitMode()
        {
            _mode = EditingMode.Init;
            _view.EnterInitMode();
        }

    }
}
