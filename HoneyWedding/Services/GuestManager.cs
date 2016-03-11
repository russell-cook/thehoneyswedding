using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HoneyWedding.Models;
using HoneyWedding.DAL.Repositories;
using System.Threading.Tasks;

namespace HoneyWedding.Services
{
    public class GuestManager
    {
        public int TotalCount(IEnumerable<Guest> guests)
        {
            var guestCount = guests.Count();
            var plusOneCount = guests.Where(g => g.RsvpPlusOne).Count();
            return guestCount + plusOneCount;
        }
    }
}