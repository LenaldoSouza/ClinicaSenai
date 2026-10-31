using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClinicaCrud.Models
{
    public partial class Consultum
    {
        public Consultum()
        {
            Exames = new HashSet<Exame>();
        }

        public int ConsultaId { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DataHora { get; set; }
        public int? MedicoId { get; set; }
        public int? PacienteId { get; set; }
        public string? Observacoes { get; set; }

        public virtual Medico? Medico { get; set; }
        public virtual Paciente? Paciente { get; set; }
        public virtual ICollection<Exame> Exames { get; set; }
    }
}
