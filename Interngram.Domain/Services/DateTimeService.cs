using Interngram.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interngram.Domain.Services
{
    public class DateTimeService : IDateTimeService
    {
        public string ConvertDateTimeToUnix(DateTime birthDay)
        {
            string date = new DateTimeOffset(birthDay).ToUnixTimeMilliseconds().ToString();
            return date;
        }

        public DateTime ConvertUnixToDateTime(string birthDay)
        {
            DateTime date = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(birthDay)).UtcDateTime.Date;
            return date;
        }
    }
}
