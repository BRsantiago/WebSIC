using Entity.Entities;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSIC.Models;

namespace WebSIC.Controllers
{
    //[AllowAnonymous]
    public class PhotoController : Controller
    {
        IPessoaService PessoaService;

        public PhotoController(IPessoaService _PessoaService)
        {
            PessoaService = _PessoaService;
        }

        [HttpGet, ActionName("Index")]
        public ActionResult LoadIndex(string idPessoa)
        {
            PessoaViewModel model = new PessoaViewModel();
            model.IdPessoa = Convert.ToInt32(idPessoa);
            Session["val"] = "";
            Session["idPessoa"] = idPessoa;
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Index(string base64Image, string pessoaId)
        {
            try
            {
                Pessoa pessoa = new Pessoa();
                string idPessoa = Session["idPessoa"] != null ? Session["idPessoa"].ToString() : pessoaId;
                var uploadDir = "/WebImages";

                if (!string.IsNullOrEmpty(base64Image) && base64Image.Split(',').Length > 1)
                {
                    string date = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string fileName = idPessoa + ".jpg";

                    var imgPath = Server.MapPath(uploadDir) + "/" + fileName;
                    var bytes = Convert.FromBase64String(base64Image.Split(',')[1]);

                    using (var imageFile = new FileStream(imgPath, FileMode.Create))
                    {
                        imageFile.Write(bytes, 0, bytes.Length);
                        imageFile.Flush();
                    }
                    Session["val"] = date + "def.jpg";
                    ViewBag.Picture = "../../WebImages/" + fileName;

                    pessoa = PessoaService.ObterPorId(idPessoa);

                    pessoa.ImageUrl = Path.Combine(uploadDir, fileName);
                    pessoa.DataValidadeFoto = DateTime.Now.AddYears(2);

                    PessoaService.Atualizar(pessoa);
                }

                return Json(new { success = true, title = "Sucesso", message = "Foto capturada com sucesso!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    title = "Erro",
                    message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult Changephoto()
        {
            ViewBag.Picture = (Convert.ToString(Session["val"]) != string.Empty)
                ? "../../WebImages/" + Session["val"].ToString()
                : ViewBag.Picture = "../../WebImages/person.jpg";

            return View();
        }

        public JsonResult Rebind()
        {
            string path = "../../WebImages/" + Session["val"].ToString();
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
                var path = Server.MapPath("~/WebImages/" + date + "test.jpg");
                System.IO.File.WriteAllBytes(path, String_To_Bytes2(dump));
                ViewData["path"] = date + "test.jpg";
                Session["val"] = date + "test.jpg";
            }

            return View("Index", new PessoaViewModel() { IdPessoa = Convert.ToInt32(Session["idPessoa"].ToString()) });
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
