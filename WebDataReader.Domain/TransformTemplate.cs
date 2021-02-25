using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
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
    public string Transform(string fullTemplate, List<ColumnMetadata> metadata)
    {
      StringBuilder sb = new StringBuilder();

      List<string> templateArray = new List<string>();
      var sbTemp = new StringBuilder();
      foreach (var item in fullTemplate)
      {
        if (item == 13) //enter
        {
          templateArray.Add(sbTemp.ToString());
          sbTemp.Clear();
        }
        else
        {
          if (item == 10) //newline
            continue;
          sbTemp.Append(item);
        }
      }
      if (sbTemp.Length > 0)
      {
        templateArray.Add(sbTemp.ToString());
        sbTemp.Clear();
      }
      foreach (var columnMetadata in metadata)
      {
        foreach (var template in templateArray)
        {
          var resp = TransformLine(template, columnMetadata);
          if (resp != null && resp != "NULL")
            sb.AppendLine(resp);
        }
      }

      return sb.ToString();
    }

    public string CleanType(string type)
    {
      return type
        .Replace("System.", "")
        .Replace("Int32", "int")
        .Replace("String", "string");
    }
    public string TransformLine(string linetemplate, ColumnMetadata metadata)
    {
      metadata.DataType = CleanType(metadata.DataType);
      if (metadata.AllowDbNull && metadata.DataType != "string")
      {
        linetemplate = linetemplate
            .Replace("{{Type}}", $"{metadata.DataType}?")
          ;
      }
      else
      {
        linetemplate = linetemplate
            .Replace("{{Type}}", metadata.DataType)
          ;
      }

      linetemplate = linetemplate
        .Replace("{{Name}}", metadata.ColumnName);

      linetemplate = ExtractSingle_Is(linetemplate, metadata);
      return linetemplate;
    }
    public string ExtractSingle_Is(string linetemplate, ColumnMetadata metadata)
    {
      if (linetemplate.StartsWith("{{") && linetemplate.EndsWith("}}"))
      {
        linetemplate = linetemplate.Substring(2, linetemplate.Length - 4).Trim();

        // SI no tiene formato =>    ?  " " " "
        var indexFirstQuestion = linetemplate.IndexOf("?");
        if (indexFirstQuestion < 1)
          return null;

        if (linetemplate.Substring(indexFirstQuestion).IndexOf("::") == -1)
          return null;

        // Si inicial con => IsRequired
        if (linetemplate.Substring(0, indexFirstQuestion).Trim() == "IsRequired")
        {
          return ExtractSingle_IsRequired(linetemplate, metadata);
        }
        // Si inicial con => IsSized
        if (linetemplate.Substring(0, indexFirstQuestion).Trim() == "IsSizable")
        {
          return ExtractSingle_IsSizable(linetemplate, metadata);
        }

      }
      return linetemplate;
    }
    public string ExtractSingle_IsRequired(string linetemplate, ColumnMetadata metadata)
    {
      var tipoAdmitidos = new List<string>()
      {
        "string"
      };
      if (!tipoAdmitidos.Contains(metadata.DataType))
        return null;

      var indexFirstQuestion = linetemplate.IndexOf("?");

      linetemplate = linetemplate.Substring(indexFirstQuestion + 1).Trim();

      var pares = PartesEntreComillas(linetemplate);
      if (metadata.AllowDbNull)
      {
        return pares.segundoValor;
      }
      else
      {
        return pares.primerValor;
      }
    }

    public string ExtractSingle_IsSizable(string linetemplate, ColumnMetadata metadata)
    {
      var tipoAdmitidos = new List<string>()
      {
        "string"
      };
      if (!tipoAdmitidos.Contains(metadata.DataType))
        return null;

      var indexFirstQuestion = linetemplate.IndexOf("?");

      linetemplate = linetemplate.Substring(indexFirstQuestion + 1).Trim();

      var pares = PartesEntreComillas(linetemplate);

      if (metadata.DataType == "string")
        if (metadata.ColumnSize < 2147483647) // Que no sea varchar(max)
          return pares.primerValor.Replace("{{Size}}", metadata.ColumnSize.ToString());
      return null;
    }

    (string primerValor, string segundoValor) PartesEntreComillas(string linetemplate)
    {
      var _1 = 0;
      var _2 = linetemplate.IndexOf("::", _1 + 1);
      var _3 = _2 + 1;
      var _4 = linetemplate.Length;

      string primeroContenido = linetemplate.Substring(_1, _2 - _1);
      string segundoContenido = linetemplate.Substring(_3 + 1, _4 - (_3 + 1));

      return (primeroContenido, segundoContenido);
    }
  }
}
