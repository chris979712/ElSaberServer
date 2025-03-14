using ElSaberDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ElSaberServices.Contratos
{
    [ServiceContract]
    public interface ILibroManejador
    {
        [OperationContract]
        int ObtenerIdLibroPorISBN(string isbn);

        [OperationContract]
        int ValidarExistenciaDeLibros();

        [OperationContract]
        int RegistrarNuevoLibro(LibroBinding libro);

        [OperationContract]
        int AumentarCantidadLibrosDisponiblesPorISBN(string isbn);

        [OperationContract]
        List<LibroBinding> ObtenerLibrosPorTitulo(string titulo);

        [OperationContract]
        List<LibroBinding> ObtenerLibrosPorISBN(string isbn);

        [OperationContract]
        List<LibroBinding> ObtenerLibrosPorIdAutor(int idAutor);

        [OperationContract]
        List<LibroBinding> ObtenerLibrosPorIdGenero(int idGenero);

        [OperationContract]
        List<GeneroBinding> ObtenerListaDeGeneros();

        [OperationContract]
        List<AutorBinding> ObtenerListaDeAutores();

        [OperationContract]
        List<EditorialBinding> ObtenerEditoriales();
    }

    [DataContract]
    public class LibroBinding
    {
        [DataMember]
        public string Titulo { get; set; }

        [DataMember]
        public string Isbn { get; set; }

        [DataMember]
        public int FK_IdAutor { get; set; }

        [DataMember]
        public int FK_IdEditorial { get; set; }

        [DataMember]
        public int FK_IdGenero { get; set; }

        [DataMember]
        public string AnioDePublicacion { get; set; }

        [DataMember]
        public string NumeroDePaginas { get; set; }

        [DataMember]
        public string RutaPortada { get; set; }

        [DataMember]
        public string Estado { get; set; }

        [DataMember]
        public int CantidadEjemplares { get; set; }

        [DataMember]
        public int CantidadEjemplaresPrestados { get; set; }
    }

    [DataContract]
    public class GeneroBinding 
    {
        [DataMember]
        public int IdGenero { get; set; }

        [DataMember]
        public string Genero { get; set; }

    }

    [DataContract]
    public class AutorBinding 
    {
        [DataMember]
        public int IdAutor { get; set; }

        [DataMember]
        public string Autor { get; set; }
    }

    [DataContract]
    public class EditorialBinding 
    {
        [DataMember]
        public int IdEditorial { get; set; }

        [DataMember]
        public string Editorial { get; set; }  
    }
}
