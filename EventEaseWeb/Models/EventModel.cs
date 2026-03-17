using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventEaseWeb.Models
{
    public class EventModel
    {
        [Key]
        public int EventID { get; set; }

        [Required(ErrorMessage = "Event name is required")]
        [StringLength(100)]
        public string EventName { get; set; }

        [Required(ErrorMessage = "Event date is required")]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please select a venue")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a venue")]
        public int VenueID { get; set; }  

        [ValidateNever]
        [ForeignKey("VenueID")]
        public VenueModel Venue { get; set; }
        
    }
}