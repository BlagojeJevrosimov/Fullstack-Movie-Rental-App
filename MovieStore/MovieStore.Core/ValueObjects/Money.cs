using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Core.ValueObjects
{
    public record Money
    {
        public decimal Value { get; init; }
        private Money(decimal value) { Value = value; }

        public static Result<Money> Create( decimal  value ) {
            
            if(value < 0)
            {
                return Result.Fail("Money spent can't be bellow zero.");
            }
            if (HasMoreThanTwoDecimalPlaces(value))
            {
                return Result.Fail("Currency can't have more that 2 decimal places.");
            }
            
            
            return Result.Ok( new Money(value)); 
        }
        private static bool HasMoreThanTwoDecimalPlaces(decimal number)
        {
            string decimalString = number.ToString(System.Globalization.CultureInfo.InvariantCulture);

            int decimalSeparatorIndex = decimalString.IndexOf('.');
            if (decimalSeparatorIndex == -1)
            {
                return false;
            }

            int decimalPlacesCount = decimalString.Skip(decimalSeparatorIndex + 1).Count();

            return decimalPlacesCount > 2;
        }
    }
}
