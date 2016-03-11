using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoneyWedding.Models
{
    public class Guest : ApplicationUser
    {
        //public Guid ID { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //[DataType(DataType.EmailAddress)]
        //public string Email { get; set; }
        //[DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        //public string PhoneNumber { get; set; }
        public bool? CanAttend { get; set; }
        public bool DidRsvp { get; set; }
        [Display(Name = "RSVP Date")]
        public DateTime RsvpDate { get; set; }
        public bool HasPlusOne { get; set; }
        public bool RsvpPlusOne { get; set; }
        public string Notes { get; set; }

        public bool EmailSent { get; set; }

        [Display(Name = "Name")]
        public string FullName
        {
            get
            {
                if (FirstName != null && LastName != null)
                {
                    return FirstName + " " + LastName;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}