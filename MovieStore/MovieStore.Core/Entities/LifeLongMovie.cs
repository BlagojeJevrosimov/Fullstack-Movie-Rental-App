using MovieStore.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Core.Entities
{
    public class LifeLongMovie : Movie
    {
        public override LicensingTypes LicensingType { get; set; } = LicensingTypes.LifeLong;

        public override Money GetBasePrice()
        {
            return Price;
        }

        public override DateTime? GetExpirationDate()
        {
            return null as DateTime?;
        }

    }
}
