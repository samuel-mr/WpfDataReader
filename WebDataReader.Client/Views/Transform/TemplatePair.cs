using System;
using System.Collections.ObjectModel;
using System.ServiceModel.PeerResolvers;
using Prism.Mvvm;

namespace WebDataReader.Client.Views.Transform
{
  public class TransformModelToSerialize
  {
    public ObservableCollection<TemplatePair> Plantillas { get; set; }
    public ObservableCollection<TemplatePair> Conexiones { get; set; }
    public Guid? ConnectionSelected { get; set; }
    public Guid? TemplateSelected { get; set; }
    public string Query { get; set; }
  }
  public class TemplatePair : BindableBase
  {
    public Guid Guid { get; set; }

    private string nombre;
    public string Nombre
    {
      get => nombre;
      set
      {
        if (nombre == value) return;
        nombre = value;
        RaisePropertyChanged(nameof(Nombre));
      }
    }
    private string plantilla;
    public string Plantilla
    {
      get => plantilla;
      set
      {
        if (plantilla == value) return;
        plantilla = value;
        RaisePropertyChanged(nameof(Plantilla));
      }
    }
  }
}