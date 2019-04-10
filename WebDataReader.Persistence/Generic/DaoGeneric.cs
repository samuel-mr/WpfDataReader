using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDataReader.Persistence.Generic
{
  public abstract class DaoGeneric
  {
    internal string Connection
    {
      get
      {
        var con = ConfigurationManager.ConnectionStrings["ServidorLocal"].ConnectionString;
        if (con == null)
          throw new Exception("La cada de conexion no se ha especificado");
        return con;
      }
    }
  }
}
