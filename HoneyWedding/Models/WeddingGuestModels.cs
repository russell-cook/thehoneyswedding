using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoneyWedding.Models
{
    public class WeddingGuest : ApplicationUser
    {
        public DateTime? InviteDate { get; set; }
        public bool? CanAttend { get; set; }
        public bool DidRsvp { get; set; }
        [Display(Name = "RSVP Date")]
        public DateTime? RsvpDate { get; set; }
        public bool UpdatedRsvp { get; set; }
        public DateTime? UpdatedRsvpDate { get; set; }
        [UIHint("BooleanYesNoUnknown")]
        public bool HasPlusOne { get; set; }
        [UIHint("BooleanYesNoUnknown")]
        public bool? PlusOneCanAtend { get; set; }
        public string Notes { get; set; }

        // dietary prefs
        [UIHint("BooleanYesNoUnknown")]
        public bool Meatless { get; set; }
        [UIHint("BooleanYesNoUnknown")]
        public bool Vegan { get; set; }
        [UIHint("BooleanYesNoUnknown")]
        public bool NoGluten { get; set; }
        public string DietaryNotes { get; set; }
        [UIHint("BooleanYesNoUnknown")]
        public bool MeatlessPlusOne { get; set; }
        [UIHint("BooleanYesNoUnknown")]
        public bool VeganPlusOne { get; set; }
        [UIHint("BooleanYesNoUnknown")]
        public bool NoGlutenPlusOne { get; set; }
        public string DietaryNotesPlusOne { get; set; }

    }

    public class WeddingGuestViewModel
    {
        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phone (###-###-####)")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string PhoneNumber { get; set; }


        [Display(Name = "Can Attend")]
        [UIHint("BooleanYesNoUnknown")]
        public bool? CanAttend { get; set; }
        [Display(Name = "Plus one?")]
        [UIHint("BooleanYesNoUnknown")]
        public bool HasPlusOne { get; set; }
        [Display(Name = "Plus one can attend")]
        [UIHint("BooleanYesNoUnknown")]
        public bool? PlusOneCanAttend { get; set; }
        [Display(Name = "Notes...")]
        public string Notes { get; set; }

        // dietary prefs
        [UIHint("BooleanYesNoUnknown")]
        public bool Meatless { get; set; }
        [UIHint("BooleanYesNoUnknown")]
        public bool Vegan { get; set; }
        [UIHint("BooleanYesNoUnknown")]
        public bool NoGluten { get; set; }
        public string DietaryNotes { get; set; }
        [UIHint("BooleanYesNoUnknown")]
        public bool MeatlessPlusOne { get; set; }
        [UIHint("BooleanYesNoUnknown")]
        public bool VeganPlusOne { get; set; }
        [UIHint("BooleanYesNoUnknown")]
        public bool NoGlutenPlusOne { get; set; }
        public string DietaryNotesPlusOne { get; set; }
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