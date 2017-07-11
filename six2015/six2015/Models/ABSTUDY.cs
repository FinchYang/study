namespace six2015.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WYXX.ABSTUDY")]
    public partial class ABSTUDY
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string IDCARD { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string SNAME { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string LICENCE { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime TIME { get; set; }

        [StringLength(2)]
        public string SREMARK { get; set; }

        [StringLength(2)]
        public string PHOTO { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(20)]
        public string PHONENUMBER { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(10)]
        public string DEDUCTPOINTS { get; set; }

        [StringLength(30)]
        public string LICENCENUMBER { get; set; }

        [StringLength(50)]
        public string FILENAME { get; set; }

        [StringLength(10)]
        public string STATUS { get; set; }
    }
}
