using miAppVeterinaria.Model;
using miAppVeterinaria.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace miAppVeterinaria.Pages.Consultorios
{
    public class BorrarModel : PageModel
    {

        //Variables de clase públicas
        [BindProperty] public Consultorio Consultorio { get; set; }

        //Servicios
        private IconsultorioService consultorioService { get; set; }
        private IveterinarioService vetService { get; set; }
        private IsecretariaService secService { get; set; }

        //Constructores
        public void OnGet(int ConsultorioId)
        {
            Consultorio = consultorioService.MostrarTodos().Where(x => x.IdConsultorio == ConsultorioId).First();
        }
        public BorrarModel(IveterinarioService _veterinarioService, IconsultorioService _consultorioService, IsecretariaService _secVec)
        {
            this.consultorioService = _consultorioService;
            this.vetService = _veterinarioService;
            this.secService = _secVec; ;
        }
        public async Task<IActionResult> OnPostAsync(int Id)
        {

            if (consultorioService.MostrarTodos().Count>1)
            {
                //nos guardamos el 1er consultorio disponible para que no queden sin consultorio los veterinarios y secretarios
                var consultorioDisponible = consultorioService.MostrarTodos().Where(x => x.IdConsultorio != Id).First();

                //Primero cambiamos el consultorio para los veterinarios con ese consultorio porque no puede quedar vacío por tener una foreign Key
                var listaVeterinarios = vetService.MostrarTodos().Where(x => x.consultorio == Id).ToList();
                foreach (Veterinario veterinario in listaVeterinarios)
                {
                    veterinario.consultorio = consultorioDisponible.IdConsultorio;
                    await vetService.Modificar(veterinario);
                }

                //Segundo cambiamos el consultorio para las secretariasVeterinarios con ese consultorio porque no puede quedar vacío por tener una foreign Key
                var listaSecretarias = secService.MostrarTodos().Where(x => x.Consultorio == Id).ToList();
                foreach (Secretaria secretaria in listaSecretarias)
                {
                    secretaria.Consultorio = consultorioDisponible.IdConsultorio;
                    await secService.Modificar(secretaria);
                }

                //Finalmente eliminamos el consultorio
                var consultorio = consultorioService.MostrarTodos().Where(x => x.IdConsultorio == Id).First();
             await consultorioService.Eliminar(consultorio);
            }

            return RedirectToPage("/Consultorios/Listado");
        }
    }
}
