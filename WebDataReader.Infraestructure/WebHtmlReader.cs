using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using WebDataReader.Application.Interfaces;

namespace WebDataReader.Infraestructure
{
  public class WebHtmlReader : IHtmlReader
  {
    public async Task<string[]> GetHtml(string url)
    {
      //await Task.Delay(2000);
      HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
      request.Method = "GET";
      using (var response = request.GetResponse())
      using (var stream = response.GetResponseStream())
      using (var reader = new StreamReader(stream))
      {
        HttpStatusCode statusCode = ((HttpWebResponse)response).StatusCode;
        string contents = await reader.ReadToEndAsync();
        return contents.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
      }
    }
  }
}
