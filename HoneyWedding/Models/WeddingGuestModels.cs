using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoneyWedding.Models
{
    public class WeddingGuest : ApplicationUser
    {
        //public Guid ID { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //[DataType(DataType.EmailAddress)]
        //public string Email { get; set; }
        //[DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        //public string PhoneNumber { get; set; }
        public DateTime? InviteDate { get; set; }
        public bool? CanAttend { get; set; }
        public bool DidRsvp { get; set; }
        [Display(Name = "RSVP Date")]
        public DateTime? RsvpDate { get; set; }
        [UIHint("BooleanYesNoUnknown")]
        public bool HasPlusOne { get; set; }
        [UIHint("BooleanYesNoUnknown")]
        public bool PlusOneCanAtend { get; set; }
        public string Notes { get; set; }

        // dietary prefs
        [UIHint("BooleanYesNoUnknown")]
        public bool? NoMeat { get; set; }
        [UIHint("BooleanYesNoUnknown")]
        public bool? NoDairy { get; set; }
        [UIHint("BooleanYesNoUnknown")]
        public bool? NoGluten { get; set; }
        public string DietaryNotes { get; set; }

    }
    public class InviteWeddingGuestViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phone (###-###-####)")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Plus one?")]
        [UIHint("BooleanYesNoUnknown")]
        public bool HasPlusOne { get; set; }
        [Display(Name = "Notes...")]
        public string Notes { get; set; }

    }



}