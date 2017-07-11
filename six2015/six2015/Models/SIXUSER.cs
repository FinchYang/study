namespace six2015.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WYXX.SIXUSER")]
    public partial class SIXUSER
    {
        [Key]
        [StringLength(50)]
        public string NAME { get; set; }

        [Required]
        [StringLength(50)]
        public string PASSWORD { get; set; }

        public decimal POWER { get; set; }
    }
}
