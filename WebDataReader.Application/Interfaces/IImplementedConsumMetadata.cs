using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDataReader.Domain;
using WebDataReader.Domain.ValueObjects.Transform;

namespace WebDataReader.Application.Interfaces
{
  public interface IImplementedConsumMetadata
  {
    List<ColumnMetadata> GetComumnsMetadata(string query,string connectionString);
  }
}
