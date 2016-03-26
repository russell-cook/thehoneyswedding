using HoneyWedding.DAL.Repositories;
using HoneyWedding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HoneyWedding.Services
{
    public class AccommodationsManager
    {
        private UnitOfWork _unitOfWork;

        public AccommodationsManager()
        {
            _unitOfWork = new UnitOfWork();
        }

        public AccommodationsManager(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<AccommodationViewModel>> GetAsync()
        {
            var accommodations = await _unitOfWork.AccommodationsRepository.GetAsync(null, q => q.OrderBy(a => a.LocationName), "Rooms");
            var viewModel = new List<AccommodationViewModel>();
            foreach (AccommodationLocation location in accommodations)
            {
                decimal? lowPrice = null;
                decimal? highPrice = null;
                string priceRange = null;
                string roomFor = null;

                if (location.Rooms.Any())
                {
                    lowPrice = location.Rooms.OrderBy(r => r.CostNightly).First().CostNightly.Value;
                    highPrice = location.Rooms.OrderByDescending(r => r.CostNightly).First().CostNightly.Value;
                    int counter = 0;
                    foreach (AccommodationRoom room in location.Rooms)
                    {
                        if (room.SleepsTotal > 0)
                        {
                            counter = counter + room.SleepsTotal;
                        }
                    }
                    if (counter > 0)
                    {
                        roomFor = counter.ToString();
                    }
                }

                if (lowPrice.HasValue)
                {
                    if (lowPrice != highPrice)
                    {
                        priceRange = string.Format("${0}-${1}", lowPrice, highPrice);
                    }
                    else
                    {
                        priceRange = string.Format("${0}", lowPrice);
                    }
                }

                viewModel.Add(new AccommodationViewModel {
                    ID = location.ID,
                    LocationName = location.LocationName,
                    PriceRange = priceRange,
                    RoomFor = roomFor
                });
            }
            return viewModel;
        }
    }
}