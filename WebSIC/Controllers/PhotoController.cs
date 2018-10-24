using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSIC.Controllers
{
    public class PhotoController : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {
            Session["val"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult Index(string base64Image)
        {
            if (!string.IsNullOrEmpty(base64Image) && base64Image.Split(',').Length > 1)
            {
                string date = DateTime.Now.ToString("yyyyMMddHHmmss");
                var imgPath = Server.MapPath("~/Images/Foto/" + date + "def.jpg");
                var bytes = Convert.FromBase64String(base64Image.Split(',')[1]);
                using (var imageFile = new FileStream(imgPath, FileMode.Create))
                {
                    imageFile.Write(bytes, 0, bytes.Length);
                    imageFile.Flush();
                }
                Session["val"] = date + "def.jpg";
                ViewBag.Picture = "http://localhost:55694/Images/Foto/" + Session["val"].ToString();
            }

            return View();
        }

        [HttpGet]
        public ActionResult Changephoto()
        {
            ViewBag.Picture = (Convert.ToString(Session["val"]) != string.Empty)
                ? "http://localhost:55694/Images/Foto/" + Session["val"].ToString()
                : ViewBag.Picture = "../../Images/Foto/person.jpg";

            return View();
        }

        public JsonResult Rebind()
        {
            string path = "http://localhost:55694/Images/Foto/" + Session["val"].ToString();
            return Json(path, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Capture()
        {
            var stream = Request.InputStream;

            using (var reader = new StreamReader(stream))
            {
                string dump = reader.ReadToEnd();
                DateTime nm = DateTime.Now;
                string date = nm.ToString("yyyyMMddHHmmss");
                var path = Server.MapPath("~/Images/Foto/" + date + "test.jpg");
                System.IO.File.WriteAllBytes(path, String_To_Bytes2(dump));
                ViewData["path"] = date + "test.jpg";
                Session["val"] = date + "test.jpg";
            }

            return View("Index");
        }

        private byte[] String_To_Bytes2(string strInput)
        {
            int numBytes = (strInput.Length) / 2;
            byte[] bytes = new byte[numBytes];
            for (int x = 0; x < numBytes; ++x)
                bytes[x] = Convert.ToByte(strInput.Substring(x * 2, 2), 16);

            return bytes;
        }
    }
}