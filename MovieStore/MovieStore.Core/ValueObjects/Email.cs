using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MovieStore.Core.ValueObjects
{
    public record Email
    {
        public string Value { get; init; }
        private Email(string email) { Value = email; }

        public static Result<Email> Create(string value) { 
        
            if(string.IsNullOrEmpty(value))  {
                
                return Result.Fail("Email cannot be null");

                 }

            if (!Regex.IsMatch(value,"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$")) {

                return Result.Fail("Not a valid email");
            }
        
        
        return Result.Ok(new Email(value));
        }
    }
}
