using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDataReader.Application.Interfaces;
using WebDataReader.Domain.ValueObjects.Transform;
using WebDataReader.Persistence.Generic;

namespace WebDataReader.Persistence
{
  public class ImplementedConsumMetadata : DaoGeneric, IImplementedConsumMetadata
  {
    public List<ColumnMetadata> GetComumnsMetadata(string query, string stringConnection)
    {
      var adapter = new SqlDataAdapter(query, stringConnection)
      {
        MissingSchemaAction = MissingSchemaAction.AddWithKey
      };
      var table = new DataTable();
      adapter.Fill(table);

      using (DataTableReader reader = new DataTableReader(table))
      {
        var lista = DisplaySchemaTableInfo(reader);
        return lista;
      }
    }

     List<ColumnMetadata> DisplaySchemaTableInfo(DataTableReader reader)
    {
      var columnas = new List<ColumnMetadata>();

      DataTable schemaTable = reader.GetSchemaTable();

      foreach (DataRow rdrColumn in schemaTable.Rows)
      {
        var columna = new ColumnMetadata()
        {
          ColumnName = rdrColumn[schemaTable.Columns["ColumnName"]].ToString(),
          ColumnOrdinal = int.Parse(rdrColumn[schemaTable.Columns["ColumnOrdinal"]].ToString()),
          ColumnSize = int.Parse(rdrColumn[schemaTable.Columns["ColumnSize"]].ToString()),
          DataType = rdrColumn[schemaTable.Columns["DataType"]].ToString(),
          AllowDbNull = bool.Parse(rdrColumn[schemaTable.Columns["AllowDBNull"]].ToString()),
        };
        columnas.Add(columna);
      }

      return columnas;
    }
  }


}
