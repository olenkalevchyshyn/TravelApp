using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using TravelApp.Models;
using Newtonsoft.Json.Serialization;
using TravelApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace TravelApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PlaceController : ControllerBase
    {
        private readonly PlaceService _placeService;
        public PlaceController(PlaceService placeService)
        {
            _placeService = placeService;
        }

        //get place by id
        // https://localhost:44347/api/Place/5f8c78f355499b2d7609ca37
        [HttpGet("{placeId}", Name = "GetPlace")]
        public ActionResult<Place> Get(string placeId)
        {
            ObjectId id = new ObjectId(placeId);
            if (id != null)
                return _placeService.Get(id);
            else
                return NotFound();
        }

        [HttpGet("getInRadius/{placeId}")]
        public ActionResult<IEnumerable<Place>> GetNear(string placeId)
        {
            ObjectId id = new ObjectId(placeId);
            if (id != null)
                return _placeService.FindPlaces(id).ToList();
            else
                return NotFound();
        }

        [HttpPost]
        public ActionResult Post(Place place)
        {
            if (place != null)
            {
                _placeService.Create(place);
                return CreatedAtRoute("GetPlace", new { id = place.Id }, place);
            }
            else
                return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult Put(string placeId)
        {
            ObjectId id = new ObjectId(placeId);
            if (id != null)
            {
                var place = _placeService.Get(id);
                if (place != null)
                    _placeService.Update(id, place);
                else
                    return NotFound();
            }
            else
                return NotFound();
            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(string placeId)
        {
            ObjectId id = new ObjectId(placeId);
            if (id != null)
            {
                var place = _placeService.Get(id);
                if (place != null)
                    _placeService.Delete(place);
                else
                    return NotFound();
            }
            else
                return NotFound();
            return NoContent();
        }

        public ActionResult<IEnumerable<Place>> GetPlacesInRadius(Place initialPlace, double radius)
        {
            return null;
        }
    }
}
