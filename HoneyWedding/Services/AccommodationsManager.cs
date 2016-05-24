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

        public async Task<List<AccommodationViewModel>> GetAsync(string sortOrder)
        {

            Func<IQueryable<AccommodationLocation>, IOrderedQueryable<AccommodationLocation>> sortParm;

            switch (sortOrder)
            {
                case "name_desc":
                    sortParm = q => q.OrderByDescending(a => a.LocationName);
                    break;
                case "Distance":
                    sortParm = q => q.OrderBy(a => a.DistanceFromVenue);
                    break;
                case "distance_desc":
                    sortParm = q => q.OrderByDescending(a => a.DistanceFromVenue);
                    break;
                case "Baller":
                    sortParm = q => q.OrderBy(a => a.BallerRating).ThenBy(a => a.LocationName);
                    break;
                case "baller_desc":
                    sortParm = q => q.OrderByDescending(a => a.BallerRating).ThenBy(a => a.LocationName);
                    break;
                default:
                    sortParm = q => q.OrderBy(a => a.LocationName);
                    break;
            }

            var accommodationsManager = new AccommodationsManager();

            var locations = await _unitOfWork.AccommodationsRepository.GetAsync(null, sortParm, "Rooms");
            var viewModel = new List<AccommodationViewModel>();
            foreach (AccommodationLocation location in locations)
            {
                decimal? lowPrice = null;
                decimal? highPrice = null;
                string priceRange = null;
                int roomFor = 0;

                if (location.Rooms.Any())
                {
                    lowPrice = location.Rooms.OrderBy(r => r.CostNightly).First().CostNightly.Value;
                    highPrice = location.Rooms.OrderByDescending(r => r.CostNightly).First().CostNightly.Value;
                    int counter = 0;
                    foreach (AccommodationRoom room in location.Rooms)
                    {
                        if (room.SleepsTotal > 0 && room.IsAvailable)
                        {
                            counter = counter + room.SleepsTotal;
                        }
                    }
                    if (counter > 0)
                    {
                        roomFor = counter;
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
                    accommodationLocation.Img = "accommodations/" + location.Img;
                }
                else
                {
                    accommodationLocation.Img = "needImage.png";
                }

                foreach (AccommodationRoom room in location.Rooms.OrderBy(r => r.RoomName).ToList())
                {
                    accommodationLocation.Rooms.Add(room);
                }

                viewModel.Add(accommodationLocation);
            }

            switch (sortOrder)
            {
                case "Sleeps":
                    viewModel = viewModel.OrderBy(m => m.RoomFor).ThenBy(a => a.LocationName).ToList();
                    break;
                case "sleeps_desc":
                    viewModel = viewModel.OrderByDescending(m => m.RoomFor).ThenBy(a => a.LocationName).ToList();
                    break;
                case "Price":
                    viewModel = viewModel.OrderBy(m => m.PriceRange).ToList();
                    break;
                case "price_desc":
                    viewModel = viewModel.OrderByDescending(m => m.PriceRange).ToList();
                    break;
                default:
                    break;
            }


            return viewModel;
        }

        public async Task<AccommodationLocation> Detail(int id)
        {
            var accommodation = await _unitOfWork.AccommodationsRepository.GetByIDAsync(id);
            accommodation.Rooms = accommodation.Rooms.OrderBy(r => r.RoomName).ToList();
            return accommodation;
        }

    }
}