using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDataReader.Domain
{
  public class DomainExtracter
  {
    public List<string> ExtraerDominios(string[] html)
    {
      var domains = new List<string>();
      var domain = new DomainExtracter();
      foreach (var linea in html)
      {
        if (string.IsNullOrWhiteSpace(linea))
          continue;
        var result1 = domain.ExtraerDominioWithComodin("href=", '"', linea);
        if (result1 != null)
          domains.Add(result1);
        var result2 = domain.ExtraerDominioWithComodin("src=", '"', linea);
        if (result2 != null)
          domains.Add(result2);
        var result3 = domain.ExtraerDominioWithComodin("url(", Char.Parse("'"), linea);
        if (result3 != null)
          domains.Add(result3);
      }
      return domains;
    }

    public string ExtraerDominioWithComodin(string pattern, char separator, string linea)
    {
      var indIni = linea.IndexOf(pattern, StringComparison.Ordinal);
      if (indIni >= 0)
      {
        var longitud = pattern.Length + 1;
        var indFin = linea.IndexOf(separator, indIni + longitud);
        if (indFin >= 0)
        {
          return linea.Substring(indIni + longitud, indFin - (indIni + longitud));
        }
      }
      return null;
    }
  }
}
