using System.Collections.Generic;

namespace WebDataReader.Domain.ValueObjects.WebReader
{
  public class MyRows : List<MyRow> { }

  public class MyRow
  {
    public string[] Html { get; set; }

    private MyRow() { }

    public MyRow(string[] html)
    {
      Html = html;
    }

    private MyColumns columns;
    public MyColumns Columns
    {
      get
      {
        if (columns == null)
          columns = GenerateColumns(Html);
        return columns;
      }
    }


    public MyColumns GenerateColumns(string[] html)
    {
      var columns = new MyColumns();

      var isColumnStartedOpened = false;
      var temp = new List<string>();
      foreach (var h in html)
      {
        var i = h.Trim();

        if (isColumnStartedOpened)
        {
          if (i != "")
            temp.Add(i);
          if (i.Contains("</td>"))
          {
            isColumnStartedOpened = false;
            columns.Add(new MyColumn(temp.ToArray()));
            temp.Clear();
          }
        }
        else if (i.Contains("<td"))
        {
          if (i.Contains("</td>"))
          {
            var finalLength = i.Length - i.IndexOf("<td") - (i.Length - (i.IndexOf("</td>") + 5));
            string[] newRow = { i.Substring(i.IndexOf("<td"), finalLength) };
            columns.Add(new MyColumn(newRow));
          }
          else
          {
            isColumnStartedOpened = true;
            temp.Add(i.Substring(i.IndexOf("<td")));
          }
        }
      }
      return columns;
    }
  }
}
