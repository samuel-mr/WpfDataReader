using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDataReader.Application.Interfaces;
using WebDataReader.Domain;

namespace WebDataReader.Application.Sunat
{
  public class GetTipoCambioHandler
  {
    private readonly IHtmlReader _htmlReader;

    public GetTipoCambioHandler(IHtmlReader htmlReader)
    {
      _htmlReader = htmlReader;
    }

    public async Task<GetTipoCambioModel> Handle(GetTipoCambioParams @params)
    {
      var model = new GetTipoCambioModel();

      var result = await _htmlReader.GetHtml(@params.SunatUrl);
      model.HtmlSunatArray = result;
      model.HtmlSunat = String.Join("", result);
      model.SunatData = new SunatExtracter().ExtraerDataSunat(result);
      return model;
    }
  }
}
