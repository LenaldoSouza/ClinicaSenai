using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClinicaCrud.Models
{
    public partial class Paciente
    {
        public Paciente()
        {
            Consulta = new HashSet<Consultum>();
        }

        public int PacienteId { get; set; }
        public string Nome { get; set; } = null!;
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; } = null!;
        public string? Telefone { get; set; }

        public virtual ICollection<Consultum> Consulta { get; set; }
    }
}
