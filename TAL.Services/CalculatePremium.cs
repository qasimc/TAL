using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using TAL.Domain.Interfaces;
using TAL.Domain.Models;
using TAL.Service.Interfaces;

namespace TAL.Services
{
    public class CalculatePremium : ICalculatePremium
    {

        IProvideTALData TALDataProvider;
        public CalculatePremium(IProvideTALData talData)
        {
            this.TALDataProvider = talData;
        }

        public ResultValue<double> CalculateDeathPremium(double coverAmount, int age, string occupation)
        {

            try
            {
                var TALOccupation = TALDataProvider.GetOccupation(occupation);
                if (TALOccupation.IsOk) {
                    var TALOccupationRating = TALDataProvider.GetOccupationRating(TALOccupation.Value.Rating);
                    if (TALOccupationRating.IsOk)
                    {
                        double deathPremium = (coverAmount * TALOccupationRating.Value.Factor * age) / 1000 * 12;
                        return Result.Ok(deathPremium);
                    }
                    else
                    {
                        return Result.Failed<double>(TALOccupationRating.Errors);
                    }
                }
                else
                {
                    return Result.Failed<double>(TALOccupation.Errors);
                }

            }
            catch (Exception ex)
            {
                return Result.Failed<double>(Error.CreateFrom("Internal Server Error", ErrorType.InternalServerError, ex.Message));
            }
        }
    }
}
