using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using WebDataReader.Application.Interfaces;
using WebDataReader.Application.Transform.GetTransformed;

namespace WebDataReader.Client.Views.Transform
{
  public class TransformViewModel : BindableBase
  {
    private readonly IImplementedConsumMetadata _implementedConsumMetadata;
    
      /*
       Template = "public {{Type}} {{Name}} { get; set; }";
        ConnectionString =
        @"Data Source=DESKTOP-901LI9F\SQL2017DEV;Initial Catalog=DemoCopia;User ID=sa;Password=Pa$$123";
       Query = @"SELECT TOP (1000) [UsuarioId]
       ,[Nombre]
       ,[Password]
   FROM [DemoCopia].[dbo].[Usuario] where Nombre='f'";
   */
    

    public TransformViewModel(IImplementedConsumMetadata implementedConsumMetadata) 
    {
      Templates = new ObservableCollection<TemplatePair>();
      Connections = new ObservableCollection<TemplatePair>();
      _implementedConsumMetadata = implementedConsumMetadata;
      LoadCache();
    }

    public ICommand AddTemplateCommand => new DelegateCommand(async () => await AddTemplate());
    public ICommand AddConnectionCommand => new DelegateCommand(async () => await AddConnection());
    public ICommand DeleteConnectionCommand => new DelegateCommand(DeleteConnection);
    public ICommand DeleteTemplateCommand => new DelegateCommand(() =>
    {
      if (TemplateSelected == null) return;
      Templates.Remove(TemplateSelected);
    });

    private void DeleteConnection()
    {
      if (ConnectionSelected == null) return;
      Connections.Remove(ConnectionSelected);
    }

    private async Task AddConnection()
    {
      var view = new AddConnectionView()
      {
        DataContext = new AddConnectionViewModel()
      };
      var result = await DialogHost.Show(view,
        "RootDialog",
        (sender, eventargs) => { },
        (sender, eventargs) =>
        {
          if (!Equals(eventargs.Parameter, true)) return;

          if (eventargs.Session?.Content is AddConnectionView connectionView)
          {
            if (connectionView.DataContext is AddConnectionViewModel vm)
            {
              if (string.IsNullOrWhiteSpace(vm.Name) || string.IsNullOrWhiteSpace(vm.Content))
                return;
              Connections.Add(new TemplatePair()
              {
                Guid = Guid.NewGuid(),
                Nombre = vm.Name,
                Plantilla = vm.Content
              });
            }
          }
        });
    }

    async Task AddTemplate()
    {
      var view = new AddTemplateView()
      {
        DataContext = new AddTemplateViewModel()
      };
      var result = await DialogHost.Show(view,
        "RootDialog",
        (sender, eventargs) => { },
        (sender, eventargs) =>
          {
            if (!Equals(eventargs.Parameter, true)) return;

            if (eventargs.Session?.Content is AddTemplateView templateView)
            {
              if (templateView.DataContext is AddTemplateViewModel vm)
              {
                if (string.IsNullOrWhiteSpace(vm.Name) || string.IsNullOrWhiteSpace(vm.Content))
                  return;

                Templates.Add(new TemplatePair()
                {
                  Guid = Guid.NewGuid(),
                  Nombre = vm.Name,
                  Plantilla = vm.Content
                });
              }
            }
          });
    }
    /*
    private void ClosingEventHandler(object sender, DialogClosingEventArgs eventargs)
    {
      if (!Equals(eventargs.Parameter, true)) return;

      if (eventargs.Session?.Content is AddTemplateView view)
      {
        if (view.DataContext is AddTemplateViewModel vm)
        {
          if (string.IsNullOrWhiteSpace(vm.Name) || string.IsNullOrWhiteSpace(vm.Content))
            return;

          Templates.Add(new TemplatePair()
          {
            Nombre = vm.Name,
            Plantilla = vm.Content
          });
        }
      }
    }*/



    private ObservableCollection<TemplatePair> connections;
    public ObservableCollection<TemplatePair> Connections
    {
      get => connections;
      set
      {
        if (connections == value) return;
        connections = value;
        RaisePropertyChanged(nameof(Connections));
      }
    }
    private TemplatePair connectionSelected;
    public TemplatePair ConnectionSelected
    {
      get => connectionSelected;
      set
      {
        if (connectionSelected == value) return;
        connectionSelected = value;
        RaisePropertyChanged(nameof(ConnectionSelected));
        TransformSunatCommand.RaiseCanExecuteChanged();
      }
    }


    private ObservableCollection<TemplatePair> templates;
    public ObservableCollection<TemplatePair> Templates
    {
      get => templates;
      set
      {
        if (templates == value) return;
        templates = value;
        RaisePropertyChanged(nameof(Templates));
      }
    }
    private TemplatePair templateSelected;
    public TemplatePair TemplateSelected
    {
      get => templateSelected;
      set
      {
        if (templateSelected == value) return;
        templateSelected = value;
        RaisePropertyChanged(nameof(TemplateSelected));
        TransformSunatCommand.RaiseCanExecuteChanged();
      }
    }

    private string query;
    public string Query
    {
      get => query;
      set
      {
        if (query == value) return;
        query = value;
        RaisePropertyChanged(nameof(Query));
      }
    }

    private string result;
    public string Result
    {
      get => result;
      set
      {
        if (result == value) return;
        result = value;
        RaisePropertyChanged(nameof(Result));
      }
    }

    private DelegateCommand _TransformSunatCommand;
    public DelegateCommand TransformSunatCommand
    {
      get
      {
        if (_TransformSunatCommand == null)
        {
          _TransformSunatCommand = new DelegateCommand(
            () =>
            {
              if (TemplateSelected == null) return;

              var result = new GetTransformedHandler(_implementedConsumMetadata).Handle(new GetTransformedParams()
              {
                ConnectionString = ConnectionSelected.Plantilla,
                Template = TemplateSelected.Plantilla,
                Tsql = Query
              });
              Result = result.Transformed;
            },
            () => TemplateSelected != null && ConnectionSelected != null);
        }
        return
          _TransformSunatCommand;
      }
    }
    private DelegateCommand _CopyCommand;
    public DelegateCommand CopyCommand
    {
      get
      {
        if (_CopyCommand == null)
        {
          _CopyCommand = new DelegateCommand(
            () =>
            {
              try
              {
                if (string.IsNullOrWhiteSpace(Result))
                  return;
                Clipboard.SetText(Result);
              }
              catch (Exception e)
              {
                MessageBox.Show(e.Message);
              }
            },
            () => true);
        }
        return
          _CopyCommand;
      }
    }

    public void OnClosing()
    {
      SaveCache();
    }

    void SaveCache()
    {
      App.Log.Trace($"[Cerrando] Guardando configuracion");
      var serializado = JsonConvert.SerializeObject(new TransformModelToSerialize()
      {
        Query = Query,
        Plantillas = Templates,
        Conexiones = Connections,
        ConnectionSelected = ConnectionSelected?.Guid,
        TemplateSelected = TemplateSelected?.Guid,
      }, Newtonsoft.Json.Formatting.Indented);
      var newFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create), Constantes.AppFolder);
      var newPath = Path.Combine(newFolder, Constantes.ConfigurationFileName);
      if (Directory.Exists(newFolder) == false)
        Directory.CreateDirectory(newFolder);
      File.WriteAllText(newPath, serializado);
    }

    void LoadCache()
    {
      App.Log.Trace($"[Loading] cargando configuracion");
      var newPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create), Constantes.AppFolder, Constantes.ConfigurationFileName);
      if (File.Exists(newPath) == false)
        return;

      var serialziedText = File.ReadAllText(newPath);
      TransformModelToSerialize serializedObject = null;
      try
      {
        serializedObject = JsonConvert.DeserializeObject<TransformModelToSerialize>(serialziedText);
      }
      catch (Exception e)
      {
        App.Log.Error(e);
      }

      if (serializedObject != null)
      {
        Query = serializedObject.Query;
        Templates = new ObservableCollection<TemplatePair>(serializedObject.Plantillas);
        Connections = new ObservableCollection<TemplatePair>(serializedObject.Conexiones);
       
        if (serializedObject.TemplateSelected.HasValue)
          TemplateSelected = Templates.FirstOrDefault(x => x.Guid == serializedObject.TemplateSelected.Value);

        if (serializedObject.ConnectionSelected.HasValue)
          ConnectionSelected = Connections.FirstOrDefault(x => x.Guid == serializedObject.ConnectionSelected.Value);
      }
    }
  }
}