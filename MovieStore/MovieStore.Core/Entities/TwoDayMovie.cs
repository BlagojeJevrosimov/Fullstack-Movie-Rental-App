using MovieStore.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Core.Entities
{
    public class TwoDayMovie : Movie
    {
        public override LicensingTypes LicensingType { get ; set; } = LicensingTypes.TwoDay;

        public override DateTime? GetExpirationDate()
        {
            return DateTime.Now.AddDays(2);
        }

        public override Money GetBasePrice()
        {
            return Price;
        }
    }
}
