using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meeting_Scheduler_App.Model
{
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
