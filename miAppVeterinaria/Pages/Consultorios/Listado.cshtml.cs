using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using miAppVeterinaria.Model;
using miAppVeterinaria.Servicios;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;

namespace miAppVeterinaria.Pages.Consultorios
{
    public class ListadoModel : PageModel
    {
        //atributos de clase
        [BindProperty] public List<Consultorio> Consultorio { get; set; }
        [BindProperty] public Consultorio consultorio { get; set; }

        //Servicios
        private IconsultorioService IconsultorioService { get; set; }
        
        //Constructor
        public ListadoModel(IconsultorioService _IconsultorioService)
        {
            this.IconsultorioService = _IconsultorioService;
        }
        public void OnGet()
        {
            Consultorio = IconsultorioService.MostrarTodos().ToList();
        }
        public IActionResult OnPost()
        {
            IconsultorioService.Agregar(consultorio);
            return RedirectToPage("/Consultorios/Listado");
        }
    }
}
