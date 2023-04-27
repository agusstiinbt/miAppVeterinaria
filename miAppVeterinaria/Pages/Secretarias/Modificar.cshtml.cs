using miAppVeterinaria.Model;
using miAppVeterinaria.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel;
using System.Reflection.Metadata;

namespace miAppVeterinaria.Pages.Secretarias
{
    public class ModificarModel : PageModel
    {
        private readonly VeterinariaDBContext _context;
        [BindProperty] public List<Consultorio> Consultorio { get; set; }
        [BindProperty] public Secretaria secretaria { get; set; }
        private IsecretariaService secretariaService { get; set; }
        private IconsultorioService consultorioService { get; set; }
        private IdiasLaboralesServiceSecretaria diaslaboralesService { get; set; }

        private DiasLaboralesSecretarias setearDia(int IdSecretaria, List<string> results)
        {
            DiasLaboralesSecretarias dias = new DiasLaboralesSecretarias();
            dias.Id = IdSecretaria;
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
        public ModificarModel(VeterinariaDBContext context, IsecretariaService secretariaService, IconsultorioService consultorioService, IdiasLaboralesServiceSecretaria diaslaboralesService)
        {
            _context = context;
            this.secretariaService = secretariaService;
            this.consultorioService = consultorioService;
            this.diaslaboralesService = diaslaboralesService;
        }

        public void OnGet(int Id)
        {
            Consultorio = consultorioService.MostrarTodos();
            secretaria = secretariaService.MostrarTodos().Where(x => x.IdSecretaria == Id).First();
        }

        public IActionResult OnPost(string consultorio, List<string> diasLaborales)
        {
            var array = consultorio.Split(' ');
            string nroconsultorio = array[2];
            secretaria.Consultorio = Convert.ToInt32(nroconsultorio);
            var dias = setearDia(secretaria.IdSecretaria, diasLaborales);
            if (ModelState.IsValid)
            {
                secretariaService.Modificar(secretaria);
                bool a = diaslaboralesService.existeId(secretaria.IdSecretaria);
                if (a)
                {
                    diaslaboralesService.Modificar(dias);
                }
                else
                {
                    diaslaboralesService.Agregar(dias);
                }
            }
            return RedirectToPage("/Veterinarios/Listado");
        }
    }
}
