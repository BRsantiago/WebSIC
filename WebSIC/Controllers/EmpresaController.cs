using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Entity.Entities;
using Repository.Context;
using Service.Interface;
using WebSIC.Models;

namespace WebSIC.Controllers
{
    [Authorize(Roles ="CASSA\\GS_COMERCIAL")]
    public class EmpresaController : Controller
    {
        public IEmpresaService EmpresaService;
        public ITipoEmpresaService TipoEmpresaService;
        public IAeroportoService AeroportoService;
        public IPessoaService PessoaService;
        public IContratoService ContratoService;

        public EmpresaController(IEmpresaService _EmpresaService,
                                    ITipoEmpresaService _TipoEmpresaService,
                                        IAeroportoService _AeroportoService,
                                            IPessoaService _PessoaService,
                                                IContratoService _ContratoService)
        {
            EmpresaService = _EmpresaService;
            TipoEmpresaService = _TipoEmpresaService;
            AeroportoService = _AeroportoService;
            PessoaService = _PessoaService;
            ContratoService = _ContratoService;
        }

        // GET: Empresa
        public ActionResult Index()
        {
            List<Empresa> empresas = new List<Empresa>(); //EmpresaService.ObterTodos();

            return View(empresas);
        }

        //GET with Paging
        public JsonResult GetPagination(DataTableAjaxPostModel model)
        {
            int filteredResultsCount = 0;
            int totalResultsCount = 0;

            var result = CustomSearchFunc(model, out filteredResultsCount, out totalResultsCount);

            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
            });
        }

        public IList<Empresa> CustomSearchFunc(DataTableAjaxPostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            var searchBy = (model.search != null) ? model.search.value : null;

            var take = model.length;
            var skip = model.start;

            string sortBy = "";
            bool sortDir = false;

            if (model.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = model.columns[model.order[0].column].data;
                sortDir = model.order[0].dir.ToLower() != "asc";
            }

            // search the dbase taking into consideration table sorting and paging
            var result = EmpresaService.GetDataList(searchBy, take, skip, sortBy, sortDir, out filteredResultsCount, out totalResultsCount);
            if (result == null)
                // empty collection...
                return new List<Empresa>();

            return result;
        }

        // GET: Empresa/Details/5
        public ActionResult Details(int? id)
        {
            Empresa empresa = EmpresaService.ObterPorId(id.Value);
            return PartialView(empresa);
        }

        // GET: Empresa/Create
        public ActionResult Create()
        {
            EmpresaViewModel model = new EmpresaViewModel();

            model.Aeroportos = AeroportoService.ObterTodos();
            model.TiposEmpresa = TipoEmpresaService.ObterTodos();
            model.Representantes = new List<Pessoa>();

            return View(model);
        }

        // POST: Empresa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(EmpresaViewModel model)
        {
            Empresa novaEmpresa = new Empresa();
            if (ModelState.IsValid)
            {
                TipoEmpresa tipoEmpresa = TipoEmpresaService.ObterPorId(model.IdTipoEmpresa);
                Aeroporto aeroporto = AeroportoService.ObterPorId(model.IdAeroporto);
                //List<Aeroporto> aeroportos = new List<Aeroporto>();
                //aeroportos.Add(aeroporto);

                novaEmpresa.RazaoSocial = model.RazaoSocial;
                novaEmpresa.NomeFantasia = model.NomeFantasia;
                novaEmpresa.Endereco = model.Endereco;
                novaEmpresa.Complemento = model.Complemento;
                novaEmpresa.Numero = model.Numero;
                novaEmpresa.Bairro = model.Bairro;
                novaEmpresa.Cidade = model.Cidade;
                novaEmpresa.UF = model.UF;
                novaEmpresa.CGC = model.CGC;
                novaEmpresa.Telefone = model.Telefone;
                novaEmpresa.TipoCobranca = model.TipoCobranca;
                novaEmpresa.Observacao = model.Observacao;
                novaEmpresa.CEP = model.CEP;
                novaEmpresa.Email = model.Email;
                novaEmpresa.TipoEmpresa = tipoEmpresa;
                novaEmpresa.Aeroporto = aeroporto;
                novaEmpresa.TipoCobranca = model.TipoCobranca;

                if (model.Logotipo != null && model.Logotipo.ContentLength > 0)
                {
                    var uploadDir = "/Images/Logo";
                    var imagePath = Server.MapPath(uploadDir) + "/" + model.Logotipo.FileName;//Path.Combine(Server.MapPath(uploadDir), model.Logotipo.FileName);
                    var imageUrl = Path.Combine(uploadDir, model.Logotipo.FileName);
                    model.Logotipo.SaveAs(imagePath);
                    novaEmpresa.ImageUrl = imageUrl;
                }


                EmpresaService.IncluirNovaEmpresa(novaEmpresa);

                return RedirectToAction("Edit", new { id = novaEmpresa.IdEmpresa });
            }

            model.Aeroportos = AeroportoService.ObterTodos();
            model.TiposEmpresa = TipoEmpresaService.ObterTodos();

            model.IdEmpresa = novaEmpresa.IdEmpresa;
            model.RazaoSocial = novaEmpresa.RazaoSocial;
            model.NomeFantasia = novaEmpresa.NomeFantasia;
            model.Endereco = novaEmpresa.Endereco;
            model.Complemento = novaEmpresa.Complemento;
            model.Numero = novaEmpresa.Numero;
            model.Bairro = novaEmpresa.Bairro;
            model.Cidade = novaEmpresa.Cidade;
            model.UF = novaEmpresa.UF;
            model.CGC = novaEmpresa.CGC;
            model.Telefone = novaEmpresa.Telefone;
            model.TipoCobranca = novaEmpresa.TipoCobranca;
            model.Observacao = novaEmpresa.Observacao;
            model.CEP = novaEmpresa.CEP;
            model.Email = novaEmpresa.Email;
            model.IdTipoEmpresa = novaEmpresa.TipoEmpresa.IdTipoEmpresa;
            model.IdAeroporto = novaEmpresa.AeroportoId.Value;

            return PartialView(novaEmpresa);
        }

        // GET: Empresa/Edit/5
        public ActionResult Edit(int? id)
        {
            EmpresaViewModel model = new EmpresaViewModel();

            Empresa empresa = EmpresaService.ObterPorId(id.Value);

            model.Aeroportos = AeroportoService.ObterTodos();
            model.TiposEmpresa = TipoEmpresaService.ObterTodos();
            model.Representantes = PessoaService.ObterPorEmpresa(empresa.IdEmpresa);
            model.Contratos = ContratoService.ObterPorEmpresa(empresa.IdEmpresa).ToList();

            model.IdEmpresa = empresa.IdEmpresa;
            model.RazaoSocial = empresa.RazaoSocial;
            model.NomeFantasia = empresa.NomeFantasia;
            model.Endereco = empresa.Endereco;
            model.Complemento = empresa.Complemento;
            model.Numero = empresa.Numero;
            model.Bairro = empresa.Bairro;
            model.Cidade = empresa.Cidade;
            model.UF = empresa.UF;
            model.CGC = empresa.CGC;
            model.Telefone = empresa.Telefone;
            model.TipoCobranca = empresa.TipoCobranca;
            model.Observacao = empresa.Observacao;
            model.CEP = empresa.CEP;
            model.Email = empresa.Email;
            model.IdTipoEmpresa = empresa.TipoEmpresa.IdTipoEmpresa;
            model.IdAeroporto = empresa.AeroportoId.Value;
            model.TipoCobranca = empresa.TipoCobranca;

            model.ImageUrl = empresa.ImageUrl;

            return View(model);
        }

        // POST: Empresa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(EmpresaViewModel model)
        {
            try
            {
                //TipoEmpresa tipoEmpresa = TipoEmpresaService.ObterPorId(model.IdTipoEmpresa);
                //Aeroporto aeroporto = AeroportoService.ObterPorId(model.IdAeroporto);
                //List<Aeroporto> aeroportos = new List<Aeroporto>();
                //aeroportos.Add(aeroporto);
                Empresa empresa = EmpresaService.ObterPorId(model.IdEmpresa);

                empresa.RazaoSocial = model.RazaoSocial;
                empresa.NomeFantasia = model.NomeFantasia;
                empresa.Endereco = model.Endereco;
                empresa.Complemento = model.Complemento;
                empresa.Numero = model.Numero;
                empresa.Bairro = model.Bairro;
                empresa.Cidade = model.Cidade;
                empresa.UF = model.UF;
                empresa.CGC = model.CGC;
                empresa.Telefone = model.Telefone;
                empresa.TipoCobranca = model.TipoCobranca;
                empresa.Observacao = model.Observacao;
                empresa.CEP = model.CEP;
                empresa.Email = model.Email;
                empresa.TipoEmpresaId = model.IdTipoEmpresa;

                if (model.Logotipo != null && model.Logotipo.ContentLength > 0)
                {
                    var uploadDir = "/Images/Logo";
                    var fileName = model.Logotipo.FileName.Split('\\').Last();
                    var imagePath = Server.MapPath(uploadDir) + "/" + fileName; // model.Logotipo.FileName;Path.Combine(Server.MapPath(uploadDir), model.Logotipo.FileName);
                    var imageUrl = Path.Combine(uploadDir, fileName);
                    model.Logotipo.SaveAs(imagePath);
                    empresa.ImageUrl = imageUrl;
                }

                EmpresaService.AtualizarNovaEmpresa(empresa);

                var msg = "<script> swal({title: 'Good job!', text: 'Empresa atualizada com sucesso !', icon: 'success', button: 'OK!'}) </script>";

                TempData["notification"] = msg;

                return RedirectToAction("Index");
                //return Json(new { success = true, title = "Sucesso", message = "Empresa atualizada com sucesso !" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var msg = "<script> swal({title: 'Atenção!', text: 'Erro ao tentar atualizar a empresa, procure o TI.', icon: 'warning', button: 'OK!'}) </script>";

                TempData["notification"] = msg;

                //return Json(new { success = false, title = "Erro", message = ex.Message }, JsonRequestBehavior.AllowGet);

                return RedirectToAction("Edit", new { id = model.IdEmpresa.ToString() });
            }
        }

        // GET: Empresa/Delete/5
        public ActionResult Delete(int? id)
        {
            Empresa empresa = EmpresaService.ObterPorId(id.Value);
            return PartialView(empresa);
        }

        // POST: Empresa/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? IdEmpresa)
        {
            try
            {
                EmpresaService.ExcluirEmpresa(IdEmpresa.Value);
                return Json(new { success = true, title = "Sucesso", message = "Empresa excluída com sucesso !" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, title = "Erro", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
