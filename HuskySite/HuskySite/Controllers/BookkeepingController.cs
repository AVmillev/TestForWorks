using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HuskySite.Models;
using HuskySite.Models.BookkeepingModels;
using HuskySite.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HuskySite.Controllers
{
    public class BookkeepingController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HuskySiteContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public BookkeepingController(
            UserManager<ApplicationUser> userManager, 
            HuskySiteContext context,
            IHostingEnvironment environment
            )
        {
            _userManager = userManager;
            _context = context;
            _hostingEnvironment = environment;
        }
        // GET: Wallets
        public IActionResult Index()
        {

            //string webRootPath = _hostingEnvironment.WebRootPath + "/images/Expense.png";
           
            /*byte[] imageByteData = System.IO.File.ReadAllBytes(webRootPath);
            string imageBase64Data = Convert.ToBase64String(imageByteData);
            string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);*/
            //ViewBag.ImageData = Directory.GetFiles(webRootPath).First();
            //System.IO.Directory.GetFiles(webRootPath);
            
                       

            List<KeyValuePair<string, string>> images = new List<KeyValuePair<string, string>>();
            string webRootPath = _hostingEnvironment.WebRootPath + "/images/AccTypeImg/";
            foreach (string lfile in Directory.GetFiles(webRootPath))
            {
                var file = Path.Combine(Directory.GetCurrentDirectory(),
                                    "wwwroot", "images", "AccTypeImg", lfile);
                byte[] imageByteData = System.IO.File.ReadAllBytes(file);
                string imageBase64Data = Convert.ToBase64String(imageByteData);
                string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
                images.Add(new KeyValuePair<string, string>(Path.GetFileNameWithoutExtension(lfile).Replace("ic_ms_", ""), "images/AccTypeImg/"+ Path.GetFileName(lfile)));
            }
            ViewBag.ImageData = images;
            return View();
        }

        public IActionResult BannerImage()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(),
                                    "wwwroot", "images", "banner1.svg");
            return PhysicalFile(file, "image/svg+xml");
        }

        /*[HttpPost]
        public async Task<string> GetDataNalog()
        {
            NalogData data = new NalogData();
            string json = await data.GetReceiptAsinch("","","");
            JObject o = JObject.Parse(json);
            var converted = JsonConvert.SerializeObject(json);
            return converted;
        }*/

       

    }
}