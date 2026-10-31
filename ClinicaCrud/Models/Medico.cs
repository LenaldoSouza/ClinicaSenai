using System;
using System.Collections.Generic;

namespace ClinicaCrud.Models
{
    public partial class Medico
    {
        public Medico()
        {
            Consulta = new HashSet<Consultum>();
        }

        public int MedicoId { get; set; }
        public string Nome { get; set; } = null!;
        public string? Especialidade { get; set; }
        public string Crm { get; set; } = null!;

        public virtual ICollection<Consultum> Consulta { get; set; }
    }
}
