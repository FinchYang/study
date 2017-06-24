namespace Stress_and_Performance_Testing
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CITY.BUSSINESS")]
    public partial class BUSSINESS
    {
        public decimal ID { get; set; }

        public decimal TYPE { get; set; }

        [Required]
        [StringLength(10)]
        public string START_TIME { get; set; }

        [StringLength(10)]
        public string END_TIME { get; set; }

        public decimal STATUS { get; set; }

        [Required]
        [StringLength(10)]
        public string QUEUE_NUM { get; set; }

        [StringLength(25)]
        public string ID_NUM { get; set; }

        [StringLength(100)]
        public string ADDRESS { get; set; }

        [StringLength(20)]
        public string SERIAL_NUM { get; set; }

        [StringLength(100)]
        public string REJECT_REASON { get; set; }

        [StringLength(100)]
        public string NAME { get; set; }

        [StringLength(50)]
        public string PHONE_NUM { get; set; }

        [StringLength(50)]
        public string PROCESS_USER { get; set; }

        [StringLength(50)]
        public string FILE_RECV_USER { get; set; }

        public decimal? TRANSFER_STATUS { get; set; }

        [StringLength(50)]
        public string UPLOADER { get; set; }

        [StringLength(50)]
        public string COMPLETE_PAY_USER { get; set; }

        [StringLength(100)]
        public string ATTENTION { get; set; }

        [StringLength(20)]
        public string UNLOAD_TASK_NUM { get; set; }

        [Required]
        [StringLength(20)]
        public string COUNTYCODE { get; set; }
    }
}
