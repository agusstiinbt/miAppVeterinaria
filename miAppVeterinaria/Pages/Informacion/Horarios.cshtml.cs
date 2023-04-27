using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using miAppVeterinaria.Model;
using miAppVeterinaria.Servicios;

namespace miAppVeterinaria.Pages.Informacion


{
    public class HorariosModel : PageModel
    {
        [BindProperty] public List<DiasLaboralesVeterinarios> listaDiasLaboralesVet { get; set; }
        [BindProperty] public List<DiasLaboralesSecretarias> listaDiasLaboralesSec { get; set; }


        //SERVICIOS
        private IdiasLaboralesServiceVeterinario diasLaboralesServiceVet { get; set; }
        private IdiasLaboralesServiceSecretaria diasLaboralesServiceSec { get; set; }


        public HorariosModel(IdiasLaboralesServiceVeterinario _diaslaboralesService,IdiasLaboralesServiceSecretaria _diaslaboralesServiceSec)
        {
            this.diasLaboralesServiceVet= _diaslaboralesService;
            this.diasLaboralesServiceSec= _diaslaboralesServiceSec;
        }
        public void OnGet()
        {
            listaDiasLaboralesVet = diasLaboralesServiceVet.MostrarTodos().ToList();
            listaDiasLaboralesSec = diasLaboralesServiceSec.MostrarTodos().ToList();
        }

      
    }
}
