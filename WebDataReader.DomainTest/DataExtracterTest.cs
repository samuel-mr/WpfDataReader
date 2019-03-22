using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using FluentAssertions.Common;
using System.Threading.Tasks;
using WebDataReader.Domain;
using WebDataReader.Infraestructure;
using Xunit;

namespace WebDataReader.DomainTest
{
  public class DataExtracterTest
  {
    public string HtmlFilePath
    {
      get
      {
        var currentDirectory = Path.GetDirectoryName(Environment.CurrentDirectory);
        if (currentDirectory == null)
          throw new Exception("No se puede obtener el directorio principal dle proyecto");

        var path = new FileInfo(currentDirectory).Directory?.Parent?.FullName;
        if (path == null)
          throw new Exception("No se puede obtener el directorio principal dle proyecto");

        var fullPath = Path.Combine(path, "WebSunatHtml.txt");

        if (!File.Exists(fullPath))
          throw new Exception($"No existe el archivo {fullPath}");
        return fullPath;
      }
    }

    [Fact]
    public async Task ExtraerFecha_Ok()
    {
      var html = await new FileHtmlReader().GetHtml(HtmlFilePath);
      var fecha = new SunatExtracter().ExtraerDataSunat_Fecha(html);
      Assert.Equal("Marzo - 2019", fecha);// fecha.IsSameOrEqualTo("Marzo - 2019x");
    }

    [Fact]
    public async Task ExtraerTablaCompleta_Ok()
    {
      var html = await new FileHtmlReader().GetHtml(HtmlFilePath);
      var fecha = new SunatExtracter().ExtraerDataSunat_Fecha(html);
      Assert.Equal("Marzo - 2019", fecha);// fecha.IsSameOrEqualTo("Marzo - 2019x");
    }
  }
}
