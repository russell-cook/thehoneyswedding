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
            var locations = await _unitOfWork.AccommodationsRepository.GetAsync(null, q => q.OrderBy(a => a.LocationName), "Rooms");
            var viewModel = new List<AccommodationViewModel>();
            foreach (AccommodationLocation location in locations)
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
                    var lowPriceFormatted = lowPrice.ToString().Remove(lowPrice.ToString().IndexOf('.'));
                    if (lowPrice != highPrice)
                    {
                        var highPriceFormatted = highPrice.ToString().Remove(highPrice.ToString().IndexOf('.'));
                        priceRange = string.Format("${0}-${1}", lowPriceFormatted, highPriceFormatted);
                    }
                    else
                    {
                        priceRange = string.Format("${0}", lowPriceFormatted);
                    }
                }

                var accommodationLocation = new AccommodationViewModel {
                    ID = location.ID,
                    LocationName = location.LocationName,
                    PriceRange = priceRange,
                    RoomFor = roomFor,
                    BallerRating = location.BallerRating,
                    Description = location.Description,
                    PhoneNumber = location.PhoneNumber,
                    Email = location.Email,
                    InFairPlay = location.InFairPlay,
                    DistanceFromVenue = location.DistanceFromVenue,
                    Notes = location.Notes,
                    Website = location.Website,
                    MapLink = location.MapLink
                };

                if (location.Img != null)
                {
                    accommodationLocation.Img = location.Img;
                }
                else
                {
                    accommodationLocation.Img = "needImage.png";
                }

                foreach (AccommodationRoom room in location.Rooms)
                {
                    accommodationLocation.Rooms.Add(room);
                }

                viewModel.Add(accommodationLocation);
            }

            return viewModel;
        }

        public async Task<AccommodationLocation> Detail(int id)
        {
            var accommodation = await _unitOfWork.AccommodationsRepository.GetByIDAsync(id);
            return accommodation;
        }

    }
}