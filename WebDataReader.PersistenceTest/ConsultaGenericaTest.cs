using FluentAssertions;
using WebDataReader.Persistence;
using Xunit;

namespace WebDataReader.PersistenceTest
{
  public class ConsultaGenericaTest
  {
    [Fact]
    public void ConsultaSimpleTest()
    {
      string query = @"
SELECT *
  FROM [DemoCopia].[dbo].[Usuario] where Nombre='f'
";
      var c = new ImplementedConsumMetadata().GetComumnsMetadata(query, @"Data Source=DESKTOP-901LI9F\SQL2017DEV;Initial Catalog=DemoCopia;User ID=sa;Password=Pa$$123");
     // Assert.NotNull(c);
      c.Should().NotBeNull();
      c.Count.Should().Be(3);
      c[0].ColumnName.Should().Be("UsuarioId");
      c[2].ColumnName.Should().Be("Password");
    }
  }


}