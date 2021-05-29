using Cortes.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cortes.Web.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index(string Id)
        {
            if (string.IsNullOrEmpty(Id))
                return RedirectToAction("Login", "Home");

            TempData["Id"] = Id;
            return View("Index");
        }

    }
}
