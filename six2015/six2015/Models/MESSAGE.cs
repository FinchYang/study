namespace six2015.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WYXX.MESSAGE")]
    public partial class MESSAGE
    {
        public decimal ID { get; set; }

        public decimal HISTORYID { get; set; }

        public DateTime TIME { get; set; }

        [Required]
        [StringLength(2000)]
        public string CONTENT { get; set; }

        [StringLength(1)]
        public string SENT { get; set; }
    }
}
