using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using WebDataReader.Application.Interfaces;
using WebDataReader.Application.Sunat;
using WebDataReader.Application.Sunat.GetTipoCambio;
using WebDataReader.Client.Views.Sunat;
using WebDataReader.Infraestructure;

namespace WebDataReader.Client
{
  public static class Bootstraper
  {
    private static ILifetimeScope _rootScope;

    public static void Start()
    {
      if (_rootScope != null)
        return;

      var builder = new ContainerBuilder();
      builder.RegisterType<MainWindow>().AsSelf();
      builder.RegisterType<MainWindowViewModel>().AsSelf();
      builder.RegisterType<SunatReportViewModel>().AsSelf();
      builder.RegisterType<SunatWorkerViewModel>().AsSelf();

      builder.RegisterType<WebHtmlReader>().As<IHtmlReader>();

      // Commands
      builder.RegisterType<GetTipoCambioHandler>().AsSelf();

      _rootScope = builder.Build();
    }

    public static T Resolve<T>()
    {
      if (_rootScope == null)
        throw new Exception("Bootstraper no ha sido inicializado");
      return _rootScope.Resolve<T>();
    }

    public static T Resolve<T>(Parameter[] parameters)
    {
      if (_rootScope == null)
        throw new Exception("Bootstraper no ha sido inicializado");
      return _rootScope.Resolve<T>(parameters);
    }
  }
}
