using WebDataReader.Domain;

namespace WebDataReader.Application.Sunat.GetTipoCambio
{
  public class GetTipoCambioModel
  {
    public string[] HtmlSunatArray { get; set; }
    public string HtmlSunat { get; set; }
    public SunatData SunatData { get; set; }
  }
}