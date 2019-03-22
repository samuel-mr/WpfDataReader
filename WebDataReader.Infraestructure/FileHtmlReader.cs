using System;
using System.IO;
using System.Threading.Tasks;
using WebDataReader.Application.Interfaces;

namespace WebDataReader.Infraestructure
{
  public class FileHtmlReader : IHtmlReader
  {
    public async Task<string[]> GetHtml(string url)
    {
      if (File.Exists(url) == false)
        throw new Exception($"No existe el archivo {url}");

      var file = File.ReadAllLines(url);
      return file;
    }
  }
}
