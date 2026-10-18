namespace DocuTrack.Model
{
    /// <summary>
    /// Modela un nodo individual dentro del Árbol Binario de Búsqueda (BST) de documentos.
    /// Distingue entre archivos (hojas) y carpetas (nodos internos).
    /// </summary>
    public class Nodo
    {
        public string Nombre { get; set; }
        public bool EsCarpeta { get; set; }
        public Nodo? Izquierdo { get; set; }
        public Nodo? Derecho { get; set; }

        public Nodo(string nombre, bool esCarpeta)
        {
            Nombre = nombre;
            EsCarpeta = esCarpeta;
            Izquierdo = null;
            Derecho = null;
        }
    }
}
