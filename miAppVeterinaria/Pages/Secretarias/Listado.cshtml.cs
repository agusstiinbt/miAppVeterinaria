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

namespace miAppVeterinaria.Pages.Secretarias
{
    public class ListadoModel : PageModel
    {

        //Variables privadas de clase

        private readonly VeterinariaDBContext _context;

        //Variable públicas de clase
        [BindProperty] public Secretaria secretaria { get; set; }
        [BindProperty] public List<Secretaria> listaSecretarias { get; set; }
        [BindProperty] public List<Consultorio> Consultorio { get; set; }


        //Servicios 
        private IconsultorioService IconsultorioService { get; set; }
        private IsecretariaService isecretariaService { get; set; }
        private IdiasLaboralesServiceSecretaria diasSecService { get; set; }
        private IconsultorioService iconsultorioService { get; set; }

        //Métodos de clase
        private DiasLaboralesSecretarias setearDiaSec(int IdSecretaria, List<string> results)
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
        public string getConsultorio(int Id)
        {
            var consultorio = iconsultorioService.MostrarTodos().Where(x => x.IdConsultorio == Id).FirstOrDefault();
            if (consultorio != null)
            {
                return consultorio.IdConsultorio.ToString();
            }
            return "A definir";
        }

        //Constructor
        public ListadoModel(VeterinariaDBContext context, IconsultorioService _consultorioService, IdiasLaboralesServiceSecretaria _diasSecService, IconsultorioService _IconsultorioService, IsecretariaService _isecretariaService)
        {
            this.IconsultorioService = _IconsultorioService;
            this.isecretariaService = _isecretariaService;
            diasSecService = _diasSecService;
            iconsultorioService = _consultorioService;
            this._context = context;
        }

        //Métodos OnGet/OnPost
        public async Task OnGetAsync(string campoOrden, string nombreABuscar)
        {
            listaSecretarias = isecretariaService.MostrarTodos().ToList();
            Consultorio = IconsultorioService.MostrarTodos().ToList();

            if (!string.IsNullOrEmpty(nombreABuscar))
            {
                listaSecretarias = await _context.secretarias.FromSqlRaw($"GetSecretariasNombre {nombreABuscar}").ToListAsync();
            }
            else
            {
                if (!string.IsNullOrEmpty(campoOrden))
                {
                    switch (campoOrden)
                    {
                        case "Nombre":
                            listaSecretarias = listaSecretarias.OrderBy(x => x.Nombre).ToList();
                            break;
                        case "Apellido":
                            listaSecretarias = listaSecretarias.OrderBy(x => x.Apellido).ToList();
                            break;
                        case "Id":
                            listaSecretarias = listaSecretarias.OrderBy(x => x.IdSecretaria).ToList();
                            break;
                    }
                }
            }

        }
        public async Task OnPostBusqueda(string Nombre)
        {
            await OnGetAsync("", Nombre);
        }
        public async Task OnPostAsync(string consultorio, List<string> testSelect1)
        {
            var array = consultorio.Split(" ");
            secretaria.Consultorio = Convert.ToInt32(array[2]);
            var dias = setearDiaSec(secretaria.IdSecretaria, testSelect1);
            if (ModelState.IsValid)
            {
                isecretariaService.Agregar(this.secretaria);
                diasSecService.Agregar(dias);
            }
            await OnGetAsync("", "");
        }
    }
}
