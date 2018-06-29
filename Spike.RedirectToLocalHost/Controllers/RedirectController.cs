using System.Collections.Specialized;
using System.Text;
using System.Web.Mvc;
using Spike.RedirectToLocalHost.Models;

namespace Spike.RedirectToLocalHost.Controllers
{
    public class RedirectController : Controller
    {
        private RedirectModel GetModel(NameValueCollection queryString, string portNumber, string levelA, string levelB)
        {
            var redirectPath = new StringBuilder();

            if (!string.IsNullOrEmpty(levelA))
            {
                redirectPath.Append($"/{levelA}");
            }

            if (!string.IsNullOrEmpty(levelB))
            {
                redirectPath.Append($"/{levelB}");
            }

            var queryParms = new StringBuilder("?");

            foreach (var parmeter in queryString)
            {
                if (queryParms.Length > 1)
                {
                    queryParms.Append("&");
                }
                queryParms.Append(parmeter + "=" + queryString[parmeter.ToString()]);
            }

            var port = string.Empty;

            if (!string.IsNullOrEmpty(portNumber))
            {
                port = $":{portNumber}";
            }

            return new RedirectModel { Url = $"http://localhost{port}{redirectPath}{queryParms.ToString()}" };
        }

        // GET: Redirect
       // [Route("Redirect/Index/{portNumber}")]
        public ActionResult Index(string portNumber, string levelA, string levelB)
        {
          
            var queryString = ControllerContext.RequestContext.HttpContext.Request.QueryString;

            var model = GetModel(queryString, portNumber, levelA, levelB);

            return View(model);
        }




    }
}
