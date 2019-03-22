using System;
using System.Windows;
using NLog;
using System.Globalization;
using System.Threading;

namespace WebDataReader.Client
{
  /// <summary>
  /// Lógica de interacción para App.xaml
  /// </summary>
  public partial class App : System.Windows.Application
  {
    public static Logger Log = LogManager.GetCurrentClassLogger();

    public App()
    {
      DispatcherUnhandledException += App_DispatcherUnhandledException;
    }

    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);
      System.AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

      Bootstraper.Start();
      var main = Bootstraper.Resolve<MainWindow>();
      main.Show();
    }


    bool _isErrorCatchedFromMe;
    void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
      _isErrorCatchedFromMe = true;
      MessageBox.Show(e.Exception?.Message, "ERROR SUPER GRAVE", MessageBoxButton.OK, MessageBoxImage.Error);
      ProcessError(e.Exception);
      e.Handled = true;
    }

    void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      if (_isErrorCatchedFromMe) return;
      MessageBox.Show("Ocurrio un error no controlado dentro del dominio del proceso, el aplicativo se cerrará. \nPorfavor repórtelo al área de sistemas", "ERROR  MUY MUY GRAVE", MessageBoxButton.OK, MessageBoxImage.Error);
      var exception = e.ExceptionObject as Exception;
      ProcessError(exception);
      if (e.IsTerminating)
      {

      }
    }

    public void ProcessError(Exception exception)
    {
      Log.Error(exception);

      var error = "LOGGED: Exception = " + exception.Message;
      while (exception.InnerException != null)
      {
        exception = exception.InnerException;
        error += " : Inner Exception = " + exception.Message;
      }
      MessageBox.Show(error);
    }
  }
}
