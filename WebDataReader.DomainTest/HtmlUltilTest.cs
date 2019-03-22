using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using WebDataReader.Domain;
using WebDataReader.Domain.Util;

namespace WebDataReader.DomainTest
{
  public class HtmlUltilTest
  {
     [Fact]
     public void InnerHtml_Simple_Ok()
     {
       string html = @"<td>Dia</td>";
       Assert.Equal("Dia", html.InnerHtmlFromTag());
     }
     [Fact]
     public void InnerHtml_SimpleWithSpace_Ok()
     {
       string html = @"<td>  Dia  </td>";
       Assert.Equal("Dia", html.InnerHtmlFromTag());
     }

     [Fact]
     public void InnerHtml_WithAtrributes_Ok()
     {
       string html = @"<td class='algo'>  Dia  </td>";
       Assert.Equal("Dia", html.InnerHtmlFromTag());
     }

     [Fact]
     public void InnerHtml_WithAtrributes_WithTagContent_Ok()
     {
       string html = @"<td class='algo'>  <strong>Dia</strong>  </td>";
       Assert.Equal("<strong>Dia</strong>", html.InnerHtmlFromTag());
     }

     [Fact]
     public void InnerHtml_WithEnter_Ok()
     {
       string html = @"        <td width='8%' align='center' class='tne10'>  
		  HOLA
        </td> ";
       Assert.Equal("HOLA", html.InnerHtmlFromTag());
     }

    [Fact]
    public void InnerHtml2_Ok()
    {
      string html = @"<strong>Dia</strong>";
      Assert.Equal("Dia", html.InnerHtmlFromTag());
    }

    [Fact]
    public void InnerContent_WithAtrributes_WithTagContent_Ok()
    {
      string html = @"<td class='algo'>  <strong>Dia</strong>  </td>";
      Assert.Equal("Dia", html.InnerContentFromTag());
    }
    /*
    [Fact]
    public void InnerContent_SimpleError()
    {
      string html = @"<td  Dia  </td>";
      Assert.Equal("", html.InnerContentFromTag());
    }*/
  }
}
