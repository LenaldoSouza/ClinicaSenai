using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClinicaCrud.Models
{
    public partial class Exame
    {
        public int ExameId { get; set; }
        public int? ConsultaId { get; set; }
        public string TipoExame { get; set; } = null!;
        public string? Resultado { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DataExame { get; set; }

        public virtual Consultum? Consulta { get; set; }
    }
}
