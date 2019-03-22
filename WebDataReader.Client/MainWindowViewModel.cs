using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using WebDataReader.Application.Interfaces;
using WebDataReader.Application.Sunat;
using WebDataReader.Domain;
using WebDataReader.Domain.ValueObjects;

namespace WebDataReader.Client
{
  public class MainWindowViewModel : BindableBase
  {
    public MainWindowViewModel()
    {
      Titulo = "Data Reader";
      LoadTableSunatCommand = new DelegateCommand(async () => await Load());
      Mes = DateTime.Now.Month;
      Año = DateTime.Now.Year;
    }

    private string titulo;
    public string Titulo
    {
      get => titulo;
      set
      {
        if (titulo == value) return;
        titulo = value;
        RaisePropertyChanged(nameof(Titulo));
      }
    }

    private int mes = 1;
    public int Mes
    {
      get => mes;
      set
      {
        if (value < 1 || value > 12)
          throw new Exception("Número de mes inválido");

        if (mes == value) return;
        mes = value;
        RaisePropertyChanged(nameof(Mes));
        RaisePropertyChanged(nameof(SunatUrl));
      }
    }

    private int año;
    public int Año
    {
      get => año;
      set
      {
        if (value < 1995 || value > DateTime.Now.Year)
          throw new Exception("Número de año inválido");

        if (año == value) return;
        año = value;
        RaisePropertyChanged(nameof(Año));
        RaisePropertyChanged(nameof(SunatUrl));
      }
    }

    public string SunatUrl => $"https://e-consulta.sunat.gob.pe/cl-at-ittipcam/tcS01Alias?mesElegido=03&anioElegido=2019&mes={(Mes.ToString()).PadLeft(2, '0')}&anho={Año}&accion=init&email=";

    private string[] htmlSunatArray;

    private string htmlSunat;
    public string HtmlSunat
    {
      get => htmlSunat;
      set
      {
        if (htmlSunat == value) return;
        htmlSunat = value;
        RaisePropertyChanged(nameof(HtmlSunat));
      }
    }

    public async Task Load()
    {
      var getTipoCambioHandler = Bootstraper.Resolve<GetTipoCambioHandler>();
      var result = await getTipoCambioHandler.Handle(new GetTipoCambioParams()
      {
        SunatUrl = SunatUrl
      });

      htmlSunatArray = result.HtmlSunatArray;
      htmlSunat = result.HtmlSunat;
      SunatModel = result.SunatData;
      /* var result = await _htmlReader.GetHtml(SunatUrl);
       htmlSunatArray = result;
       HtmlSunat = String.Join("", result);
       var sunat = new SunatExtracter().ExtraerDataSunat(this.htmlSunatArray);
       SunatModel = sunat;*/
    }

    public DelegateCommand LoadTableSunatCommand { get; set; }

    private SunatData sunatModel;
    public SunatData SunatModel
    {
      get => sunatModel;
      set
      {
        if (sunatModel == value) return;
        sunatModel = value;
        RaisePropertyChanged(nameof(SunatModel));
      }
    }


  }
}
