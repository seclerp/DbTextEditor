using System.Windows.Forms;
using DbTextEditor.Views;

namespace DbTextEditor.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            var mainView = new MainView(this);
        }
    }
}
