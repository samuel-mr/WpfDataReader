using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDataReader.Domain;
using WebDataReader.Domain.ValueObjects;
using WebDataReader.Infraestructure;
using Xunit;

namespace WebDataReader.DomainTest
{
  public class SunatExtracterTest
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
    public async Task ExtraerTiposCambio_Test()
    {
      var arrays = await new FileHtmlReader().GetHtml(HtmlFilePath);
      var myhtml = new MyHtml(arrays);
      var model = new List<TipoCambio>();

      if (myhtml.Tables.Count < 2)
        throw new Exception($"Se esperaba almenos 2 tablas en la estructura de la Sunat, la cantidad actual es {myhtml.Tables.Count}");

      var table = myhtml.Tables[1];

      var cambios = new SunatExtracter().ExtraerDataSunat_TiposDeCambio(table);
      Assert.Equal(14, cambios.Count);

      Assert.Equal(1, cambios[0].Dia);
      Assert.Equal(3.3, cambios[0].Compra);
      Assert.Equal(3.305d, cambios[0].Venta);

      Assert.Equal(20, cambios[13].Dia);
      Assert.Equal(3.3, cambios[13].Compra);
      Assert.Equal(3.301d, cambios[13].Venta);
    }
  }
}
