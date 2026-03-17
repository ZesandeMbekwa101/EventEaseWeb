using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EventEaseWeb.Models
{
    public class BookingModel
    {
        [Key]
        public int BookingID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select an event")]
        public int EventID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a venue")]
        public int VenueID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a client")]
        public int BookedBy { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }

        // Navigation Properties
        [ValidateNever]
        [ForeignKey("EventID")]
        public EventModel Event { get; set; }

        [ValidateNever]
        [ForeignKey("VenueID")]
        public VenueModel Venue { get; set; }

        [ValidateNever]
        [ForeignKey("BookedBy")]
        public UserModel User { get; set; }
    }
}