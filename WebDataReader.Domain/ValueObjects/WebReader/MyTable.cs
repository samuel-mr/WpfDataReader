using System.Collections.Generic;

namespace WebDataReader.Domain.ValueObjects.WebReader
{
  public class MyTables : List<MyTable>
  {

  }

  public class MyTable
  {
    public string[] Html { get; set; }

    private MyTable() { }

    public MyTable(string[] html)
    {
      Html = html;
    }

    private MyRows rows;
    public MyRows Rows
    {
      get
      {
        if (rows == null)
          rows = GenerateRows(Html);
        return rows;
      }
    }

    public MyRows GenerateRows(string[] html)
    {
      var rows = new MyRows();

      var isRowStarted = false;
      var temp = new List<string>();
      foreach (var h in html)
      {
        var i = h.Trim();

        if (isRowStarted)
        {
          if (i != "")
            temp.Add(i);
          if (i.Contains("</tr>"))
          {
            isRowStarted = false;
            rows.Add(new MyRow(temp.ToArray()));
            temp.Clear();
          }
          continue;
        }

        if (i.Contains("<tr"))
        {
          isRowStarted = true;
          temp.Add(i.Substring(i.IndexOf("<tr")));
        }
      }
      //to fix (if don't exist final "tr")
      if (isRowStarted && temp.Count > 0)
      {
        rows.Add(new MyRow(temp.ToArray()));
      }
      return rows;
    }
  }
}
