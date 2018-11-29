using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Entity.DTO;
using Entity.Entities;
using Repository.Context;
using Service.Interface;
using WebSIC.Models;

namespace WebSIC.Controllers
{
    [AllowAnonymous]
    public class PessoaController : Controller
    {
        public IPessoaService PessoaService;
        public ICursoSemTurmaService CursoSemTurmaService;
        public ICursoService CursoService;

        public PessoaController(IPessoaService _PessoaService,
                                    ICursoSemTurmaService _CursoSemTurmaService,
                                         ICursoService _CursoService)
        {
            PessoaService = _PessoaService;
            CursoSemTurmaService = _CursoSemTurmaService;
            CursoService = _CursoService;
        }

        // GET: Pessoa
        public ActionResult Index()
        {
            var pessoas = PessoaService.ObterTodos();
            return View(pessoas);
        }

        // GET: Pessoa/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = PessoaService.ObterPorId(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // GET: Pessoa/Create
        public ActionResult Create()
        {
            ViewBag.Picture = "../../WebImages/person.jpg";

            return View();
        }

        // POST: Pessoa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PessoaViewModel model)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                Pessoa pessoa = model.MapearParaObjetoDominio();
                pessoa.ImageUrl = "../../WebImages/person.jpg";
                PessoaService.IncluirPessoa(pessoa);
                return RedirectToAction("Edit", new { id = pessoa.IdPessoa });
                //}
                //return View(model);
                //var msg = "<script> swal({title: 'Good job!', text: 'Alterações salvas com sucesso !', icon: 'success', button: 'OK!'}) </script>";

                //TempData["notification"] = msg;

                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var msg = "<script> swal({title: 'Atenção!', text: '" + ex.Message.Replace('\'', ' ') + "', icon: 'warning', button: 'OK!'}) </script>";

                TempData["notification"] = msg;

                return View(model);
            }
        }

        // GET: Pessoa/Edit/5
        public ActionResult Edit(string id)
        {
            //ViewBag.Picture = (Convert.ToString(Session["val"]) != string.Empty) ? "../../WebImages/" + Session["val"].ToString() : "../../WebImages/person.jpg";

            Pessoa pessoa = PessoaService.ObterPorId(id);
            return View(new PessoaViewModel(pessoa));
        }

        // POST: Pessoa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PessoaViewModel model)
        {
            try
            {
                Pessoa pessoa = PessoaService.ObterPorId(model.IdPessoa.ToString());

                pessoa.IdPessoa = model.IdPessoa;
                pessoa.NomeCompleto = model.NomeCompleto.ToUpper();
                pessoa.Nome = String.IsNullOrEmpty(model.Nome) ? model.Nome : model.Nome.ToUpper();
                pessoa.DataNascimento = Convert.ToDateTime(model.DataNascimento);
                pessoa.NomePai = String.IsNullOrEmpty(model.NomePai) ? model.NomePai : model.NomePai.ToUpper();
                pessoa.NomeMae = String.IsNullOrEmpty(model.NomeMae) ? model.NomeMae : model.NomeMae.ToUpper();
                pessoa.Endereco = model.Endereco;
                pessoa.Numero = Convert.ToInt32(model.Numero);
                pessoa.Complemento = String.IsNullOrEmpty(model.Complemento) ? model.Complemento : model.Complemento.ToUpper();
                pessoa.Bairro = String.IsNullOrEmpty(model.Bairro) ? model.Bairro : model.Bairro.ToUpper();
                pessoa.Cidade = String.IsNullOrEmpty(model.Cidade) ? model.Cidade : model.Cidade.ToUpper();
                pessoa.UF = String.IsNullOrEmpty(model.UF) ? model.UF : model.UF.ToUpper();
                pessoa.CEP = model.CEP;
                pessoa.TelefoneEmergencia = model.TelefoneEmergencia;
                pessoa.TelefoneResidencial = model.TelefoneResidencial;
                pessoa.TelefoneCelular = model.TelefoneCelular;
                pessoa.RNE = model.RNE;
                pessoa.CPF = model.CPF;
                pessoa.RG = model.RG;
                pessoa.OrgaoExpeditor = String.IsNullOrEmpty(model.OrgaoExpeditor) ? model.OrgaoExpeditor : model.OrgaoExpeditor.ToUpper();
                pessoa.UFOrgaoExpeditor = String.IsNullOrEmpty(model.UFOrgaoExpeditor) ? model.UFOrgaoExpeditor : model.UFOrgaoExpeditor.ToUpper();
                pessoa.Genero = model.Genero;
                pessoa.Observacao = model.Observacao;
                pessoa.FlgCVE = model.FlgCVE;
                pessoa.Email = model.Email;
                pessoa.CNH = model.CNH;
                pessoa.CategoriaUm = model.CategoriaUm;
                pessoa.CategoriaDois = model.CategoriaDois;
                if (!String.IsNullOrEmpty(model.DataValidadeCNH)) pessoa.DataValidadeCNH = Convert.ToDateTime(model.DataValidadeCNH);
                //pessoa.Usuario = model.Usuario;
                //pessoa.Solicitacaos = model.Solicitacaos;
                //pessoa.Turmas = model.Turmas;
                //pessoa.Credenciais = model.Credenciais;
                //pessoa.Empresas = model.Empresas;
                //pessoa.Curso = model.Curso;
                pessoa.Atualizacao = DateTime.Now;
                pessoa.Atualizador = "";
                pessoa.Ativo = model.Ativo;
                pessoa.ImageUrl = model.ImageUrl;
                if (!String.IsNullOrEmpty(model.DataValidadeFoto)) pessoa.DataValidadeFoto = Convert.ToDateTime(model.DataValidadeFoto);

                UploadFilesHandler(model, pessoa.IdPessoa);

                pessoa.RGFilePath = model.RGFilePath;
                pessoa.CRFilePath = model.CRFilePath;
                pessoa.CNHFilePath = model.CNHFilePath;
                pessoa.CTPSFilePath = model.CTPSFilePath;

                PessoaService.Atualizar(pessoa);

                var msg = "<script> swal({title: 'Good job!', text: 'Alterações salvas com sucesso !', icon: 'success', button: 'OK!'}) </script>";

                TempData["notification"] = msg;

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                var msg = "<script> swal({title: 'Atenção!', text: '" + ex.Message.Replace('\'', ' ') + "', icon: 'warning', button: 'OK!'}) </script>";

                TempData["notification"] = msg;

                Pessoa pessoa = PessoaService.ObterPorId(model.IdPessoa.ToString());
                return View(new PessoaViewModel(pessoa));

            }
        }

        private void UploadFilesHandler(PessoaViewModel viewModel, int id)
        {
            var targetDirectory = string.Format("~/App_Data/Documents/{0}", id);
            if (!Directory.Exists(Server.MapPath(targetDirectory)))
                Directory.CreateDirectory(Server.MapPath(targetDirectory));

            foreach (PropertyInfo propertyInfo in viewModel.GetType().GetProperties())
            {
                if (propertyInfo.PropertyType == typeof(HttpPostedFileBase) && propertyInfo.Name != "Foto")
                {
                    var propertyValue = propertyInfo.GetValue(viewModel);
                    if (propertyValue == null)
                        continue;

                    var postedFileName = propertyValue.GetType().GetProperty("FileName").GetValue(propertyValue);
                    
                    //To Get File Extension  
                    string fileExtension = Path.GetExtension(postedFileName.ToString());
                    //Add Current Date To Attached File Name  
                    string fileName = string.Format("{0}-{1}{2}", DateTime.Now.ToString("yyyyMMddHHmmss"), propertyInfo.Name, fileExtension);
                    //Its Create complete path to store in server.  
                    string uploadPath = string.Format("{0}/{1}", targetDirectory, fileName);
                    //To copy and save file into server.
                    propertyInfo.PropertyType.GetMethod("SaveAs").Invoke(propertyValue, new object[] { Server.MapPath(uploadPath) });

                    viewModel.GetType().GetProperty(propertyInfo.Name + "Path").SetValue(viewModel, uploadPath);
                }
            }
        }

        // GET: Pessoa/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = PessoaService.ObterPorId(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return PartialView(new PessoaViewModel(pessoa));
        }

        // POST: Pessoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string IdPessoa)
        {
            try
            {
                Pessoa pessoa = PessoaService.ObterPorId(IdPessoa);
                PessoaService.ExcluirPessoa(pessoa);

                return Json(new
                {
                    success = true,
                    title = "Sucesso",
                    message = "Pessoa excluída com sucesso !"
                }, JsonRequestBehavior.AllowGet);
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

        public ActionResult ObterPessoaPorCPF(string cpf)
        {
            //Here I'll bind the list of cities corresponding to selected state's state id  

            Pessoa pessoa = this.PessoaService.ObterPorCPF(cpf);
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string result = javaScriptSerializer.Serialize(pessoa);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CriarNovoCST(string id)
        {
            CursoSemTurmaViewModel cst = new CursoSemTurmaViewModel();
            cst.PessoaId = Convert.ToInt32(id);
            cst.Cursos = this.CursoService.Listar().ToList();
            return PartialView("../CursoSemTurma/Create", cst);
        }

        // POST: Pessoa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CriarNovoCST(CursoSemTurmaViewModel cst)
        {
            try
            {
                CursoSemTurmaService.Incluir(cst.MapearParaObjetoDeDominio());
                return Json(new { success = true, title = "Sucesso", message = "Curso cadastrado com sucesso !" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, title = "Erro", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ExcluirCursoSemTurma(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CursoSemTurma cst = CursoSemTurmaService.ObterPorId(Convert.ToInt32(id));
            if (cst == null)
            {
                return HttpNotFound();
            }
            return PartialView("../CursoSemTurma/Delete", cst);
        }

        [HttpPost, ActionName("ExcluirCursoSemTurma")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirCursoSemTurmaConfirmacao(string IdCursoSemTurma)
        {
            try
            {
                CursoSemTurma cst = CursoSemTurmaService.ObterPorId(Convert.ToInt32(IdCursoSemTurma));
                CursoSemTurmaService.Excluir(cst);

                return Json(new { success = true, title = "Sucesso", message = "Curso excluído com sucesso !" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, title = "Erro", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EditarCursoSemTurma(string id)
        {
            CursoSemTurma cst = this.CursoSemTurmaService.ObterPorId(Convert.ToInt32(id));
            CursoSemTurmaViewModel model = new CursoSemTurmaViewModel(cst);
            model.Cursos = this.CursoService.Listar().ToList();
            return PartialView("../CursoSemTurma/Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarCursoSemTurma(CursoSemTurmaViewModel cst)
        {
            try
            {
                CursoSemTurmaService.Atualizar(cst.MapearParaObjetoDeDominio());
                return Json(new { success = true, title = "Sucesso", message = "Registro Atualizado com sucesso !" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, title = "Erro", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}
