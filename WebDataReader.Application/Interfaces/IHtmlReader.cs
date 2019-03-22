using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDataReader.Application.Interfaces
{
  public interface IHtmlReader
  {
     Task<string[]> GetHtml(string url);
  }
}
