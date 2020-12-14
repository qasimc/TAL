using System;
using System.Collections.Generic;
using System.Text;
using TAL.Domain.Models;

namespace TAL.Service.Interfaces
{
    public interface ICalculatePremium
    {
        ResultValue<double> CalculateDeathPremium(double coverAmount, int age, string occupation);
    }
}
