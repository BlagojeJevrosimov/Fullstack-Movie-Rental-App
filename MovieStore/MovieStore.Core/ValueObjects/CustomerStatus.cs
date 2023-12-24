using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Core.ValueObjects
{
    public record CustomerStatus( Status StatusValue, ExpirationDate StatusExpirationDateValue)
    {
        public static CustomerStatus Regular => new(Status.Regular, ExpirationDate.Infinity);
        public bool IsAdvanced() => StatusValue == Status.Advanced && !StatusExpirationDateValue.IsExpired();

        public decimal GetModifier() => this.IsAdvanced() ? 0.8m : 1m;
    }
}
