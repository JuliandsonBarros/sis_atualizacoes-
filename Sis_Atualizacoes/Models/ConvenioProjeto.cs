﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Sis_Atualizacoes.Models
{
    [Table("Convenio_Projeto")]
    public partial class ConvenioProjeto
    {
        [Key]
        [Column("Id_Con")]
        public int IdCon { get; set; }
        [Key]
        [Column("Id_Proj")]
        public int IdProj { get; set; }

        [ForeignKey(nameof(IdCon))]
        [InverseProperty(nameof(Conveniados.ConvenioProjeto))]
        public virtual Conveniados IdConNavigation { get; set; } = null!;
    }
}
