namespace six2015.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WYXX.COUNT")]
    public partial class COUNT
    {
        [Key]
        [Column(Order = 0)]
        public DateTime TIME { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal PAGEVIEW { get; set; }

        [Key]
        [Column(Order = 2)]
        public decimal PAGEVIEWDAY { get; set; }

        [Key]
        [Column(Order = 3)]
        public decimal APPLICATION { get; set; }

        [Key]
        [Column(Order = 4)]
        public decimal APPLICATIONDAY { get; set; }

        [Key]
        [Column(Order = 5)]
        public decimal KAIFAQU { get; set; }

        [Key]
        [Column(Order = 6)]
        public decimal ZHIFUQU { get; set; }

        [Key]
        [Column(Order = 7)]
        public decimal FUSHANQU { get; set; }

        [Key]
        [Column(Order = 8)]
        public decimal MUPINGQU { get; set; }

        [Key]
        [Column(Order = 9)]
        public decimal LAISHANQU { get; set; }

        [Key]
        [Column(Order = 10)]
        public decimal LONGKOU { get; set; }

        [Key]
        [Column(Order = 11)]
        public decimal ZHAOYUAN { get; set; }

        [Key]
        [Column(Order = 12)]
        public decimal QIXIA { get; set; }

        [Key]
        [Column(Order = 13)]
        public decimal LAIZHOU { get; set; }

        [Key]
        [Column(Order = 14)]
        public decimal CHANGDAO { get; set; }

        [Key]
        [Column(Order = 15)]
        public decimal HAIYANG { get; set; }

        [Key]
        [Column(Order = 16)]
        public decimal LAIYANG { get; set; }

        [Key]
        [Column(Order = 17)]
        public decimal PENGLAI { get; set; }

        [Key]
        [Column(Order = 18)]
        public decimal GAOXINQU { get; set; }

        [Key]
        [Column(Order = 19)]
        public decimal OTHER { get; set; }
    }
}
