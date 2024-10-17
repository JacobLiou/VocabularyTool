using VocabularyTool.Dialog;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

namespace VocabularyTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            try
            {
                string name = Assembly.GetExecutingAssembly()!.GetName()!.Name!.ToString();
                Process[] pro = Process.GetProcesses();
                var processes = pro.Where(t => t.ProcessName == name);
                int n = processes.Count();
                if (n > 1)
                {
                    var first = processes.OrderBy(t => t.StartTime).FirstOrDefault();
                    App.Current.Shutdown();
                    return;
                }

                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                TaskScheduler.UnobservedTaskException += UnobservedTaskException;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            ExceptionDialog.HandleException(e.Exception);
            e.Handled = true;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            ExceptionDialog.HandleException(exception!);
        }

        private void UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            var exception = e.Exception;
            ExceptionDialog.HandleException(exception!);
        }
    }

}
