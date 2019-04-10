using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDataReader.Domain.ValueObjects.Transform;

namespace WebDataReader.Domain
{
  public class TransformTemplate
  {
    public string Transform(string template, List<ColumnMetadata> metadata)
    {
      StringBuilder sb = new StringBuilder();
      foreach (var columnMetadata in metadata)
      {
        sb.AppendLine(TransformLine(template, columnMetadata));
      }
      return sb.ToString();
    }

    public string CleanType(string type)
    {
      return type.Replace("System.", "");
    }
    public string TransformLine(string linetemplate, ColumnMetadata metadata)
    {
      metadata.DataType = CleanType(metadata.DataType);
      if (metadata.AllowDbNull && metadata.DataType != "String")
      {
        linetemplate = linetemplate
            .Replace("{{Type}}", metadata.DataType + "?")
          ;
      }
      else
      {
        linetemplate = linetemplate
            .Replace("{{Type}}", metadata.DataType)
          ;
      }

      return linetemplate
        .Replace("{{Name}}", metadata.ColumnName)
        ;
    }
  }
}
