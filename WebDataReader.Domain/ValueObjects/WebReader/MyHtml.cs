using System.Collections.Generic;

namespace WebDataReader.Domain.ValueObjects.WebReader
{
  public class MyHtml
  {
    public string[] Html { get; set; }

    private MyHtml() { }

    public MyHtml(string[] html)
    {
      Html = html;
    }

    private MyTables tables;
    public MyTables Tables
    {
      get
      {
        if (tables == null)
          tables = GenerateTables(Html);
        return tables;
      }
    }

    public MyTables GenerateTables(string[] html)
    {
      var tables = new MyTables();

      var isTableStarted = false;
      var temp = new List<string>();
      foreach (var h in html)
      {
        var i = h.Trim();

        if (isTableStarted)
        {
          if (i != "")
            temp.Add(i);
          if (i.Contains("</table>"))
          {
            isTableStarted = false;
            tables.Add(new MyTable(temp.ToArray()));
            temp.Clear();
          }
          continue;
        }

        if (i.Contains("<table"))
        {
          isTableStarted = true;
          temp.Add(i.Substring(i.IndexOf("<table")));
        }
      }
      return tables;
    }
  }
}
