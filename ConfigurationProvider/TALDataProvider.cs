using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using TAL.Domain.Interfaces;
using TAL.Domain.Models;
using System.Linq;
using System.Configuration;

namespace ConfigurationProvider
{
    public class TALDataProvider : IProvideTALData
    {
        
        public ResultValue<List<Occupation>> GetAllOccupations()
        {
            try
            {
                string occupationsString = System.IO.File.ReadAllText("Occupations.json");
                var occupations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Occupation>>(occupationsString);
                if (occupations != null)
                    return Result.Ok(occupations);

                return Result.Failed<List<Occupation>>(Error.CreateFrom("Occupations not found in datatabase", ErrorType.OccupationNotFound));

            }
            catch (Exception ex)
            {
                return Result.Failed<List<Occupation>>(Error.CreateFrom("Error while fetching occupations", ErrorType.InternalServerError, ex.Message));
            }
        }

        public ResultValue<Occupation> GetOccupation(string occupationName)
        {
            try
            {
                string occupationsString = System.IO.File.ReadAllText("Occupations.json");
                var occupations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Occupation>>(occupationsString).FirstOrDefault(x=>x.OccupationName == occupationName);
                if(occupations != null)
                    return Result.Ok(occupations);

                return Result.Failed<Occupation>(Error.CreateFrom("Occupation not found in datatabase", ErrorType.OccupationNotFound));

            }
            catch(Exception ex)
            {
                return Result.Failed<Occupation>(Error.CreateFrom("Error while fetching occupations", ErrorType.InternalServerError, ex.Message));
            }
        }

        public ResultValue<OccupationRating> GetOccupationRating(string rating)
        {
            try
            {
                string occupationRatingString = System.IO.File.ReadAllText("OccupationRating.json");
                var occupationRatings = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OccupationRating>>(occupationRatingString).FirstOrDefault(x=>x.Rating == rating);

                if(occupationRatings != null)
                   return Result.Ok(occupationRatings);

                return Result.Failed<OccupationRating>(Error.CreateFrom("Occupation rating not found", ErrorType.OccupationRatingNotFound));
            }
            catch (Exception ex)
            {
                return Result.Failed<OccupationRating>(Error.CreateFrom("Error while fetching occupation ratings", ErrorType.InternalServerError, ex.Message));
            }

        }
    }
}
