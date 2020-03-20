using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using WebDataReader.Domain;
using Xunit;

namespace WebDataReader.DomainTest
{
  public class DomainExtracterTest
  {
    // HREF
    [Fact]
    public void HrefPerfectTestOK()
    {
      var hmtl = "<link href=\"https://www.esan.edu.pe/estaticos/2015/css/home.css?v1.2\" rel=\"stylesheet\" type=\"text/css\">";
      var result = new DomainExtracter().ExtraerDominioWithComodin("href=",'"', hmtl);
      result.Should().Be("https://www.esan.edu.pe/estaticos/2015/css/home.css?v1.2");
    }
    [Fact]
    public void HrefTestBlank_OK()
    {
      var hmtl = "";
      var result = new DomainExtracter().ExtraerDominioWithComodin("href=", '"', hmtl);
      result.Should().BeNull();
    }
    [Fact]
    public void HrefTestWithTextButEmpty_OK()
    {
      var hmtl = "oñiwjefoijoifj";
      var result = new DomainExtracter().ExtraerDominioWithComodin("href=", '"', hmtl);
      result.Should().BeNull();
    }
    [Fact]
    public void HrefWithStartButWithoutEndTestOK()
    {
      var hmtl = "<link href=\"https://www.esan.edu.pe/estaticos/2015/css/home.css?v1.2";
      var result = new DomainExtracter().ExtraerDominioWithComodin("href=", '"', hmtl);
      result.Should().BeNull();
    }
    
    // SRC
    [Fact]
    public void srcPerfectTestOK()
    {
      var hmtl = "<script src=\"https://www.esan.edu.pe/Scripts/jquery-validation-1.13.1/dist/localization/messages_es.min.js\"></script>";
      var result = new DomainExtracter().ExtraerDominioWithComodin("src=", '"', hmtl);
      result.Should().Be("https://www.esan.edu.pe/Scripts/jquery-validation-1.13.1/dist/localization/messages_es.min.js");
    }
    [Fact]
    public void srcTestBlank_OK()
    {
      var hmtl = "";
      var result = new DomainExtracter().ExtraerDominioWithComodin("src=", '"', hmtl);
      result.Should().BeNull();
    }
    [Fact]
    public void srcTestWithTextButEmpty_OK()
    {
      var hmtl = "oñiwjefoijoifj";
      var result = new DomainExtracter().ExtraerDominioWithComodin("src=", '"', hmtl);
      result.Should().BeNull();
    }
    [Fact]
    public void srcWithStartButWithoutEndTestOK()
    {
      var hmtl = "<script src=\"https://www.esan.edu.pe/Scripts/jquery-validation-1.13.1/dist/localization/mes";
      var result = new DomainExtracter().ExtraerDominioWithComodin("src=", '"', hmtl);
      result.Should().BeNull();
    }


    // URL
    [Fact]
    public void urlPerfectTestOK()
    {
      var hmtl = "  src: url('https://www.esan.edu.pe/estaticos/2015/js/slick-1.5.0/fonts/slick.eot?#iefix') format('embedded-opentype'),";
      var result = new DomainExtracter().ExtraerDominioWithComodin("url(", Char.Parse("'"), hmtl);
      result.Should().Be("https://www.esan.edu.pe/estaticos/2015/js/slick-1.5.0/fonts/slick.eot?#iefix");
    }
    [Fact]
    public void urlTestBlank_OK()
    {
      var hmtl = "";
      var result = new DomainExtracter().ExtraerDominioWithComodin("url(", Char.Parse("'"), hmtl);
      result.Should().BeNull();
    }
    [Fact]
    public void urlTestWithTextButEmpty_OK()
    {
      var hmtl = "oñiwjefoijoifj";
      var result = new DomainExtracter().ExtraerDominioWithComodin("url(", Char.Parse("'"), hmtl);
      result.Should().BeNull();
    }
    [Fact]
    public void urlWithStartButWithoutEndTestOK()
    {
      var hmtl = "    src: url('https://www.esan.edu.pe/estaticos/2015/js/sl,";
      var result = new DomainExtracter().ExtraerDominioWithComodin("url(", Char.Parse("'"), hmtl);
      result.Should().BeNull();
    }
  }
}
