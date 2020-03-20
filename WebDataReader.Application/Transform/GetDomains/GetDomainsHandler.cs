using System;
using System.Threading.Tasks;
using WebDataReader.Application.Interfaces;
using WebDataReader.Application.Sunat.GetTipoCambio;
using WebDataReader.Domain;

namespace WebDataReader.Application.Transform.GetDomains
{
  public class GetDomainsHandler
  {
    private readonly IHtmlReader _htmlReader;

    public GetDomainsHandler(IHtmlReader htmlReader)
    {
      _htmlReader = htmlReader;
    }

    public async Task<GetDomainsModel> Handle(GetDomainsParams @params)
    {
      var model = new GetDomainsModel();

      var result = await _htmlReader.GetHtml(@params.Url);
      model.Domains = new DomainExtracter().ExtraerDominios(result);
      return model;
    }
  }
}