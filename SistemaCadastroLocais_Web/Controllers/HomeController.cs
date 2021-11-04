using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SistemaCadastroLocais_Web.Funcoes;
using SistemaCadastroLocais_Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace SistemaCadastroLocais_Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private FuncaoTurismo functions = new FuncaoTurismo();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }      
      
        [HttpGet]
        public ActionResult Index()
        {

            return View(functions.getAll(""));
        }

        [HttpPost]
        public ActionResult Index(string texto)
        {

            return View(functions.getNome(texto));
        }


        [HttpGet]
        public ActionResult Cadastrar()
        {
            ViewBag.Companies = functions.getAll("");
            var ponto = new TB_PontosTuristicos();
            return View(ponto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(TB_PontosTuristicos model)
        {
            if (ModelState.IsValid)
            {

                if (functions.Add(model))
                    return RedirectToAction("Index");

            }
            return View();

        }

        [HttpGet]
        public ActionResult Atualizar(int? id)
        {

            var retorno = functions.findById(id.Value);

            if (retorno == null) {
                return RedirectToAction("Index");
            }
               
            return View(retorno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Atualizar(int? id, TB_PontosTuristicos model)
        {

            if (ModelState.IsValid) // se nao tiver nenhum erro no envio
            {

                //var person = new PersonIndexModelView();

                var retorno = functions.findById(id.Value);

                if (retorno == null)
                    return RedirectToAction("Index");

                if (functions.Edit(model))
                {

                    return RedirectToAction("Index");
                }

            }

            return View();
        }   
        
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (ModelState.IsValid) // se nao tiver nenhum erro no envio
            {

                var retorno = functions.findById(id.Value);

                if (retorno == null)
                    return RedirectToAction("Index");


                if (functions.Delete(id.Value))
                {
                    return RedirectToAction("Index");
                }

            }

            return View();
        }

    public IActionResult Sobre()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }


}
