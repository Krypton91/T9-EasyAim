using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T9_EasyAim.UserManager
{
    internal class User
    {
        protected string Username { get; private set; }
        protected string ExpireTimeString { get; private set; }

        public User(string username) 
        {
            Username = username;
        }

        internal string GetUsername() 
        {
            return Username;
        }


        internal string GetLicenseExpireTime()
        {
            double actuelTimestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            DateTime acutellTime = UnixTimeStampToDateTime(actuelTimestamp);
            DateTime ExpireTime = UnixTimeStampToDateTime(double.Parse(ExpireTimeString));

            if (ExpireTime <= acutellTime)
                return "Expired.";

            TimeSpan span = (ExpireTime - acutellTime);

            return string.Format("{0:00}:{1:00}:{2:00}:{3:00}", span.TotalDays, span.Hours, span.Minutes, span.Seconds);
        }

        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        internal void SetExpireTime(string time) 
        {
            ExpireTimeString = time;
        }
    }
}
