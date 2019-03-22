using System;
using System.Collections.Generic;
using WebDataReader.Domain.Util;

namespace WebDataReader.Domain.ValueObjects
{
  public class MyColumns : List<MyColumn> { }

  public class MyColumn
  {
    public string[] Html { get; set; }

    private MyColumn() { }

    public MyColumn(string[] html)
    {
      Html = html;
    }

    private string value;
    public string Value
    {
      get
      {
        if (value == null)
          value = GenerateValue(Html);
        return value;
      }
    }

    public string GenerateValue(string[] html)
    {
      var columns = new MyColumns();
      var h = String.Join("", html);
      var result = h.InnerContentFromTag();
      return result;
    }
  }
}
