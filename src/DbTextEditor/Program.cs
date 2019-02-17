using System;
using System.Windows.Forms;
using DbTextEditor.Configuration;
using DbTextEditor.Forms;
using DbTextEditor.Shared.DependencyInjection;

namespace DbTextEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CompositionRoot.Wire(new ApplicationModule());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
