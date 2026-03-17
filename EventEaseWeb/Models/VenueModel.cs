using System.ComponentModel.DataAnnotations;

namespace EventEaseWeb.Models
{
    public class VenueModel
    {
        [Key]
        public int VenueID { get; set; }

        [Required(ErrorMessage = "Venue Name is required")]
        [StringLength(100)]
        public string VenueName { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(150)]
        public string Location { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be at least 1")]
        public int Capacity { get; set; }

        [StringLength(250)]
        public string Image { get; set; }
    }
}