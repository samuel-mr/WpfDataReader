using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WebDataReader.Domain.Util
{
  public static class StringHelpers
  {
    public static string FirstAsUpperCase(this string text)
    {
      if (text == null) return null;
      if (text.Length <= 1) return text;
      return text[0].ToString().ToUpper() + text.Substring(1, text.Length - 1);
    }
  }
}
