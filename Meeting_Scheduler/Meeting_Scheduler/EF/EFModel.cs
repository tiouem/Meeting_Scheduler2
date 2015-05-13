namespace Meeting_Scheduler.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EFModel : DbContext
    {
        public EFModel()
            : base("name=EFModel")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Meeting> Meetings { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<User> Users { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meeting>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Meeting>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.Type)
                .IsUnicode(false);
        }
    }
}
