using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

            var premium = PremiumCalculator.CalculateDeathPremium(Convert.ToDouble(coverAmount), Convert.ToInt32(age), occupation);

            if (premium.IsOk)
                return Ok(premium.Value);

            else
                return BadRequest(premium.Errors);

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
