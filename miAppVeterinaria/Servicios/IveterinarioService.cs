using System.Collections.Generic;
using miAppVeterinaria.Model;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NuGet.Versioning;

namespace miAppVeterinaria.Servicios
{
    //Veterinario
    public interface IveterinarioService
    {
        List<Veterinario> MostrarTodos();
        void Agregar(Veterinario veterinario);
        Task Eliminar(Veterinario veterinario);
        Task Modificar(Veterinario veterinario);
    }
    public class VeterinarioService : IveterinarioService
    {
        private readonly VeterinariaDBContext _context;
        public VeterinarioService(VeterinariaDBContext context)
        {
            _context = context;
            //Código para cargar veterinarios en BBDD (EF)            
            //ListaCompletaProfes = new List<Profesor>()
            //                   {
            //                       new Profesor(){Id=101,FechaNacimiento=new DateTime(1986,02,02),Legajo=11111,ADistancia=true,Apellido="Esposito",Nombre="Agustin"}
            //                   };

            //VeterinarioList = new List<Veterinario>()
            //{

            //    new Veterinario(){Nombre="José",Apellido="Espósito",Legajo=465123,FechaNacimiento=new DateTime(1963,03,21),HorarioCompleto=true},
            //    new Veterinario(){Nombre="Agustín",Apellido="Espósito",Legajo=789123,FechaNacimiento=new DateTime(1997,02,26),HorarioCompleto=false},
            //    new Veterinario(){Nombre="Lautaro", Apellido="Scarcelli",Legajo=987657,FechaNacimiento= new DateTime(1996,10,10),HorarioCompleto=true},
            //    new Veterinario(){Nombre="Matias", Apellido="Alonzo",Legajo=324656,FechaNacimiento= new DateTime(1978,01,28),HorarioCompleto=true},
            //    new Veterinario(){Nombre="Juan", Apellido="Stirpalo",Legajo=543895,FechaNacimiento= new DateTime(1996,11,11),HorarioCompleto=true},
            //    new Veterinario(){Nombre="Ezequiel", Apellido="Martinez",Legajo=745621,FechaNacimiento= new DateTime(2000,10,12),HorarioCompleto=true},
            //    new Veterinario(){Nombre="Maria", Apellido="Scagnezi",Legajo=981235,FechaNacimiento= new DateTime(2004,12,01),HorarioCompleto=true}

            //};

            //people = new Dictionary<string, bool>();
            //people.Add("Lunes", true);
            //people.Add("Martes", false);
            //people.Add("Miercoles", false);
            //people.Add("Jueves", true);
            //people.Add("Viernes", false);
            //people.Add("Sabado", false);
            //people.Add("Domingo", false);//Código para cargar veterinarios a mano:
        }

        public void Agregar(Veterinario veterinario)
        {
            _context.veterinarios.Add(veterinario);
            _context.SaveChanges();//esto es como un commit
        }

        public async Task Eliminar(Veterinario veterinario)
        {
            _context.veterinarios.Remove(veterinario);
            await _context.SaveChangesAsync();
        }

        public async Task Modificar(Veterinario veterinario)
        {
            //var vetAnterior = VeterinarioList.Where(x => x.Legajo == veterinario.Legajo).First();
            //VeterinarioList.Remove(vetAnterior);
            //VeterinarioList.Add(veterinario);

            _context.veterinarios.Update(veterinario);
            await _context.SaveChangesAsync();//esto es como un commit
        }

        List<Veterinario> IveterinarioService.MostrarTodos()
        {
            return _context.veterinarios.ToList();
        }
    }
    //Secretaria
    public interface IsecretariaService
    {
        List<Secretaria> MostrarTodos();
        void Agregar(Secretaria secretaria);
        Task Eliminar(Secretaria secretaria);
        Task Modificar(Secretaria secretaria);
        Secretaria traerSecretaria(int Id);
        List<SecretariaVeterinarios> MostrarTodosSecVet();
    }
    public class SecretariaService : IsecretariaService
    {
        public List<Secretaria> SecretariaList { get; set; }
        private readonly VeterinariaDBContext _context;
        public SecretariaService(VeterinariaDBContext context)
        {
            _context = context;//Código para cargar Secretarias en BBDD (EF)            
        }
        public void Agregar(Secretaria Secretaria)
        {
            _context.secretarias.Add(Secretaria);
            _context.SaveChanges();//esto es como un commit
                                   //SecretariaList.Add(Secretaria);
        }
        public async Task Eliminar(Secretaria Secretaria)
        {
            _context.secretarias.Remove(Secretaria);
            await _context.SaveChangesAsync();//esto es como un commit
        }
        public async Task Modificar(Secretaria Secretaria)
        {
            _context.secretarias.Update(Secretaria);
            await _context.SaveChangesAsync();//esto es como un commit
        }
        public List<Secretaria> MostrarTodos()
        {
            return _context.secretarias.ToList();
        }
        public Secretaria traerSecretaria(int Id)
        {

            try
            {
                var secretaria = MostrarTodos().Where(x => x.IdSecretaria == Id).First();
                return secretaria;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public List<SecretariaVeterinarios> MostrarTodosSecVet()
        {
            return _context.secretariaVeterinarios.ToList();
        }
    }
    //Consultorio
    public interface IconsultorioService
    {
        List<Consultorio> MostrarTodos();
        void Agregar(Consultorio Consultorio);
        Task Eliminar(Consultorio Consultorio);
        Task Modificar(Consultorio Consultorio);
    }
    public class ConsultorioService : IconsultorioService
    {

        private readonly VeterinariaDBContext _context;
        public ConsultorioService(VeterinariaDBContext context)
        {
            _context = context;//Código para cargar Secretarias en BBDD (EF)            
        }

        public void Agregar(Consultorio consultorio)
        {
            _context.consultorios.Add(consultorio);
            _context.SaveChanges();//esto es como un commit
                                   //SecretariaList.Add(Secretaria);
        }

        public async Task Eliminar(Consultorio consultorio)
        {
            _context.consultorios.Remove(consultorio);
            await _context.SaveChangesAsync();//esto es como un commit
        }

        public async Task Modificar(Consultorio consultorio)
        {
            _context.consultorios.Update(consultorio);
            await _context.SaveChangesAsync();//esto es como un commit
        }

        List<Consultorio> IconsultorioService.MostrarTodos()
        {
            return _context.consultorios.ToList();
        }
    }
    //Dias Laborales Vet
    public interface IdiasLaboralesServiceVeterinario
    {
        List<DiasLaboralesVeterinarios> MostrarTodos();
        void Agregar(DiasLaboralesVeterinarios diaslaborales);
        void Modificar(DiasLaboralesVeterinarios dias);
        Task Eliminar(DiasLaboralesVeterinarios DiasLaborales);
        bool existeId(int Id);
    }
    public class DiasLaboralesServiceVeterinarios : IdiasLaboralesServiceVeterinario
    {
        private readonly VeterinariaDBContext _context;
        public DiasLaboralesServiceVeterinarios(VeterinariaDBContext context)
        {
            _context = context;//Código para cargar Secretarias en BBDD (EF)            
        }
        public void Agregar(DiasLaboralesVeterinarios diaslaborales)
        {
            _context.diasLaboralesVet.Add(diaslaborales);
            _context.SaveChanges();
        }

        public async Task Eliminar(DiasLaboralesVeterinarios DiasLaborales)
        {
            _context.diasLaboralesVet.Remove(DiasLaborales);
            await _context.SaveChangesAsync();
        }

        public void Modificar(DiasLaboralesVeterinarios dias)
        {
            _context.diasLaboralesVet.Update(dias);
            _context.SaveChanges();//esto es como un commit
        }

        public List<DiasLaboralesVeterinarios> MostrarTodos()
        {
            return _context.diasLaboralesVet.ToList();
        }
        public bool existeId(int Id)
        {
            var b = _context.diasLaboralesVet.AsNoTracking<DiasLaboralesVeterinarios>().Where(d => d.Id == Id);
            if (b.Count() == 0)
            {
                return false;
            }
            return true;
        }
    }
    //Dias Laborales Secretarias
    public interface IdiasLaboralesServiceSecretaria
    {
        List<DiasLaboralesSecretarias> MostrarTodos();
        void Agregar(DiasLaboralesSecretarias diaslaborales);
        void Modificar(DiasLaboralesSecretarias dias);
        Task Eliminar(DiasLaboralesSecretarias DiasLaborales);
        bool existeId(int Id);
    }
    public class DiasLaboralesServiceSecretarias : IdiasLaboralesServiceSecretaria
    {
        private readonly VeterinariaDBContext _context;
        public DiasLaboralesServiceSecretarias(VeterinariaDBContext context)
        {
            _context = context;
        }

        public void Agregar(DiasLaboralesSecretarias diaslaborales)
        {
            _context.diasLaboralesSec.Add(diaslaborales);
            _context.SaveChanges();
        }

        public async Task Eliminar(DiasLaboralesSecretarias DiasLaborales)
        {
            _context.diasLaboralesSec.Remove(DiasLaborales);
            await _context.SaveChangesAsync();
        }

        public void Modificar(DiasLaboralesSecretarias dias)
        {
            _context.diasLaboralesSec.Update(dias);
            _context.SaveChanges();//esto es como un commit
        }

        public List<DiasLaboralesSecretarias> MostrarTodos()
        {
            return _context.diasLaboralesSec.ToList();
        }

        public bool existeId(int Id)
        {
            var b = _context.diasLaboralesSec.AsNoTracking<DiasLaboralesSecretarias>().Where(d => d.Id == Id);
            if (b.Count() == 0)
            {
                return false;
            }
            return true;
        }
    }
    //SecretariasVeterinarios
    public interface ISecretariasVeterinarios
    {
        //TODO: debemos poner un servicio para eliminar o directamente si eliminamos un registro de veterinario, se debería borrar
        //después aca?
        List<SecretariaVeterinarios> MostrarTodos();
        bool existeRegistroVet(int IdVet);
        bool existeRegistroSec(int IdSec);

        void actualizarSecretaria(int idVeterinario, int IdSecretaria);
        void Modificar(SecretariaVeterinarios secVet);
        void Agregar(int veterinario, int secretaria);
        Task Eliminar(SecretariaVeterinarios secVec);
    }
    public class SecretariaVeterinariosService : ISecretariasVeterinarios
    {
        private readonly VeterinariaDBContext _context;
        public SecretariaVeterinariosService(VeterinariaDBContext context)
        {
            _context = context;
        }
        public void Agregar(int veterinario, int secretaria)
        {
            SecretariaVeterinarios secvec = new SecretariaVeterinarios();
            secvec.VeterinarioId = veterinario;
            secvec.SecretariaId = secretaria;
            _context.secretariaVeterinarios.Add(secvec);
            _context.SaveChanges();
        }
        //TODO: los metodos de modificar y get son muy parecidos y redundantes 
        public void Modificar(SecretariaVeterinarios secVet)
        {
            _context.secretariaVeterinarios.Update(secVet);
            _context.SaveChanges();//esto es como un commit
        }
        public void actualizarSecretaria(int idVeterinario, int IdSecretaria)
        {

            SecretariaVeterinarios registro = _context.secretariaVeterinarios.AsNoTracking<SecretariaVeterinarios>().Where(d => d.VeterinarioId == idVeterinario).First();
            registro.SecretariaId = IdSecretaria;
            try
            {
                Modificar(registro);
            }
            catch (Exception)
            {
                throw new Exception("Error al modificar registro");
            }


        }
        List<SecretariaVeterinarios> ISecretariasVeterinarios.MostrarTodos()
        {
            return _context.secretariaVeterinarios.ToList();
        }
        public async Task Eliminar(SecretariaVeterinarios secVec)
        {
            _context.secretariaVeterinarios.Remove(secVec);
            await _context.SaveChangesAsync();
        }

        public bool existeRegistroVet(int IdVet)
        {
            SecretariaVeterinarios? registro = null;
            try
            {
                registro = _context.secretariaVeterinarios.AsNoTracking<SecretariaVeterinarios>().Where(d => d.VeterinarioId == IdVet).First();
            }
            catch (Exception)
            {
            }
            if (registro != null) return true;
            return false;
        }
        public bool existeRegistroSec(int IdSec)
        {
            return true;
        }
    }
}

