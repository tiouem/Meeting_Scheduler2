namespace Meeting_Scheduler.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Room")]
    public partial class Room
    {
        public Room()
        {
            Meetings = new HashSet<Meeting>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Room_Id { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        public int? Capacity { get; set; }

        public bool? Projector { get; set; }

        public bool? FlipChart { get; set; }

        public bool? Phone { get; set; }

        public bool? Camera { get; set; }

        public string Image { get; set; }
        public virtual ICollection<Meeting> Meetings { get; set; }
    }
}
