using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DbTextEditor.Configuration;
using DbTextEditor.Forms;
using DbTextEditor.Shared.DependencyInjection;

namespace DbTextEditor
{
    static class Program
    {
        [DllImport("Shcore.dll")]
        // ReSharper disable once InconsistentNaming
        static extern int SetProcessDpiAwareness(int PROCESS_DPI_AWARENESS);

        // According to https://msdn.microsoft.com/en-us/library/windows/desktop/dn280512(v=vs.85).aspx
        private enum DpiAwareness
        {
            None = 0,
            SystemAware = 1,
            PerMonitorAware = 2
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CompositionRoot.Wire(new ApplicationModule());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // This is needed for correct application look on HighDPI environments or with upscale enabled
            SetProcessDpiAwareness((int) DpiAwareness.PerMonitorAware);

            Application.Run(new MainForm());
        }
    }
}
