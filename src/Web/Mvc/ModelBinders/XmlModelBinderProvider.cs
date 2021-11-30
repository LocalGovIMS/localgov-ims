﻿using System;
using System.Web;
using System.Web.Mvc;

namespace Web.Mvc
{
    public class XmlModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(Type modelType)
        {
            var contentType = HttpContext.Current.Request.ContentType;

            if (string.Compare(contentType, @"text/xml",
                StringComparison.OrdinalIgnoreCase) != 0)
            {
                return null;
            }

            return new XmlModelBinder();
        }
    }
}