# Prueba-Tercnica-MVC
Prueba Técnica - Módulo de CRUD de Alumnos
Descripción
Este proyecto consiste en una aplicación web desarrollada para un módulo de alta, baja, consulta y actualización de alumnos (CRUD). Fue realizada como parte de una evaluación técnica, utilizando ASP.NET MVC y las herramientas especificadas.

Tecnologías Utilizadas
IDE: Visual Studio
Framework: .NET Framework 4.x
Lenguaje: C#
Tecnologías web:
ASP.NET MVC
Bootstrap
DevExpress
Base de datos: Microsoft SQL Server
API: CRUD de Alumnos desarrollado como una API en C#
Opcional: LINQ
Funcionalidades
CRUD de Alumnos:

Crear, leer, actualizar y eliminar registros de alumnos.
Los campos de la entidad Alumno incluyen:
Id: Identificador único del alumno.
Nombre: Nombre completo del alumno.
Matricula: Número único asignado al alumno.
Carrera: Carrera que cursa el alumno.
FechaNacimiento: Fecha de nacimiento del alumno.
Email: Correo electrónico del alumno.
API RESTful:

Una API en C# que proporciona endpoints para cada operación CRUD sobre los alumnos.
Endpoints:
GET /api/alumnos: Obtiene la lista de alumnos.
GET /api/alumnos/{id}: Obtiene un alumno específico por su ID.
POST /api/alumnos: Crea un nuevo alumno.
PUT /api/alumnos/{id}: Actualiza los datos de un alumno existente.
DELETE /api/alumnos/{id}: Elimina un alumno.
Interfaz de Usuario:

Diseño responsivo utilizando Bootstrap.
Controles avanzados con DevExpress para mejorar la experiencia del usuario.
Instalación
Requisitos previos:

Visual Studio 2019 o superior.
Microsoft SQL Server.
.NET Framework 4.x.
Configuración del proyecto:

Clonar o descomprimir el repositorio en tu máquina local.
Configurar la cadena de conexión en el archivo Web.config para que apunte a tu instancia de SQL Server.
xml
Copiar código
<connectionStrings>
  <add name="DefaultConnection" 
       connectionString="Server=TU_SERVIDOR;Database=Universidad;User Id=sa;Password=TU_CONTRASEÑA;" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
Base de datos:

Ejecutar el script SQL ubicado en la carpeta Database para crear la base de datos y las tablas necesarias.
Ejecutar el proyecto:

Abrir el proyecto en Visual Studio.
Compilar y ejecutar la solución.
Uso
Navegar al módulo principal de alumnos.
Realizar las operaciones CRUD desde la interfaz web o mediante los endpoints de la API.
Estructura del Proyecto
Models: Contiene las clases de dominio y modelos de datos.
Controllers: Controladores de la aplicación y de la API.
Views: Archivos Razor para la interfaz de usuario.
Scripts: Contiene los scripts necesarios para inicializar la base de datos.
API: Controlador y configuración para los endpoints RESTful.
Autor
Nombre: Juan Daniel Luevano Ruiz
Contacto: daniel.luevanoui@uanl.edu.mx
