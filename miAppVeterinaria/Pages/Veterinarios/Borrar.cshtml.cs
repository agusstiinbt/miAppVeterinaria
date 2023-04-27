using miAppVeterinaria.Model;
using miAppVeterinaria.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Options;
using Microsoft.Win32;
using System.ComponentModel;
using System.Reflection.Metadata;

namespace miAppVeterinaria.Pages.Veterinarios
{
    public class BorrarModel : PageModel
    {

        //Variables de clase
        [BindProperty] public Veterinario veterinario { get; set; }
        [BindProperty] public DiasLaboralesVeterinarios diasLaborales { get; set; }
        private readonly VeterinariaDBContext _context;

        //Servicios
        private IveterinarioService veterinarioService { get; set; }
        private IdiasLaboralesServiceVeterinario diaslaboralesService { get; set; }
        private ISecretariasVeterinarios secVec { get; set; }


        //Constructores
        public void OnGet(int vet)
        {
            veterinario = veterinarioService.MostrarTodos().Where(x => x.IdVeterinario == vet).First();
            try
            {
                diasLaborales = diaslaboralesService.MostrarTodos().Where(x => x.Id == vet).First();
            }
            catch (Exception)
            {
                //No contiene elementos porque se borro anteriormente
            }

        }
        public BorrarModel(ISecretariasVeterinarios _secVec, IdiasLaboralesServiceVeterinario _diaslaboralesService, IveterinarioService _veterinarioService, IconsultorioService _consultorioService, VeterinariaDBContext context)
        {
            _context = context;
            secVec = _secVec;
            diaslaboralesService = _diaslaboralesService;
            veterinarioService = _veterinarioService;
        }
        public async Task<IActionResult> OnPostAsync(int Id)
        {

            var dias= diaslaboralesService.MostrarTodos().Where(x => x.Id == Id).FirstOrDefault();
            if (dias != null)
            {
                await diaslaboralesService.Eliminar(dias);
            }

            var registro = secVec.MostrarTodos().Where(r => r.VeterinarioId == Id).FirstOrDefault();
            if (registro != null)
            {
                await secVec.Eliminar(registro);
            }

            var vet = veterinarioService.MostrarTodos().Where(x => x.IdVeterinario == Id).FirstOrDefault();
            if (vet != null)
            {
                await veterinarioService.Eliminar(vet);
            }
            
            return RedirectToPage("/Veterinarios/Listado");
        }
    }
}
