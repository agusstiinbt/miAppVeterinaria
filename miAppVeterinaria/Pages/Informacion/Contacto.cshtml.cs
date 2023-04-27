using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using Microsoft.AspNetCore.Hosting;
using System.Runtime.InteropServices;
using System.Net.Mail;
using System.Net;
using miAppVeterinaria.Model;
using miAppVeterinaria.Servicios;
using Microsoft.Extensions.Options;

namespace miAppVeterinaria.Pages.Informacion
{
    public class ContactoModel : PageModel
    {
        private IEmailSender _emailSenderService;
        public ContactoModel(IEmailSender emailSenderService)
        {
            _emailSenderService = emailSenderService;
        }
        public void OnGet()
        {

        }
        public void OnPost(string emailaddress)
        {

            var a = _emailSenderService.SendEmailAsync(emailaddress);
            //try
            //{
            //    string messageStatus= await _emailSender.SendEmailAsync(emailaddress);
            //    string a = messageStatus;
            //}
            //catch (Exception)
            //{

            //    throw;
            //}


            //Código simple para envíar correo que sí funciona
            //try
            //{
            //    using (MailMessage mailMessage = new MailMessage())
            //    {
            //        mailMessage.To.Add(emailaddress);

            //        mailMessage.Subject = "hola";

            //        mailMessage.Body = "<h1> se envio correctamente </h1>";

            //        mailMessage.IsBodyHtml = true;

            //        mailMessage.From = new MailAddress("agusstiinbt@gmail.com");
            //        using (SmtpClient cliente = new SmtpClient())
            //        {
            //            cliente.UseDefaultCredentials = false;
            //            cliente.Credentials = new NetworkCredential("agusstiinbt@gmail.com", "lwtgrvrssygzqlov");
            //            cliente.Port = 587;
            //            cliente.EnableSsl = true;

            //            cliente.Host = "smtp.gmail.com";
            //            cliente.Send(mailMessage);
            //        }
            //    }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}

        }

    }
}