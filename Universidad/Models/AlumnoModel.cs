using System;

namespace Universidad.Models
{
    public class Alumno
    {
        public int Id { get; set; } 

        public string Nombre { get; set; } = null;

        public string Matricula { get; set; } = null;

        public string Correo { get; set; } = null;

        public DateTime FechaNacimiento { get; set; } 

        public string Carrera { get; set; } = null;

        public float Promedio { get; set; } 
    }
}
