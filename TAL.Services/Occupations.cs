using System;
using System.Collections.Generic;
using System.Text;
using TAL.Domain.Interfaces;
using TAL.Domain.Models;
using TAL.Service.Interfaces;

namespace TAL.Services
{
    public class Occupations : IGetOccupations
    {
        IProvideTALData TALConfigurationProvider;
        public Occupations(IProvideTALData talConfiguration)
        {
            this.TALConfigurationProvider = talConfiguration;
        }
        public ResultValue<List<Occupation>> GetAllOccupations()
        {
            return TALConfigurationProvider.GetAllOccupations();
        }
    }
}
