# DocuTrack - Árbol Binario de Búsqueda (BST) de Documentos

## 📌 Descripción del Proyecto
DocuTrack es una aplicación de consola en **C# (.NET 8.0)** desarrollada bajo la arquitectura **MVC (Modelo-Vista-Controlador)**. Modela y gestiona una estructura jerárquica de archivos y carpetas mediante un **Árbol Binario de Búsqueda (BST)**.

---

## 👥 Integrantes del Equipo
* **Integrante 1**: [Nombre Completo] - *Desarrollador / Lógica del BST (Model)*
* **Integrante 2**: [Nombre Completo] - *Desarrollador / Interfaz y Diagrama ASCII (View)*
* **Integrante 3**: [Nombre Completo] - *Desarrollador / Secuenciación y Pruebas (Controller)*

---

## 🏗️ Arquitectura del Proyecto (MVC)

El código cumple estrictamente con el patrón de diseño MVC:

* **`Model/`**:
  * `Nodo.cs`: Modela los nodos (distinguiendo entre archivos y carpetas).
  * `ArbolBinario.cs`: Contiene toda la lógica del BST (inserción, búsquedas con conteo de comparaciones, actualización, eliminación en los 3 casos, recorridos y cálculo de altura). No contiene referencias a `Console`.
* **`View/`**:
  * `DocuTrackView.cs`: Gestiona exclusivamente la presentación en consola y la representación ASCII del árbol con conectores gráficos (`├──`, `└──`, `│`).
* **`Controller/`**:
  * `DocuTrackController.cs`: Orquesta los datos entre el modelo y la vista, ejecutando secuencialmente las pruebas del taller.
* **`Program.cs`**:
  * Punto de entrada principal. Únicamente instancia e invoca al controlador.

---

## ❓ Pregunta de Justificación Técnica

### **¿Qué recorrido confirma que el BST sigue ordenado y por qué?**

> **Respuesta:** El recorrido **Inorden (In-order)** es el único recorrido que visita los nodos en orden ascendente estricto (`Subárbol Izquierdo -> Nodo Actual -> Subárbol Derecho`). Dado que en un Árbol Binario de Búsqueda (BST) todos los elementos menores están a la izquierda y los mayores a la derecha, al procesar el árbol en Inorden se obtiene la secuencia lexicográfica (alfabética) perfectamente ordenada. Esto permite verificar objetivamente que la propiedad fundamental del BST se mantiene intacta tras cada operación CRUD (inserción, actualización y eliminación).

---

## 🚀 Instrucciones de Compilación y Ejecución

### Prerrequisitos
* Teniendo instalado **.NET SDK 8.0** o superior (o Visual Studio 2022 / VS Code con extensión C#).

### Ejecución desde Consola / Terminal
1. Abrir la consola en la carpeta raíz del proyecto (`DocuTrack`).
2. Ejecutar el siguiente comando para compilar y correr la aplicación:
   ```bash
   dotnet run
   ```

### Ejecución desde Visual Studio
1. Abrir la carpeta `DocuTrack` o el archivo de solución/proyecto `.csproj` en Visual Studio.
2. Presionar `F5` o hacer clic en el botón **Iniciar** (`DocuTrack`).

---

## 📊 Salida Esperada en Consola
La aplicación ejecutará de forma automatizada:
1. Construcción e inserción de 14 elementos mínimos (carpetas y archivos) con control de duplicados.
2. Diagramado en consola del árbol mediante conectores ASCII.
3. 6 Búsquedas rápidas registrando número exacto de comparaciones (incluyendo inexistentes).
4. Actualización selectiva de nodo hoja, nodo con 1 hijo y la raíz.
5. Eliminación selectiva en los 3 casos estándar del BST.
6. Recorridos de verificación (Preorden, Inorden, Postorden, Por Niveles) y métrica de altura final.
