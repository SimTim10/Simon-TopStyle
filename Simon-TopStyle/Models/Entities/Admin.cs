using System.ComponentModel.DataAnnotations;

namespace Simon_TopStyle.Models.Entities
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [StringLength(50)]
        public string AdminName { get; set; }
        [StringLength(80)]
        public string AdminEmail { get; set; }
    }
}
