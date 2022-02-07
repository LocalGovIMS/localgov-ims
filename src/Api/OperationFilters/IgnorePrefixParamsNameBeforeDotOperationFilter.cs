using Swashbuckle.Swagger;
using System.Linq;
using System.Web.Http.Description;

namespace Api.OperationFilters
{
    public class IgnorePrefixParamsNameBeforeDotOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters != null)
            {
                foreach (Parameter param in operation.parameters.Where(p => p.name.Contains('.')))
                {
                    param.name = param.name.Split('.').Last();
                }
            }
        }
    }
}