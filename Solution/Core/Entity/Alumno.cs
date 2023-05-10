using System;
using System.Collections.Generic;

namespace Core.Entity
{
    public partial class Alumno
    {
        public Alumno()
        {
            Nota = new HashSet<Notas>();
        }

        public int IdAlumno { get; set; }
        public string? Nombres { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? DNI { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }

        public virtual ICollection<Notas> Nota { get; set; }
    }
}
