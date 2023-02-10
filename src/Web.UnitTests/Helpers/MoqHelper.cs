using Moq;
using System.IO;
using System.Web;
using System.Web.SessionState;

namespace Web.UnitTests.Helpers
{
    public static class MoqHelper
    {
        public static HttpContext FakeHttpContext()
        {
            return FakeHttpContext("text/xml");
        }

        public static HttpContext FakeHttpContext(string contentType)
        {

            var httpRequest = new HttpRequest("", "http://www.test.com/test", "");
            httpRequest.ContentType = contentType;

            var stringWriter = new StringWriter();
            var httpResponse = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponse);

            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                                                    new HttpStaticObjectsCollection(), 10, true,
                                                    HttpCookieMode.AutoDetect,
                                                    SessionStateMode.InProc, false);

            SessionStateUtility.AddHttpSessionStateToContext(httpContext, sessionContainer);

            return httpContext;
        }

        public static HttpContext FakeHttpContext(HttpRequest httpRequest)
        {
            httpRequest.ContentType = "text/xml";

            var stringWriter = new StringWriter();
            var httpResponse = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponse);

            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                                                    new HttpStaticObjectsCollection(), 10, true,
                                                    HttpCookieMode.AutoDetect,
                                                    SessionStateMode.InProc, false);

            SessionStateUtility.AddHttpSessionStateToContext(httpContext, sessionContainer);

            return httpContext;
        }

    }
}
