using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using WebDataReader.Domain;
using WebDataReader.Domain.ValueObjects;
using WebDataReader.Domain.ValueObjects.WebReader;
using WebDataReader.Infraestructure;

namespace WebDataReader.DomainTest
{
  public class MyHtmlTest
  {
    public string HtmlFilePath(string fileName = "WebSunatHtml.txt")
    {
    
        var currentDirectory = Path.GetDirectoryName(Environment.CurrentDirectory);
        if (currentDirectory == null)
          throw new Exception("No se puede obtener el directorio principal dle proyecto");

        var path = new FileInfo(currentDirectory).Directory?.Parent?.FullName;
        if (path == null)
          throw new Exception("No se puede obtener el directorio principal dle proyecto");

        var fullPath = Path.Combine(path,fileName);

        if (!File.Exists(fullPath))
          throw new Exception($"No existe el archivo {fullPath}");
        return fullPath;
     
    }

    [Fact]
    public async Task GenerateTables()
    {
      var html = await new FileHtmlReader().GetHtml(HtmlFilePath());
      var tables = new MyHtml(html).GenerateTables(html);
      Assert.NotNull(tables);
      Assert.Equal(5, tables.Count);
    }

    [Fact]
    public async Task GenerateRows()
    {
      var htmlList = new List<string>();
      htmlList.Add("  <tr>");
      htmlList.Add("    <td class='beta' width='4%'  align='center' class='ht'>Da</td>");
      htmlList.Add("    <td class='beta' width='10%' align='center' class='ht'>Compra</td>");
      htmlList.Add("    <td class='beta' width='10%' align='center' class='ht'>Venta</td> ");
      htmlList.Add("  </tr>");
      htmlList.Add("  <tr>");
      htmlList.Add("    <td class='beta' width='4%' class='ht'>ddd</td>");
      htmlList.Add("    <td>mmmm</td> ");
      htmlList.Add("  </tr>");
      htmlList.Add("  <tr>");
      htmlList.Add("    <td></td> ");
      htmlList.Add("  </tr>");

      var html = htmlList.ToArray();
      var rows = new MyTable(html).GenerateRows(html);
      Assert.Equal(3,rows.Count);
    }

    [Fact]
    public async Task GenerateRows_FromFile()
    {
      var html = await new FileHtmlReader().GetHtml(HtmlFilePath("WebSunatHtml_onlyTable.txt"));
      var rows = new MyTable(html).GenerateRows(html);
      Assert.Equal(5, rows.Count);
    }

    [Fact]
    public async Task GenerateColumns_1()
    {
      var htmlList = new List<string>();
      htmlList.Add("  <tr>");
      htmlList.Add("    <td>Venta</td> ");
      htmlList.Add("  </tr>");

      var html = htmlList.ToArray();
      var columns = new MyRow(html).GenerateColumns(html);
      Assert.Equal(1, columns.Count);
    }
    [Fact]
    public async Task GenerateColumns_SingleLine()
    {
      var htmlList = new List<string>();
      htmlList.Add("  <tr>");
      htmlList.Add("    <td class='beta' width='4%'  align='center' class='ht'>Da</td>");
      htmlList.Add("    <td class='beta' width='10%' >Compra</td>");
      htmlList.Add("    <td>Venta</td> ");
      htmlList.Add("  </tr>");

      var html = htmlList.ToArray();
      var columns = new MyRow(html).GenerateColumns(html);
      Assert.Equal(3, columns.Count);
    }
    [Fact]
    public async Task GenerateColumns_MultiLine()
    {
      var htmlList = new List<string>();
      htmlList.Add("  <tr>");
      htmlList.Add(@"          <td width='8%' align='center' class='tne10'>
		  3.301
        </td>   ");
      htmlList.Add("    <td>Venta</td> ");
      htmlList.Add("  </tr>");

      var html = htmlList.ToArray();
      var columns = new MyRow(html).GenerateColumns(html);
      Assert.Equal(2, columns.Count);
    }

    [Fact]
    public async Task GenerateValues()
    {
 
      string[] html = { "<td width='8%' align='center' class='tne10'> hola </td>"};
      var columns = new MyColumn(html);
      Assert.Equal("hola", columns.GenerateValue(html));
    }
  }
}
