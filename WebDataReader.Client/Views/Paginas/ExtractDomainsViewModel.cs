using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using WebDataReader.Application.Sunat.GetTipoCambio;
using WebDataReader.Application.Transform.GetDomains;

namespace WebDataReader.Client.Views.Paginas
{
  public class ExtractDomainsViewModel : BindableBase
  {
    public ExtractDomainsViewModel()
    {
      LoadCommand = new DelegateCommand(async () => { await Load(); });
    }

    private async Task Load()
    {
      var application = Bootstraper.Resolve<GetDomainsHandler>();
      var result = await application.Handle(new GetDomainsParams()
      {
        Url = this.Url
      });

      Resultado = null;
      foreach (var domain in result.Domains)
      {
        Resultado += domain + "\n";
      }
    }
    private string _url;
    public string Url
    {
      get => _url;
      set
      {
        if (_url == value) return;
        _url = value;
        RaisePropertyChanged(nameof(Url));
      }
    }
    private string _resultado;
    public string Resultado
    {
      get => _resultado;
      set
      {
        if (_resultado == value) return;
        _resultado = value;
        RaisePropertyChanged(nameof(Resultado));
      }
    }

    public DelegateCommand LoadCommand { get; set; }
  }
}