using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Specialized;

namespace miAppVeterinaria.Pages.Maps
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
        public string clave { get; set; }
        
        public void OnPost(string Direccion)
        {

        }
    }
}
