using System;
using System.Collections.Generic;
using DocuTrack.Model;

namespace DocuTrack.View
{
    /// <summary>
    /// Capa de Presentación (Vista): Encargada de formatear y desplegar mensajes,
    /// gráficos ASCII del árbol BST y tablas de resultados en la consola.
    /// No contiene lógica de negocio ni manipulación directa de la estructura del árbol.
    /// </summary>
    public class DocuTrackView
    {
        public void MostrarEncabezado(string titulo)
        {
            Console.WriteLine();
            Console.WriteLine("==========================================================");
            Console.WriteLine($"  {titulo.ToUpper()}");
            Console.WriteLine("==========================================================");
        }

        public void MostrarMensaje(string mensaje)
        {
            Console.WriteLine($"[LOG] {mensaje}");
        }

        public void MostrarResultadoBusqueda(string nombre, bool hallado, int comparaciones)
        {
            string estado = hallado ? "HALLADO [OK]" : "NO HALLADO [X]";
            Console.WriteLine($"  • Objeto '{nombre}': {estado} | Comparaciones realizadas: {comparaciones}");
        }

        /// <summary>
        /// Muestra la representación jerárquica ASCII del árbol BST utilizando conectores gráficos.
        /// </summary>
        public void MostrarImpresionArbol(Nodo? nodo, string prefijo = "", bool esUltimo = true)
        {
            if (nodo == null) return;

            string tipo = nodo.EsCarpeta ? "[C]" : "[A]";
            Console.WriteLine($"{prefijo}{(esUltimo ? "└── " : "├── ")}{nodo.Nombre} {tipo}");

            string nuevoPrefijo = prefijo + (esUltimo ? "    " : "│   ");

            if (nodo.Izquierdo != null && nodo.Derecho != null)
            {
                MostrarImpresionArbol(nodo.Izquierdo, nuevoPrefijo, false);
                MostrarImpresionArbol(nodo.Derecho, nuevoPrefijo, true);
            }
            else if (nodo.Izquierdo != null)
            {
                MostrarImpresionArbol(nodo.Izquierdo, nuevoPrefijo, true);
            }
            else if (nodo.Derecho != null)
            {
                MostrarImpresionArbol(nodo.Derecho, nuevoPrefijo, true);
            }
        }

        public void MostrarRecorridos(List<string> pre, List<string> inord, List<string> post, List<string> niv)
        {
            MostrarEncabezado("RECORRIDOS DE VERIFICACIÓN");
            Console.WriteLine($"• Preorden:    {string.Join(" -> ", pre)}");
            Console.WriteLine($"• Inorden:     {string.Join(" -> ", inord)}");
            Console.WriteLine($"• Postorden:   {string.Join(" -> ", post)}");
            Console.WriteLine($"• Por Niveles: {string.Join(" -> ", niv)}");
        }

        public void MostrarMetricas(int altura)
        {
            MostrarEncabezado("MÉTRICAS Y ALTURA DEL ÁRBOL");
            Console.WriteLine($"• Altura actual del BST: {altura} niveles.");
        }
    }
}
