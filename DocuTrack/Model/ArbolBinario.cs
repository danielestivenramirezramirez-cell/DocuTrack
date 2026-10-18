using System;
using System.Collections.Generic;

namespace DocuTrack.Model
{
    /// <summary>
    /// Estructura de Datos principal: Árbol Binario de Búsqueda (BST).
    /// Implementa el CRUD de nodos, búsquedas con conteo de comparaciones,
    /// métricas de altura y recorridos (Preorden, Inorden, Postorden y Por Niveles).
    /// </summary>
    public class ArbolBinario
    {
        public Nodo? Raiz { get; private set; }

        /// <summary>
        /// Inserta un nuevo nodo en el BST garantizando el orden lexicográfico
        /// e impidiendo nombres duplicados (insensible a mayúsculas/minúsculas).
        /// </summary>
        public bool Insertar(string nombre, bool esCarpeta, out int comparaciones, out string mensaje)
        {
            comparaciones = 0;
            if (Raiz == null)
            {
                Raiz = new Nodo(nombre, esCarpeta);
                comparaciones = 1;
                mensaje = $"Raíz creada exitosamente: '{nombre}'";
                return true;
            }

            Nodo actual = Raiz;
            while (true)
            {
                comparaciones++;
                int cmp = string.Compare(nombre, actual.Nombre, StringComparison.OrdinalIgnoreCase);

                if (cmp == 0)
                {
                    mensaje = $"Rechazado: El elemento '{nombre}' ya existe en el árbol.";
                    return false;
                }
                else if (cmp < 0)
                {
                    if (actual.Izquierdo == null)
                    {
                        actual.Izquierdo = new Nodo(nombre, esCarpeta);
                        mensaje = $"Insertado '{nombre}' a la izquierda de '{actual.Nombre}'";
                        return true;
                    }
                    actual = actual.Izquierdo;
                }
                else
                {
                    if (actual.Derecho == null)
                    {
                        actual.Derecho = new Nodo(nombre, esCarpeta);
                        mensaje = $"Insertado '{nombre}' a la derecha de '{actual.Nombre}'";
                        return true;
                    }
                    actual = actual.Derecho;
                }
            }
        }

        /// <summary>
        /// Busca un nodo por su nombre y retorna la referencia y el número de comparaciones realizadas.
        /// </summary>
        public (Nodo? nodo, int comparaciones) Buscar(string nombre)
        {
            int comparaciones = 0;
            Nodo? actual = Raiz;

            while (actual != null)
            {
                comparaciones++;
                int cmp = string.Compare(nombre, actual.Nombre, StringComparison.OrdinalIgnoreCase);

                if (cmp == 0)
                    return (actual, comparaciones);
                else if (cmp < 0)
                    actual = actual.Izquierdo;
                else
                    actual = actual.Derecho;
            }

            return (null, comparaciones);
        }

        /// <summary>
        /// Actualiza un nodo reemplazando el original por uno nuevo.
        /// Requiere eliminar el nodo previo y reinsertar el nuevo para preservar la propiedad del BST.
        /// </summary>
        public (bool exito, string mensaje) Actualizar(string nombreAntiguo, string nombreNuevo, bool esCarpeta)
        {
            var (existente, _) = Buscar(nombreNuevo);
            if (existente != null)
                return (false, $"Error: El nuevo nombre '{nombreNuevo}' ya existe en el árbol.");

            var (origen, _) = Buscar(nombreAntiguo);
            if (origen == null)
                return (false, $"Error: No se encontró el nodo '{nombreAntiguo}' para actualizar.");

            string casoEliminacion = Eliminar(nombreAntiguo);
            Insertar(nombreNuevo, esCarpeta, out _, out _);
            return (true, $"Actualizado '{nombreAntiguo}' -> '{nombreNuevo}' ({casoEliminacion} + Reinserción).");
        }

        /// <summary>
        /// Elimina un nodo del BST contemplando los 3 casos:
        /// Caso 1: Hoja (0 hijos).
        /// Caso 2: Un solo hijo.
        /// Caso 3: Dos hijos (reemplazo por sucesor en inorden).
        /// </summary>
        public string Eliminar(string nombre)
        {
            string casoEjecutado = "No encontrado";
            Raiz = EliminarRec(Raiz, nombre, ref casoEjecutado);
            return casoEjecutado;
        }

        private Nodo? EliminarRec(Nodo? nodo, string nombre, ref string casoEjecutado)
        {
            if (nodo == null) return null;

            int cmp = string.Compare(nombre, nodo.Nombre, StringComparison.OrdinalIgnoreCase);

            if (cmp < 0)
            {
                nodo.Izquierdo = EliminarRec(nodo.Izquierdo, nombre, ref casoEjecutado);
            }
            else if (cmp > 0)
            {
                nodo.Derecho = EliminarRec(nodo.Derecho, nombre, ref casoEjecutado);
            }
            else
            {
                // Caso 1: Nodo Hoja
                if (nodo.Izquierdo == null && nodo.Derecho == null)
                {
                    casoEjecutado = "Caso 1 (Nodo Hoja eliminado)";
                    return null;
                }
                // Caso 2: Nodo con 1 hijo
                if (nodo.Izquierdo == null)
                {
                    casoEjecutado = "Caso 2 (Nodo con 1 hijo derecho eliminado)";
                    return nodo.Derecho;
                }
                if (nodo.Derecho == null)
                {
                    casoEjecutado = "Caso 2 (Nodo con 1 hijo izquierdo eliminado)";
                    return nodo.Izquierdo;
                }
                // Caso 3: Nodo con 2 hijos
                Nodo sucesor = ObtenerMinimo(nodo.Derecho);
                nodo.Nombre = sucesor.Nombre;
                nodo.EsCarpeta = sucesor.EsCarpeta;
                nodo.Derecho = EliminarRec(nodo.Derecho, sucesor.Nombre, ref casoEjecutado);
                casoEjecutado = "Caso 3 (Nodo con 2 hijos reemplazado por sucesor en inorden)";
            }
            return nodo;
        }

        private Nodo ObtenerMinimo(Nodo nodo)
        {
            Nodo actual = nodo;
            while (actual.Izquierdo != null)
                actual = actual.Izquierdo;
            return actual;
        }

        /// <summary>
        /// Obtiene la altura del árbol (longitud del camino más largo desde la raíz hasta una hoja).
        /// </summary>
        public int ObtenerAltura(Nodo? nodo)
        {
            if (nodo == null) return 0;
            return 1 + Math.Max(ObtenerAltura(nodo.Izquierdo), ObtenerAltura(nodo.Derecho));
        }

        // --- RECORRIDOS ---

        public List<string> Preorden(Nodo? nodo, List<string>? lista = null)
        {
            lista ??= new List<string>();
            if (nodo != null)
            {
                lista.Add(nodo.Nombre);
                Preorden(nodo.Izquierdo, lista);
                Preorden(nodo.Derecho, lista);
            }
            return lista;
        }

        public List<string> Inorden(Nodo? nodo, List<string>? lista = null)
        {
            lista ??= new List<string>();
            if (nodo != null)
            {
                Inorden(nodo.Izquierdo, lista);
                lista.Add(nodo.Nombre);
                Inorden(nodo.Derecho, lista);
            }
            return lista;
        }

        public List<string> Postorden(Nodo? nodo, List<string>? lista = null)
        {
            lista ??= new List<string>();
            if (nodo != null)
            {
                Postorden(nodo.Izquierdo, lista);
                Postorden(nodo.Derecho, lista);
                lista.Add(nodo.Nombre);
            }
            return lista;
        }

        public List<string> PorNiveles()
        {
            var resultado = new List<string>();
            if (Raiz == null) return resultado;

            var cola = new Queue<Nodo>();
            cola.Enqueue(Raiz);

            while (cola.Count > 0)
            {
                Nodo actual = cola.Dequeue();
                resultado.Add(actual.Nombre);

                if (actual.Izquierdo != null) cola.Enqueue(actual.Izquierdo);
                if (actual.Derecho != null) cola.Enqueue(actual.Derecho);
            }

            return resultado;
        }
    }
}
