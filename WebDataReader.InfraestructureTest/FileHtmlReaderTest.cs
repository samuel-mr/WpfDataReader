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
  public class  FileHtmlReaderTest
  {
    [Fact]
    public async Task TestOk()
    {
      var reader = new FileHtmlReader();
      var result = await reader.GetHtml(@"D:\Proyects\WebDataReader\WebSunatHtml.txt");
      Assert.NotNull(result);
    }
  }
}
