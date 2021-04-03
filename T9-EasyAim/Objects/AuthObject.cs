using Newtonsoft.Json;
namespace T9_EasyAim.Objects
{
    public class AuthObject
    {
        [JsonProperty]
        public string Request { get; private set; }
        public string CheatName { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }

        public AuthObject(string username, string password, string request, string cheatname) 
        {
            Username = username;
            Password = password;
            Request = request;
            CheatName = cheatname;
        }
    }
}
