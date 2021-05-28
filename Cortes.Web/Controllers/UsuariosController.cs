using Cortes.Services.ViewModels;
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
        public IActionResult Index()
        {
            var model = JsonConvert.DeserializeObject<LoginViewModel>(TempData["jsonUsuario"].ToString());
            return View(model);
        }
    }
}
