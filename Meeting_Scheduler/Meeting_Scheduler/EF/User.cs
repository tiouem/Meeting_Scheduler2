namespace Meeting_Scheduler.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int User_Id { get; set; }

        [StringLength(20)]
        public string Username { get; set; }

        [StringLength(80)]
        public string Password { get; set; }
        [StringLength(20)]
        public string Position { get; set; }

     
    }
}
