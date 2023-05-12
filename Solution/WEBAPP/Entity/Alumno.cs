using System;
using System.Collections.Generic;

namespace WEBAPP.Entity
{
    public partial class Alumno
    {
        public Alumno()
        {
            Nota = new HashSet<Notum>();
        }

        public int IdAlumno { get; set; }
        public string? Nombres { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Dni { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }

        public virtual ICollection<Notum> Nota { get; set; }
    }
}
