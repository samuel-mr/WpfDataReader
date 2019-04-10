using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDataReader.Application.Interfaces;

namespace WebDataReader.Application.Transform.GetTransformed
{
  public class GetTransformedHandler
  {
    private readonly IImplementedConsumMetadata _implementedConsumMetadata;

    public GetTransformedHandler(IImplementedConsumMetadata implementedConsumMetadata)
    {
      _implementedConsumMetadata = implementedConsumMetadata;
    }

    public GetTransformedModel Handle(GetTransformedParams param)
    {
      var columns = _implementedConsumMetadata.GetComumnsMetadata(param.Tsql, param.ConnectionString);
      var transformed = new Domain.TransformTemplate().Transform(param.Template, columns);
      return new GetTransformedModel()
      {
        Transformed = transformed
      };
    }
  }

}
