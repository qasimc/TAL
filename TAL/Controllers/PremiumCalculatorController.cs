using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TAL.Domain.Models;
using TAL.Service.Interfaces;

namespace TAL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PremiumCalculatorController : ControllerBase
    {
        ICalculatePremium PremiumCalculator;
        IGetOccupations OccupationsGetter;
        public PremiumCalculatorController(ICalculatePremium premiumCalculator, IGetOccupations occupations)
        {
            PremiumCalculator = premiumCalculator;
            OccupationsGetter = occupations;
        }

        [HttpGet("GetPremium")]
        public IActionResult GetPremium()
        {
                string occupation = Request.Query["Occupation"];
                string age = Request.Query["Age"];
                string coverAmount = Request.Query["CoverAmount"];
                double convertedCoverAmount;
                int convertedAge;
                ResultValue<double> premium;
                if (Double.TryParse(coverAmount, out convertedCoverAmount) && int.TryParse(age, out convertedAge))
                {
                    premium = PremiumCalculator.CalculateDeathPremium(convertedCoverAmount, convertedAge, occupation);
                }
                else
                {
                    premium = Result.Failed<double>(Error.CreateFrom("InvalidInput", ErrorType.InvalidInput));
                }

                if (premium.IsOk)
                    return Ok(premium.Value);

                else
                    return BadRequest(JsonConvert.SerializeObject(premium.Errors.Select(x=>x.Message)));

        }
        [HttpGet("GetOccupations")]
        public IActionResult GetOccupations()
        {


            var occupations = OccupationsGetter.GetAllOccupations();

            if (occupations.IsOk)
                return Ok(occupations.Value);

            else
                return BadRequest(occupations.Errors);

        }
    }
}
