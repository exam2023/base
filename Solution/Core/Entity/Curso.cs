using System;
using System.Collections.Generic;

namespace Core.Entity
{
    public partial class Curso
    {
        public Curso()
        {
            Nota = new HashSet<Notas>();
        }

        public int IdCurso { get; set; }
        public string? Nombre { get; set; } = null!;
        public string? Docente { get; set; } = null!;

        public virtual ICollection<Notas> Nota { get; set; }
    }
}
