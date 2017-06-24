namespace Stress_and_Performance_Testing
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<BUSINESSCATEGORY> BUSINESSCATEGORY { get; set; }
        public virtual DbSet<BUSINESSORDINAL> BUSINESSORDINAL { get; set; }
        public virtual DbSet<BUSSINESS> BUSSINESS { get; set; }
        public virtual DbSet<CARINFOR> CARINFOR { get; set; }
        public virtual DbSet<CATEGORIES> CATEGORIES { get; set; }
        public virtual DbSet<CONFIG> CONFIG { get; set; }
        public virtual DbSet<CORPORATEINFO> CORPORATEINFO { get; set; }
        public virtual DbSet<COUNTY> COUNTY { get; set; }
        public virtual DbSet<fushanbusiness> fushanbusiness { get; set; }
        public virtual DbSet<haiyangbusiness> haiyangbusiness { get; set; }
        public virtual DbSet<POPULATION> POPULATION { get; set; }
        public virtual DbSet<USERS> USERS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BUSINESSCATEGORY>()
                .Property(e => e.BUSINESSCODE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESSCATEGORY>()
                .Property(e => e.BUSINESSNAME)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESSCATEGORY>()
                .Property(e => e.CATEGORY)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESSCATEGORY>()
                .Property(e => e.SERVICEAPI)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESSORDINAL>()
                .Property(e => e.BUSINESSDATE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESSORDINAL>()
                .Property(e => e.CATEGORY)
                .IsUnicode(false);

            modelBuilder.Entity<BUSINESSORDINAL>()
                .Property(e => e.ORDINAL)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSINESSORDINAL>()
                .Property(e => e.COUNTYCODE)
                .IsUnicode(false);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.TYPE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.START_TIME)
                .IsUnicode(false);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.END_TIME)
                .IsUnicode(false);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.SERIAL_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.REJECT_REASON)
                .IsUnicode(false);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.PHONE_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.PROCESS_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.FILE_RECV_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.TRANSFER_STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.UPLOADER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.COMPLETE_PAY_USER)
                .IsUnicode(false);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.ATTENTION)
                .IsUnicode(false);

            modelBuilder.Entity<BUSSINESS>()
                .Property(e => e.COUNTYCODE)
                .IsUnicode(false);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.CAR_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.BRAND)
                .IsUnicode(false);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.MODEL_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.VIN)
                .IsUnicode(false);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.PLATE_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.OWNER)
                .IsUnicode(false);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.OWNER_ID)
                .IsUnicode(false);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.CAR_LENGTH)
                .HasPrecision(3, 3);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.CAR_WIDTH)
                .HasPrecision(3, 3);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.CAR_HEIGHT)
                .HasPrecision(3, 3);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.STANDARD_LENGTH)
                .HasPrecision(3, 3);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.STANDARD_WIDTH)
                .HasPrecision(3, 3);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.STANDARD_HEIGHT)
                .HasPrecision(3, 3);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.QUEUE_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.SERIAL_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.FINISH)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.TASK_TYPE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.INSPECTOR)
                .IsUnicode(false);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.RECHECKER)
                .IsUnicode(false);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.UNLOAD_TASK_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<CARINFOR>()
                .Property(e => e.INVALID_TASK)
                .HasPrecision(38, 0);

            modelBuilder.Entity<CATEGORIES>()
                .Property(e => e.CATEGORY)
                .IsUnicode(false);

            modelBuilder.Entity<CATEGORIES>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<CONFIG>()
                .Property(e => e.COUNTYCODE)
                .IsUnicode(false);

            modelBuilder.Entity<CONFIG>()
                .Property(e => e.BUSINESSTABLENAME)
                .IsUnicode(false);

            modelBuilder.Entity<CORPORATEINFO>()
                .Property(e => e.CODE)
                .IsUnicode(false);

            modelBuilder.Entity<CORPORATEINFO>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<CORPORATEINFO>()
                .Property(e => e.ADDRESS)
                .IsUnicode(false);

            modelBuilder.Entity<COUNTY>()
                .Property(e => e.COUNTYCODE)
                .IsUnicode(false);

            modelBuilder.Entity<COUNTY>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.TYPE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.START_TIME)
                .IsUnicode(false);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.END_TIME)
                .IsUnicode(false);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.SERIAL_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.REJECT_REASON)
                .IsUnicode(false);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.PHONE_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.PROCESS_USER)
                .IsUnicode(false);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.FILE_RECV_USER)
                .IsUnicode(false);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.TRANSFER_STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.UPLOADER)
                .IsUnicode(false);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.COMPLETE_PAY_USER)
                .IsUnicode(false);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.ATTENTION)
                .IsUnicode(false);

            modelBuilder.Entity<fushanbusiness>()
                .Property(e => e.COUNTYCODE)
                .IsUnicode(false);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.TYPE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.START_TIME)
                .IsUnicode(false);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.END_TIME)
                .IsUnicode(false);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.SERIAL_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.REJECT_REASON)
                .IsUnicode(false);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.PHONE_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.PROCESS_USER)
                .IsUnicode(false);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.FILE_RECV_USER)
                .IsUnicode(false);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.TRANSFER_STATUS)
                .HasPrecision(38, 0);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.UPLOADER)
                .IsUnicode(false);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.COMPLETE_PAY_USER)
                .IsUnicode(false);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.ATTENTION)
                .IsUnicode(false);

            modelBuilder.Entity<haiyangbusiness>()
                .Property(e => e.COUNTYCODE)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.SEX)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.NATION)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.BORN)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.ADDRESS)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.POSTCODE)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.POSTADDRESS)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.MOBILE)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.TELEPHONE)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.IDNUM)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.FIRSTFINGER)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.SECONDFINGER)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.LEFTEYE)
                .IsUnicode(false);

            modelBuilder.Entity<POPULATION>()
                .Property(e => e.RIGHTEYE)
                .IsUnicode(false);

            modelBuilder.Entity<USERS>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<USERS>()
                .Property(e => e.USERNAME)
                .IsUnicode(false);

            modelBuilder.Entity<USERS>()
                .Property(e => e.PASSWORD)
                .IsUnicode(false);

            modelBuilder.Entity<USERS>()
                .Property(e => e.LIMIT)
                .IsUnicode(false);

            modelBuilder.Entity<USERS>()
                .Property(e => e.DEPARTMENT)
                .IsUnicode(false);

            modelBuilder.Entity<USERS>()
                .Property(e => e.POST)
                .IsUnicode(false);

            modelBuilder.Entity<USERS>()
                .Property(e => e.POLICENUM)
                .IsUnicode(false);

            modelBuilder.Entity<USERS>()
                .Property(e => e.REALNAME)
                .IsUnicode(false);

            modelBuilder.Entity<USERS>()
                .Property(e => e.PDA_TYPE)
                .HasPrecision(38, 0);

            modelBuilder.Entity<USERS>()
                .Property(e => e.FIRSTFINGER)
                .IsUnicode(false);

            modelBuilder.Entity<USERS>()
                .Property(e => e.SECONDFINGER)
                .IsUnicode(false);

            modelBuilder.Entity<USERS>()
                .Property(e => e.COUNTYCODE)
                .IsUnicode(false);

            modelBuilder.Entity<USERS>()
                .Property(e => e.AUTHORITYLEVEL)
                .IsUnicode(false);
        }
    }
}
