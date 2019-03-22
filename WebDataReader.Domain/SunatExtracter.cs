using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDataReader.Domain.ValueObjects;

namespace WebDataReader.Domain
{
  public class SunatData
  {
    public SunatData()
    {
      Cambios = new List<TipoCambio>();
    }
    public string Fecha { get; set; }
    public List<TipoCambio> Cambios { get; set; }
  }

  public class TipoCambio
  {
    public int Dia { get; set; }
    public double Compra { get; set; }
    public double Venta { get; set; }
  }

  public class SunatExtracter
  {
    public SunatData ExtraerDataSunat(string[] html)
    {
      var sunatData = new SunatData();
      sunatData.Fecha = ExtraerDataSunat_Fecha(html);

      var mymhtml = new MyHtml(html);
      sunatData.Cambios = ExtraerDataSunat_TiposDeCambio(mymhtml.Tables[1]);
      return sunatData;
    }

    public string ExtraerDataSunat_Fecha(string[] html)
    {
      var isFormFound = false;
      foreach (var linea in html)
      {
        var i = linea.Trim();

        if (isFormFound)
        {
          if (i.StartsWith("<h3>"))
            return i.Substring(4, i.Length - 9).Trim();
        }
        if (i.StartsWith("<form method="))
          isFormFound = true;

      }
      return String.Empty;
    }

    public List<TipoCambio> ExtraerDataSunat_TiposDeCambio(MyTable table)
    {
      var model = new List<TipoCambio>();
      for (int j = 1; j < table.Rows.Count; j++)
      {
        var row = table.Rows[j];
        var datePosition = 0;

        var tipoCambioTemp = new TipoCambio();

        for (int k = 0; k < row.Columns.Count; k++)
        {
          datePosition++;
          if (datePosition == 1)
            tipoCambioTemp.Dia = int.Parse(row.Columns[k].Value);
          else if (datePosition == 2)
            tipoCambioTemp.Compra = double.Parse(row.Columns[k].Value);
          else
          {
            tipoCambioTemp.Venta = double.Parse(row.Columns[k].Value);
            datePosition = 0;
            model.Add(tipoCambioTemp);
            tipoCambioTemp = new TipoCambio();
          }
        }
      }
      return model;
    }
  }
}
