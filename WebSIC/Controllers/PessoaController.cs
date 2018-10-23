using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Entity.DTO;
using Entity.Entities;
using Repository.Context;
using Service.Interface;

namespace WebSIC.Controllers
{
    public class PessoaController : Controller
    {
        public IPessoaService PessoaService;

        public PessoaController(IPessoaService _PessoaService)
        {
            PessoaService = _PessoaService;
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
            List<Genero> GeneroLista = new List<Genero>();
            GeneroLista.Add(new Genero() { IdGenero = 0, Descricao = "Masculino" });
            GeneroLista.Add(new Genero() { IdGenero = 1, Descricao = "Feminino" });

            return View();
        }

        // POST: Pessoa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPessoa,Nome,Apelido,DataNascimento,NomePai,NomeMae,Endereco,Numero,Complemento,Bairro,Cidade,UF,CEP,TelefoneEmergencia,TelefoneResidencial,TelefoneCelular,RNE,CPF,RG,OrgaoExpeditor,UFOrgaoExpeditor,Genero,Observacao,FlgCVE,Email,CNH,CategoriaCNH,DataValidadeCNH,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                PessoaService.IncluirPessoa(pessoa);
                return RedirectToAction("Edit", new { id = pessoa.IdPessoa });
            }
            return View(pessoa);
        }

        // GET: Pessoa/Edit/5
        public ActionResult Edit(string id)
        {
            Pessoa pessoa = PessoaService.ObterPorId(id);
            return View(pessoa);
        }

        // POST: Pessoa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPessoa,Nome,Apelido,DataNascimento,NomePai,NomeMae,Endereco,Numero,Complemento,Bairro,Cidade,UF,CEP,TelefoneEmergencia,TelefoneResidencial,TelefoneCelular,RNE,CPF,RG,OrgaoExpeditor,UFOrgaoExpeditor,Genero,Observacao,FlgCVE,Email,CNH,CategoriaCNH,DataValidadeCNH,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Pessoa pessoa)
        {
            try
            {
                PessoaService.Atualizar(pessoa);
                //return RedirectToAction("Index");
                var msg = "<script> swal({title: 'Good job!', text: 'Empresa atualizada com sucesso !', icon: 'success', button: 'OK!'}) </script>";

                TempData["notification"] = msg;

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                var msg = "<script> swal({title: 'Atenção!', text: '" + ex.Message + "', icon: 'warning', button: 'OK!'}) </script>";

                TempData["notification"] = msg;

                //return Json(new { success = false, title = "Erro", message = ex.Message }, JsonRequestBehavior.AllowGet);
                return PartialView(pessoa);
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
            return View(pessoa);
        }

        // POST: Pessoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Pessoa pessoa = PessoaService.ObterPorId(id);
            PessoaService.ExcluirPessoa(pessoa);
            return RedirectToAction("Index");
        }

        public ActionResult ObterPessoaPorCPF(string cpf)
        {
            //Here I'll bind the list of cities corresponding to selected state's state id  

            Pessoa pessoa = this.PessoaService.ObterPorCPF(cpf);
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string result = javaScriptSerializer.Serialize(pessoa);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
