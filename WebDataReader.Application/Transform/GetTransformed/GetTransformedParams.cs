using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDataReader.Application.Transform.GetTransformed
{
  public class GetTransformedParams
  {
    public string Template { get; set; }
    public string Tsql { get; set; }
    public string ConnectionString { get; set; }
  }
}
