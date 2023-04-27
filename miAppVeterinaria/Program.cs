using miAppVeterinaria.Servicios;
using miAppVeterinaria.Model;
using Microsoft.Extensions.Options;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<IveterinarioService, VeterinarioService>();
builder.Services.AddTransient<IconsultorioService, ConsultorioService>();
builder.Services.AddTransient<IsecretariaService, SecretariaService>();
builder.Services.AddTransient<IdiasLaboralesServiceVeterinario, DiasLaboralesServiceVeterinarios>();
builder.Services.AddTransient<IdiasLaboralesServiceSecretaria,  DiasLaboralesServiceSecretarias>();
builder.Services.AddTransient<ISecretariasVeterinarios,SecretariaVeterinariosService>();


//Configuramos el servicio de Correo:

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddControllers();
builder.Services.AddTransient<IEmailSender, EmailSenderService>();

//Conexion con BBDD
builder.Services.AddDbContext<VeterinariaDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnectionString")));//Puedo tener distintos AppConnectionString que se dirigan a distintas conexiones.

//Ejecuta el programa
var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
