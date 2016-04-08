using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoneyWedding.Models
{
    public class AccommodationLocation
    {
        public int ID { get; set; }
        [Display(Name = "Name")]
        public string LocationName { get; set; }
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string PhoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Url)]
        [UIHint("OpenInNewWindow")]
        public string Website { get; set; }
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }
        [DataType(DataType.MultilineText)]
        public string HoneyComments { get; set; }
        [Display(Name = "In Fair Play?")]
        [UIHint("BooleanYesKnowUnknown")]
        public bool InFairPlay { get; set; }
        [Display(Name = "Distance from Venue")]
        public int DistanceFromVenue { get; set; }
        public string Img { get; set; }
        [UIHint("StarRating")]
        [Display(Name ="Baller-ness")]
        public int BallerRating { get; set; }


        // navigation properties
        public virtual ICollection<AccommodationRoom> Rooms { get; set; }
    }

    public class AccommodationRoom
    {
        public AccommodationRoom()
        {
            IsAvailable = true;
        }

        public int ID { get; set; }
        public int AccommodationLocationID { get; set; }
        [Display(Name = "Room")]
        public string RoomName { get; set; }
        [Display(Name = "Sleeps")]
        public int SleepsTotal { get; set; }
        [Display(Name = "Beds for:")]
        public int SleepsBed { get; set; }
        [Display(Name = "Sofa beds for:")]
        public int SleepsSofa { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Display(Name = "Cost Per Night")]
        public decimal? CostNightly { get; set; }
        [Display(Name ="Minimum # Nights")]
        public int? MinNights { get; set; }
        [Display(Name = "Available?")]
        public bool IsAvailable { get; set; }

        // navigation properties
        public virtual AccommodationLocation Location { get; set; }

        // calculated properties
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Display(Name = "Minimum Total Cost")]
        public decimal? CostMinimum {
            get
            {
                if (CostNightly != null && MinNights != null)
                {
                    return CostNightly * MinNights;
                }
                else
                {
                    return null;
                }
            }
        }

    }

    public class AccommodationViewModel
    {
        public int ID { get; set; }
        [Display(Name = "Name")]
        public string LocationName { get; set; }
        [Display(Name = "Cost/Night")]
        public string PriceRange { get; set; }
        [Display(Name = "Sleeps")]
        public string RoomFor { get; set; }
        [UIHint("StarRating")]
        [Display(Name = "Baller-ness")]
        public int BallerRating { get; set; }

    }
}