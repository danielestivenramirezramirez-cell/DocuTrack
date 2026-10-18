using DocuTrack.Controller;

namespace DocuTrack
{
    /// <summary>
    /// Punto de entrada principal de la aplicación.
    /// Inicia únicamente el Controlador para mantener el estándar de arquitectura MVC.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            var controller = new DocuTrackController();
            controller.Ejecutar();
        }
    }
}
