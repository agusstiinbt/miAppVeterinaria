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
using System.Security.Policy;
using System.Xml.Linq;

namespace miAppVeterinaria.Pages.Veterinarios
{
    public class ListadoModel : PageModel
    {
        //Variable privadas de clase

        private readonly VeterinariaDBContext _context;
        private List<Secretaria> secretarias { get; set; }

        //Variable públicas de clase
        [BindProperty] public Veterinario veterinario { get; set; }
        [BindProperty] public List<SecretariaVeterinarios> secretariasVeterinarios { get; set; }
        [BindProperty] public List<Veterinario> veterinarios { get; set; }
        [BindProperty] public List<Consultorio> Consultorio { get; set; }
        [BindProperty] public List<SelectListItem> listaSecretarias { get; set; }

        //servicios
        private IsecretariaService secretariaService { get; set; }
        private IveterinarioService veterinarioService { get; set; }
        private IconsultorioService consultorioService { get; set; }
        private IdiasLaboralesServiceVeterinario diaslaboralesService { get; set; }
        private ISecretariasVeterinarios secVec { get; set; }

        //Métodos de clase
        private DiasLaboralesVeterinarios setearDia(int IdVeterinario, List<string> results)
        {
            DiasLaboralesVeterinarios dias = new DiasLaboralesVeterinarios();
            dias.Id = IdVeterinario;
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
        public string getSecretaria(int IdVeterinario)
        {
            foreach (SecretariaVeterinarios secretaria in secretariasVeterinarios)
            {
                if (secretaria.VeterinarioId == IdVeterinario)
                {
                    var secre = secretariaService.traerSecretaria(secretaria.SecretariaId);
                    if (secre != null)
                    {
                        return secre.Nombre + " " + secre.Apellido;
                    }
                }
            }
            return "A definir";
        }

        /// <summary>
        /// Cada veterinario tiene un Id de consultorio, si el consultorio ya no existe, entonces tendrá que definir un nuevo consultorio
        /// </summary>
        /// <param name="IdVeterinario"></param>
        /// <returns>'A definir' si el consultorio ya no existe, caso contrario el Id del mismo</returns>
        public string getConsultorio(int Id)
        {
            var consultorio = consultorioService.MostrarTodos().Where(x => x.IdConsultorio == Id).FirstOrDefault();
            if (consultorio != null)
            {
                return consultorio.IdConsultorio.ToString();
            }
            return "A definir";
        }

        //Constructor
        public ListadoModel(ISecretariasVeterinarios _secVec, IsecretariaService _secretariaService, IdiasLaboralesServiceVeterinario _diaslaboralesService, IveterinarioService _veterinarioService, IconsultorioService _consultorioService, VeterinariaDBContext context)
        {
            _context = context;
            secretariaService = _secretariaService;
            diaslaboralesService = _diaslaboralesService;
            consultorioService = _consultorioService;
            veterinarioService = _veterinarioService;
            this.secVec = _secVec;
            listaSecretarias = new List<SelectListItem>();
        }

        //Métodos OnGet/OnPost
        public async Task OnGetAsync(string campoOrden, string nombreABuscar)
        {
            Consultorio = consultorioService.MostrarTodos();
            secretariasVeterinarios = secretariaService.MostrarTodosSecVet();
            secretarias = secretariaService.MostrarTodos();
            veterinarios = veterinarioService.MostrarTodos();

            //Cargamos todas las secretarias en un elemento 'select value' en el caso de agregar un nuevo veterinario
            foreach (Secretaria secretaria in secretarias)
            {
                listaSecretarias.Add(new SelectListItem() { Text = "Id: " + secretaria.IdSecretaria + "| " + secretaria.Nombre + " " + secretaria.Apellido, Value = secretaria.IdSecretaria.ToString() });
            }

            if (!string.IsNullOrEmpty(nombreABuscar))
            {
                veterinarios = await _context.veterinarios.FromSqlRaw($"GetVeterinariosNombre {nombreABuscar}").ToListAsync();
            }
            else
            {
                //Ordenamos la lista veterinarios según apellido,nombre o Id
                if (!string.IsNullOrEmpty(campoOrden))
                {
                    switch (campoOrden)
                    {
                        case "Nombre":
                            veterinarios = veterinarios.OrderBy(x => x.Nombre).ToList();
                            break;
                        case "Apellido":
                            veterinarios = veterinarios.OrderBy(x => x.Apellido).ToList();
                            break;
                        case "Id":
                            veterinarios = veterinarios.OrderBy(x => x.IdVeterinario).ToList();
                            break;
                    }
                }
            }
        }

        public async Task OnPostBusqueda(string Nombre)
        {
           await OnGetAsync("", Nombre);
        }

        public async Task OnPostAsync(string consultorio, List<string> testSelect1, string secretaria)
        {
            var array = consultorio.Split(' ');
            string nroConsultorio = array[2];
            veterinario.consultorio = Convert.ToInt32(nroConsultorio);
            var dias = setearDia(veterinario.IdVeterinario, testSelect1);
            if (ModelState.IsValid)
            {
                veterinarioService.Agregar(veterinario);
                diaslaboralesService.Agregar(dias);
                secVec.Agregar(veterinario.IdVeterinario, Convert.ToInt32(secretaria));
            }
           await OnGetAsync("", "");
        }
    }
}