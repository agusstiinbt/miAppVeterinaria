using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using miAppVeterinaria.Model;
using miAppVeterinaria.Servicios;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Cryptography;

namespace miAppVeterinaria.Pages.Veterinarios
{
    public class ModificarModel : PageModel
    {
        //variables privadas
        private readonly VeterinariaDBContext _context;

        //Variables públicas
        [BindProperty] public Veterinario veterinario { get; set; }
        [BindProperty] public List<Consultorio> Consultorio { get; set; }
        [BindProperty] public List<Secretaria> Secretarias { get; set; }

        //Servicios
        private IveterinarioService veterinarioService { get; set; }
        private IconsultorioService consultorioService { get; set; }
        private IdiasLaboralesServiceVeterinario diaslaboralesService { get; set; }
        private IsecretariaService secretariaService { get; set; }
        private ISecretariasVeterinarios secVecService { get; set; }

        //Métodos:
        private DiasLaboralesVeterinarios setearDia(int IdVeterinario, List<string> results)
        {
            DiasLaboralesVeterinarios dias = new DiasLaboralesVeterinarios();
            dias.Id = IdVeterinario;
            // dias.IdSecretaria = IdVeterinario;
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i] == "Lunes")
                {
                    dias.Lunes = true;
                }
                if (results[i] == "Martes")
                {
                    dias.Martes = true;
                }
                if (results[i] == "Miercoles")
                {
                    dias.Miercoles = true;
                }
                if (results[i] == "Jueves")
                {
                    dias.Jueves = true;
                }
                if (results[i] == "Viernes")
                {
                    dias.Viernes = true;
                }
                if (results[i] == "Sabado")
                {
                    dias.Sabado = true;
                }
                if (results[i] == "Domingo")
                {
                    dias.Domingo = true;
                }
            }
            return dias;
        }


        //Constructores
        public void OnGet(int vet)
        {
            Consultorio = consultorioService.MostrarTodos();
            veterinario = veterinarioService.MostrarTodos().Where(x => x.IdVeterinario == vet).First();
            Secretarias = secretariaService.MostrarTodos();
        }
        public ModificarModel(ISecretariasVeterinarios _secVecService, IsecretariaService _secretariaService, IdiasLaboralesServiceVeterinario _diaslaboralesService, IveterinarioService _veterinarioService, IconsultorioService _consultorioService, VeterinariaDBContext context)
        {
            _context = context;
            secretariaService = _secretariaService;
            diaslaboralesService = _diaslaboralesService;
            consultorioService = _consultorioService;
            veterinarioService = _veterinarioService;
            secVecService = _secVecService;
        }
        public IActionResult OnPost(string consultorio, List<string> diasLaborales, string secretaria)
        {
            var array = consultorio.Split(' ');
            string nroconsultorio = array[2];
            veterinario.consultorio = Convert.ToInt32(nroconsultorio);
            var dias = setearDia(veterinario.IdVeterinario, diasLaborales);
            int IdSecretaria = Convert.ToInt32(secretaria.Split(" ")[0]);
            if (ModelState.IsValid)
            {
                veterinarioService.Modificar(veterinario);
                bool a = diaslaboralesService.existeId(veterinario.IdVeterinario);

                if (a){diaslaboralesService.Modificar(dias);}
                else{diaslaboralesService.Agregar(dias);}

                if (string.IsNullOrEmpty(secretaria))
                {throw new Exception("Error al seleccionar secretaria");}

                if (secVecService.existeRegistroVet(veterinario.IdVeterinario))
                {secVecService.actualizarSecretaria(veterinario.IdVeterinario, IdSecretaria);}
                else
                { secVecService.Agregar(veterinario.IdVeterinario, IdSecretaria); }


            }
            return RedirectToPage("/Veterinarios/Listado");
        }
    }
}
