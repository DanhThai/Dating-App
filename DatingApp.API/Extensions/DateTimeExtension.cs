using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Extensions
{
    public static class DateTimeExtension
    {
        public static int GetAge(this DateTime? dateOfBirth){
            if (!dateOfBirth.HasValue) return 0;
            var today = DateTime.Now;
            return today.Year - dateOfBirth.Value.Year;
        }
    }
}