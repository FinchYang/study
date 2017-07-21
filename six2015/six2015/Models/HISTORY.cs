namespace six2015.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WYXX.HISTORY")]
    public partial class HISTORY
    {
        public decimal ID { get; set; }

        [StringLength(50)]
        public string IDCARD { get; set; }

        [StringLength(5)]
        public string LICENCE { get; set; }

        [StringLength(2)]
        public string SREMARK { get; set; }

        [StringLength(20)]
        public string PHONENUMBER { get; set; }

        [StringLength(10)]
        public string DEDUCTPOINTS { get; set; }

        [StringLength(20)]
        public string ZHIDUINUMBER { get; set; }

        [StringLength(200)]
        public string ADDRESS { get; set; }

        [StringLength(20)]
        public string NAME { get; set; }

        [StringLength(50)]
        public string FILENAME { get; set; }

        [StringLength(20)]
        public string LICENCENUMBER { get; set; }

        [StringLength(20)]
        public string STATUS { get; set; }

        public DateTime TIME { get; set; }

        [StringLength(2)]
        public string PHOTO { get; set; }

        [StringLength(1)]
        public string PROCESSED { get; set; }

        [StringLength(1)]
        public string PRINTED { get; set; }

        [StringLength(1)]
        public string MESSAGED { get; set; }

        [StringLength(500)]
        public string STUDYLOG { get; set; }

        [StringLength(1)]
        public string FAILURE { get; set; }

        public DateTime? SYYXQZ { get; set; }
    }
}
