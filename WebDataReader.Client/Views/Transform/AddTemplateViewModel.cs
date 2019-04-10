using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace WebDataReader.Client.Views.Transform
{
  public class AddTemplateViewModel : BindableBase
  {
    private string name;
    public string Name
    {
      get => name;
      set
      {
        if (string.IsNullOrWhiteSpace(value))
          throw new Exception("Nombre debe contener un valor");
        if (name == value) return;
        name = value;
        RaisePropertyChanged(nameof(Name));
      }
    }

    private string content;
    public string Content
    {
      get => content;
      set
      {
        if (string.IsNullOrWhiteSpace(value))
          throw new Exception("La plantilla debe tener un contenido");
        if (content == value) return;
        content = value;
        RaisePropertyChanged(nameof(Content));
      }
    }

    public ICommand AcceptCommand => new DelegateCommand(async () => await Accept());

    private async Task Accept()
    {
      
    }
  }
}