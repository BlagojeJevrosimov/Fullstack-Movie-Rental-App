using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Core.ValueObjects
{
    
    public record ExpirationDate(DateTime? Value)
    {
        public static ExpirationDate Infinity => new(null as DateTime?);

        public bool IsExpired() => this != ExpirationDate.Infinity && Value < DateTime.Now;
    }
}
