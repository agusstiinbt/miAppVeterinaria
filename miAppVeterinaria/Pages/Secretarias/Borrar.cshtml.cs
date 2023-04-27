using miAppVeterinaria.Model;
using miAppVeterinaria.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.Reflection.Metadata;

namespace miAppVeterinaria.Pages.Secretarias
{
    public class BorrarModel : PageModel
    {
        //Variables de clase
        [BindProperty] public Secretaria secretaria { get; set; }


        //servicios
        private IsecretariaService secretariaService { get; set; }
        private IdiasLaboralesServiceSecretaria diasLaborales { get; set; }
        private ISecretariasVeterinarios secVec { get; set; }

        //Constructores
        public BorrarModel(ISecretariasVeterinarios _secVec, IdiasLaboralesServiceSecretaria _diasLaborales, IsecretariaService secretariaService)
        {
            this.secVec = _secVec;
            this.secretariaService = secretariaService;
            this.diasLaborales = _diasLaborales;
        }
        public void OnGet(int IdSecretaria)
        {
            secretaria = secretariaService.MostrarTodos().Where(x => x.IdSecretaria == IdSecretaria).First();
        }
        public async Task<IActionResult> OnPostAsync(int Id)
        {
            //Primero eliminamos la secretaria de la tabla DiasLaborales

            var dia = diasLaborales.MostrarTodos().Where(d => d.Id == Id).FirstOrDefault();
            if (dia != null)
            {
                await diasLaborales.Eliminar(dia);
            }

            //Eliminamos el registro de secretariaVeterinarios
            var regis = secVec.MostrarTodos().Where(d => d.SecretariaId == Id).FirstOrDefault();
            if (regis != null)
            {
                await secVec.Eliminar(regis);
            }

            //Finalmente eliminamos a la secretaria de la tabla Secretarias
#pragma warning disable CS8601 // Posible asignación de referencia nula
            secretaria = secretariaService.MostrarTodos().Where(x => x.IdSecretaria == Id).FirstOrDefault();
#pragma warning restore CS8601 // Posible asignación de referencia nula
            if (secretaria != null) 
            {
                await secretariaService.Eliminar(secretaria);
            }
            return RedirectToPage("/Secretarias/Listado");
        }
    }
}
