using System;
using System.Collections.Generic;

namespace WEBAPP.Entity
{
    public partial class Curso
    {
        public Curso()
        {
            Nota = new HashSet<Notum>();
        }

        public int IdCurso { get; set; }
        public string Nombre { get; set; } = null!;
        public string Docente { get; set; } = null!;

        public virtual ICollection<Notum> Nota { get; set; }
    }
}
