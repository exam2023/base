using System;
using System.Collections.Generic;

namespace Core.Entity
{
    public partial class Notas
    {
        public int IdNota { get; set; }
        public int IdAlumno { get; set; }
        public int IdCurso { get; set; }
        public int? Nota { get; set; }

        public string? Alumno { get; set; } = string.Empty;
        public string? Curso { get; set; } = string.Empty;

        public virtual Alumno? IdAlumnoNavigation { get; set; } = null!;
        public virtual Curso? IdCursoNavigation { get; set; } = null!;
    }
}
