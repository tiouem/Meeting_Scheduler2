namespace Meeting_Scheduler.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Meeting")]
    public partial class Meeting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Meeting_Id { get; set; }

        [StringLength(11)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public int? Room_Id { get; set; }

        public DateTime? Date { get; set; }

        public int? Duration { get; set; }

        public virtual Room Room { get; set; }
    }
}
