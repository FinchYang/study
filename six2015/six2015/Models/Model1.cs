namespace six2015.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model12")
        {
        }

        public virtual DbSet<HISTORY> HISTORY { get; set; }
        public virtual DbSet<MESSAGE> MESSAGE { get; set; }
        public virtual DbSet<SIXUSER> SIXUSER { get; set; }
        public virtual DbSet<ABSTUDY> ABSTUDY { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HISTORY>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<HISTORY>()
                .Property(e => e.IDCARD)
                .IsUnicode(false);

            modelBuilder.Entity<HISTORY>()
                .Property(e => e.LICENCE)
                .IsUnicode(false);

            modelBuilder.Entity<HISTORY>()
                .Property(e => e.SREMARK)
                .IsUnicode(false);

            modelBuilder.Entity<HISTORY>()
                .Property(e => e.PHONENUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<HISTORY>()
                .Property(e => e.DEDUCTPOINTS)
                .IsUnicode(false);

            modelBuilder.Entity<HISTORY>()
                .Property(e => e.ZHIDUINUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<HISTORY>()
                .Property(e => e.ADDRESS)
                .IsUnicode(false);

            modelBuilder.Entity<HISTORY>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<HISTORY>()
                .Property(e => e.FILENAME)
                .IsUnicode(false);

            modelBuilder.Entity<HISTORY>()
                .Property(e => e.LICENCENUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<HISTORY>()
                .Property(e => e.STATUS)
                .IsUnicode(false);

            modelBuilder.Entity<HISTORY>()
                .Property(e => e.PHOTO)
                .IsUnicode(false);

            modelBuilder.Entity<HISTORY>()
                .Property(e => e.PROCESSED)
                .IsUnicode(false);

            modelBuilder.Entity<HISTORY>()
                .Property(e => e.PRINTED)
                .IsUnicode(false);

            modelBuilder.Entity<HISTORY>()
                .Property(e => e.MESSAGED)
                .IsUnicode(false);

            modelBuilder.Entity<MESSAGE>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<MESSAGE>()
                .Property(e => e.HISTORYID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<MESSAGE>()
                .Property(e => e.CONTENT)
                .IsUnicode(false);

            modelBuilder.Entity<SIXUSER>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<SIXUSER>()
                .Property(e => e.PASSWORD)
                .IsUnicode(false);

            modelBuilder.Entity<SIXUSER>()
                .Property(e => e.POWER)
                .HasPrecision(38, 0);

            modelBuilder.Entity<ABSTUDY>()
                .Property(e => e.IDCARD)
                .IsUnicode(false);

            modelBuilder.Entity<ABSTUDY>()
                .Property(e => e.SNAME)
                .IsUnicode(false);

            modelBuilder.Entity<ABSTUDY>()
                .Property(e => e.LICENCE)
                .IsUnicode(false);

            modelBuilder.Entity<ABSTUDY>()
                .Property(e => e.SREMARK)
                .IsUnicode(false);

            modelBuilder.Entity<ABSTUDY>()
                .Property(e => e.PHOTO)
                .IsUnicode(false);

            modelBuilder.Entity<ABSTUDY>()
                .Property(e => e.PHONENUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<ABSTUDY>()
                .Property(e => e.DEDUCTPOINTS)
                .IsUnicode(false);

            modelBuilder.Entity<ABSTUDY>()
                .Property(e => e.LICENCENUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<ABSTUDY>()
                .Property(e => e.FILENAME)
                .IsUnicode(false);

            modelBuilder.Entity<ABSTUDY>()
                .Property(e => e.STATUS)
                .IsUnicode(false);
        }
    }
}
