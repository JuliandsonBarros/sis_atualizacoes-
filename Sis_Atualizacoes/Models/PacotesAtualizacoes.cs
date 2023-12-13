using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Sis_Atualizacoes.Models
{
    [Table("Pacotes_Atualizacoes")]
    public partial class PacotesAtualizacoes
    {
        [Key]
        [Column("Id_Pacote")]
        public int IdPacote { get; set; }
        
        [Column("Id_Proj")]
        [ForeignKey(nameof(IdProj))]
        public int? IdProj { get; set; }
        [Column("Num_Versao")]
        [StringLength(30)]
        public string NumVersao { get; set; } = null!;
        [Column("Registro_Alteracoes")]
        public string RegistroAlteracoes { get; set; } = null!;
        [Column("Dt_Lancamento", TypeName = "datetime")]
        public DateTime? DtLancamento { get; set; }

        [ForeignKey(nameof(IdProj))]
        [InverseProperty(nameof(Projetos.PacotesAtualizacoes))]
        public virtual Projetos? IdProjNavigation { get; set; }
    }
}
