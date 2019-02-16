using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DbTextEditor.Views;
using WeifenLuo.WinFormsUI.Docking;

namespace DbTextEditor.Forms
{
    public partial class FileTreeForm : DockContent
    {
        public FileTreeForm(MainView mainView)
        {
            InitializeComponent();
        }
    }
}
