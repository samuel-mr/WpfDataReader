using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions.Common;
using WebDataReader.Infraestructure;
using Xunit;

namespace WebDataReader.InfraestructureTest
{
  public class WebHtmlReaderTest
  {
    [Fact]
    public async Task TestOk()
    {
      var reader = new WebHtmlReader();
      var result = await reader.GetHtml("https://e-consulta.sunat.gob.pe/cl-at-ittipcam/tcS01Alias?mesElegido=03&anioElegido=2019&mes=03&anho=2019&accion=init&email=");
      Assert.NotNull(result);
    }
  }
}
