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

        [OperationContract]
        int CambiarEstadoDeLibro(string isbn, string estado);

        [OperationContract]
        int EditarDetallesDeLibro(string isbn, LibroBinding libro);

        [OperationContract]
        int RegistrarNuevoAutor(string autor);
        
        [OperationContract]
        int RegistrarNuevaEditorial(string editorial);

        [OperationContract]
        int ValidarDisponibilidadPorIdLibro(int idLibro);

        [OperationContract]
        string ObtenerTituloPorIdLibro(int idLibro);

        [OperationContract]
        string GuardarImagenLibro(string tituloLibro, byte[] imagenLibro, string extension);
        [OperationContract]
        byte[] ObtenerImagenLibro(string tituloLibro);
    }

    [DataContract]
    public class LibroBinding
    {
        [DataMember]
        public int idLibro { get; set; }
        [DataMember]
        public string Titulo { get; set; }

        [DataMember]
        public string Isbn { get; set; }

        [DataMember]
        public AutorBinding autor { get; set; }

        [DataMember]
        public EditorialBinding editorial { get; set; }

        [DataMember]
        public GeneroBinding genero { get; set; }

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
        [DataMember]
        public byte[] imagenLibro { get; set; }
        [DataMember]
        public string Extension { get; set; }
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
