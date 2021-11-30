using System.Web.Mvc;
using System.Xml.Serialization;

namespace Web.Mvc
{
    public class XmlModelBinder : IModelBinder
    {
        public object BindModel(
            ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            var modelType = bindingContext.ModelType;
            var serializer = new XmlSerializer(modelType);

            var inputStream = controllerContext.HttpContext.Request.InputStream;

            return serializer.Deserialize(inputStream);
        }
    }
}