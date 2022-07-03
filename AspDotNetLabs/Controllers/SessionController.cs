using Microsoft.AspNetCore.Mvc;

namespace AspDotNetLabs.Controllers
{
    [Route("[controller]/[action]")]
    public class SessionController : Controller
    {
        [HttpGet("{parameter}"), ActionName("View")]
        public IActionResult ViewAction(string parameter)
        {
            string value = HttpContext.Session.GetString(parameter);
            value ??= "NO VALUE";
            return View(new KeyValuePair<string, string>(parameter, value));
        }

        [HttpGet("{parameter}/{value}")]
        public IActionResult Add(string parameter, string value)
        {
            HttpContext.Session.SetString(parameter, value);
            return View("View", new KeyValuePair<string, string>(parameter, value));
        }
    }
}
