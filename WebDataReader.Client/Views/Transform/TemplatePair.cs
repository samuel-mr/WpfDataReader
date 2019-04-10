using Prism.Mvvm;

namespace WebDataReader.Client.Views.Transform
{
  public class TemplatePair : BindableBase
  {
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