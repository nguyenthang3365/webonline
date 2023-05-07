using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace project03.Models.entiy
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Qlhanghoa")
        {
        }

        public virtual DbSet<HangHoa> HangHoas { get; set; }
        public virtual DbSet<LoaiHang> LoaiHangs { get; set; }
        public virtual DbSet<tblAccount> tblAccounts { get; set; }
        public virtual DbSet<dathang> dathang { get; set; }

        public virtual DbSet<lichsumuahang> lichsumuahang { get; set; }
        public virtual DbSet<post> post { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HangHoa>()
                .Property(e => e.Gia)
                .HasPrecision(18, 0);

            modelBuilder.Entity<LoaiHang>()
                .HasMany(e => e.HangHoas)
                .WithRequired(e => e.LoaiHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblAccount>()
                .Property(e => e.Username)
                .IsFixedLength();

            modelBuilder.Entity<tblAccount>()
                .Property(e => e.Password)
                .IsFixedLength();
        }
    }
}
