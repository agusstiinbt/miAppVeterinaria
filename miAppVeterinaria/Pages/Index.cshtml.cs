using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using miAppVeterinaria.Model;
using miAppVeterinaria.Servicios;
using NuGet.Protocol.Core.Types;

namespace miVeterinaria.Pages
{
    public class IndexModel : PageModel
    {
        //Atributos de clase privados
        private readonly ILogger<IndexModel> _logger;
        private IconsultorioService _IConsultorioService;

        //Atributos de clase públicos
        [BindProperty]
        public List<Consultorio> listaConsultorios { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IconsultorioService iconsultorioService)
        {
            _logger = logger;
            _IConsultorioService= iconsultorioService;
        }

        public void OnGet()
        {
            listaConsultorios = _IConsultorioService.MostrarTodos();
        }
    }
}
