using System;
using System.Collections.Generic;

namespace WEBAPP.Entity
{
    public partial class Notum
    {
        public int IdNota { get; set; }
        public int IdAlumno { get; set; }
        public int IdCurso { get; set; }
        public int? Nota { get; set; }

        public virtual Alumno IdAlumnoNavigation { get; set; } = null!;
        public virtual Curso IdCursoNavigation { get; set; } = null!;
    }
}
