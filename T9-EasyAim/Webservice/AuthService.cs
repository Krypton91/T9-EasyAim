using System;
using System.IO;
using System.Net;
using T9_EasyAim.Objects;
using Newtonsoft.Json;
namespace T9_EasyAim.Webservice
{
    
    internal class AuthService
    {
        protected bool m_IsAuthorized;
        public string error;
        public string cheatRemainigTime;
        internal void SendAuth(string username, string password)
        {
            try
            {
                
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/easyaimwebservice");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    AuthObject auth = new AuthObject(username, password, "CheatLoadAuth", "T9 Hoster");
                    string json = JsonConvert.SerializeObject(auth);
                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var response = JsonConvert.DeserializeObject<AuthResponseObject>(result);
                    if (response.ResponseStatus == "STATUS_SUCESFULLY") 
                    {
                        m_IsAuthorized = true;
                        cheatRemainigTime = response.CheatRemainigTime;
                    }
                    else 
                    {
                        error = response.ResponseStatus;
                        cheatRemainigTime = response.CheatRemainigTime;
                        m_IsAuthorized = false;
                    }
                }
            }
            catch
            {
                Environment.Exit(0);
            }
        }

        public bool IsAuthorized() 
        {
            return m_IsAuthorized;
        }
    }
}
