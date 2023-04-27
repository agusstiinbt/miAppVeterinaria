using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using miAppVeterinaria.Servicios;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Drawing;
using System.ComponentModel;

namespace miAppVeterinaria.Model
{
    public class Veterinario
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Debe poner un número de DNI")]
        public int IdVeterinario { get; set; }
        [Required(ErrorMessage = "Complete este campo con su nombre por favor")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Complete este campo con su apellido por favor")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "Debe seleccionar al menos un consultorio")]
        [ForeignKey("IdConsultorio")]
        public int consultorio { get; set; }
    }
    public class DiasLaboralesVeterinarios
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public bool Lunes { get; set; }
        public bool Martes { get; set; }
        public bool Miercoles { get; set; }
        public bool Jueves { get; set; }
        public bool Viernes { get; set; }
        public bool Sabado { get; set; }
        public bool Domingo { get; set; }
    }
    public class DiasLaboralesSecretarias
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public bool Lunes { get; set; }
        public bool Martes { get; set; }
        public bool Miercoles { get; set; }
        public bool Jueves { get; set; }
        public bool Viernes { get; set; }
        public bool Sabado { get; set; }
        public bool Domingo { get; set; }
    }
    public class Consultorio
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdConsultorio { get; set; }
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Complete este campo por favor")]
        public string Barrio { get; set; }
    }
    public class Cliente
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Consultorio consultorio { get; set; }
    }
    public class Secretaria
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdSecretaria { get; set; }
        [Required(ErrorMessage = "Complete este campo con su nombre por favor")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Complete este campo con su apellido por favor")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "Debe seleccionar al menos un consultorio")][ForeignKey("IdConsultorio")]
        public int Consultorio { get; set; }
    }
    public class SecretariaVeterinarios
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("IdVeterinario")]
        public int VeterinarioId { get; set; }
        [ForeignKey("IdSecretaria")]
        public int SecretariaId { get; set; }
    }
}