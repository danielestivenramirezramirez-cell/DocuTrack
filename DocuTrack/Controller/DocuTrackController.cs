using System.Collections.Generic;
using DocuTrack.Model;
using DocuTrack.View;

namespace DocuTrack.Controller
{
    /// <summary>
    /// Capa de Control (Controlador): Coordina el flujo de datos entre el Modelo y la Vista.
    /// Ejecuta de forma secuencial y automatizada todos los casos de prueba requeridos por el taller.
    /// </summary>
    public class DocuTrackController
    {
        private readonly ArbolBinario _arbol;
        private readonly DocuTrackView _vista;

        public DocuTrackController()
        {
            _arbol = new ArbolBinario();
            _vista = new DocuTrackView();
        }

        public void Ejecutar()
        {
            // 1. CONSTRUCCIÓN DEL ÁRBOL
            _vista.MostrarEncabezado("1. Construcción Inicial del BST (14 Nodos Mínimos)");
            var datosPrueba = new List<(string nombre, bool esCarpeta)>
            {
                ("Documentos", true),
                ("Archivos", true),
                ("Proyectos", true),
                ("Anexos", true),
                ("Contratos", true),
                ("Finanzas", true),
                ("Reportes", true),
                ("Actas.docx", false),
                ("Bitacora.docx", false),
                ("Facturas.pdf", false),
                ("Guia.pdf", false),
                ("Manual.pdf", false),
                ("Plan.xlsx", false),
                ("Ventas.xlsx", false)
            };

            foreach (var (nombre, esCarpeta) in datosPrueba)
            {
                _arbol.Insertar(nombre, esCarpeta, out int comps, out string msg);
                _vista.MostrarMensaje($"{msg} (Comparaciones: {comps})");
            }

            // Prueba de intento de duplicado
            _vista.MostrarEncabezado("1.1 Prueba de Control de Duplicados");
            _arbol.Insertar("Proyectos", true, out int compsDup, out string msgDup);
            _vista.MostrarMensaje(msgDup);

            _vista.MostrarEncabezado("Estructura Inicial del Árbol (Diagrama ASCII)");
            _vista.MostrarImpresionArbol(_arbol.Raiz);

            // 2. BÚSQUEDAS RÁPIDAS
            _vista.MostrarEncabezado("2. Búsquedas Rápidas (6 Búsquedas: 2 Izq, 2 Der, 2 Inexistentes)");
            var busquedas = new List<string>
            {
                "Actas.docx", "Contratos",           // Subárbol Izquierdo
                "Facturas.pdf", "Ventas.xlsx",        // Subárbol Derecho
                "Inexistente1.pdf", "Inexistente2.docx" // Inexistentes
            };

            foreach (var nombre in busquedas)
            {
                var (nodo, comparaciones) = _arbol.Buscar(nombre);
                _vista.MostrarResultadoBusqueda(nombre, nodo != null, comparaciones);
            }

            // 3. ACTUALIZACIONES SELECTIVAS
            _vista.MostrarEncabezado("3. Actualizaciones Selectivas (Eliminación + Reinserción)");
            
            var (ok1, log1) = _arbol.Actualizar("Actas.docx", "Actas_v2.docx", false);
            _vista.MostrarMensaje(log1);
            _vista.MostrarImpresionArbol(_arbol.Raiz);

            var (ok2, log2) = _arbol.Actualizar("Anexos", "Anexos_Nuevos", true);
            _vista.MostrarMensaje(log2);
            _vista.MostrarImpresionArbol(_arbol.Raiz);

            var (ok3, log3) = _arbol.Actualizar("Documentos", "Documentos_General", true);
            _vista.MostrarMensaje(log3);
            _vista.MostrarImpresionArbol(_arbol.Raiz);

            // 4. ELIMINACIONES SELECTIVAS
            _vista.MostrarEncabezado("4. Eliminaciones Selectivas (Hoja, 1 Hijo, Raíz)");

            string caso1 = _arbol.Eliminar("Bitacora.docx");
            _vista.MostrarMensaje($"Eliminar Hoja ('Bitacora.docx'): {caso1}");
            _vista.MostrarImpresionArbol(_arbol.Raiz);

            string caso2 = _arbol.Eliminar("Contratos");
            _vista.MostrarMensaje($"Eliminar 1 Hijo ('Contratos'): {caso2}");
            _vista.MostrarImpresionArbol(_arbol.Raiz);

            string caso3 = _arbol.Eliminar("Documentos_General");
            _vista.MostrarMensaje($"Eliminar Raíz ('Documentos_General'): {caso3}");
            _vista.MostrarImpresionArbol(_arbol.Raiz);

            // 5. RECORRIDOS Y MÉTRICAS FINALES
            _vista.MostrarRecorridos(
                _arbol.Preorden(_arbol.Raiz),
                _arbol.Inorden(_arbol.Raiz),
                _arbol.Postorden(_arbol.Raiz),
                _arbol.PorNiveles()
            );

            _vista.MostrarMetricas(_arbol.ObtenerAltura(_arbol.Raiz));
        }
    }
}
