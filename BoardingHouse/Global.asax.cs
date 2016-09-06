using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Service;

namespace BoardingHouse
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected MvcApplication()
        {
            Error += OnError;
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        private void OnError(object sender, EventArgs eventArgs)
        {
            var httpApplication = (HttpApplication)sender;
            Exception currentError = httpApplication.Context.Error;
            
            if (currentError is ApplicationException)
            {
                var message = (currentError as ApplicationException).Message;
                SetCustomError(httpApplication, message, 500);
                return;
            }

            if (currentError is UnauthorizedAccessException)
            {
                var message = (currentError as UnauthorizedAccessException).Message;
                SetCustomError(httpApplication, message, 410);
                return;
            }

            SetCustomError(httpApplication, "SYSTEM_ERROR", 500);
        }

        private void SetCustomError(HttpApplication application, string message, int code, bool formatMessage = true)
        {
            if (formatMessage)
            {
                message = FormatErrorMessage(message);
            }

            application.Context.ClearError();
            application.Context.Response.TrySkipIisCustomErrors = true;
            application.Context.Response.Write(message);
            application.Context.Response.ContentType = "application/json; charset=utf-8";
            application.Context.Response.StatusCode = code;
        }

        private string FormatErrorMessage(string message)
        {
            return "{" + string.Format("\"Message\":\"{0}\"", message) + "}";
        }
    }
}
