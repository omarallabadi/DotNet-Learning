using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SocialMediaLinks.Controllers
{
    public class HomeController : Controller
    {
        private readonly SocialMediaLinksOptions _options;


        public HomeController(IOptions<SocialMediaLinksOptions> options) {
        _options = options.Value;
            
        
        }


        [Route("/")]
        public IActionResult Index()
        {
            ViewBag.instagram = _options.Instagram;
            ViewBag.facebook = _options.Facebook;
            ViewBag.twitter = _options.Twitter;
            ViewBag.youtube = _options.Youtube;

            return View();
        }
    }
}
