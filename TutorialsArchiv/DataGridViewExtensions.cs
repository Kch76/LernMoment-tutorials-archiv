using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TutorialsArchiv.UiExtensions
{
    public static class DataGridViewExtensions
    {
        public static void Disable(this DataGridView dgv)
        {
            dgv.Enabled = false;
            dgv.DefaultCellStyle.BackColor = SystemColors.Control;
            dgv.DefaultCellStyle.ForeColor = SystemColors.GrayText;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.GrayText;
            dgv.CurrentCell = null;
            dgv.ReadOnly = true;
            dgv.EnableHeadersVisualStyles = false;
        }

        public static void Enable(this DataGridView dgv)
        {
            dgv.Enabled = true;
            dgv.DefaultCellStyle.BackColor = SystemColors.Window;
            dgv.DefaultCellStyle.ForeColor = SystemColors.ControlText;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Window;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
            dgv.ReadOnly = false;
            dgv.EnableHeadersVisualStyles = true;
        }

    }
}
