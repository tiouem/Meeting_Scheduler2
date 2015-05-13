using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Meeting_Scheduler.EF;
using Meeting_Scheduler_App.Model;

namespace Meeting_Scheduler_App
{
    class UserHandler
    {
        public List<string> PositionsList; 
        
        public void AddUser()
        {
            
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9786/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/Users").Result;

            if (response.IsSuccessStatusCode)
            {
                var asd = response.Content.ReadAsAsync<IEnumerable<User>>().Result;
                users.Clear();
                users = new List<User>(asd);
               
            }
            else
            {
                MessageDialog md = new MessageDialog("Not");
                md.ShowAsync();
            }      
            return users;
        }

      

        public bool CheckUserExists(User user,User cuser)
        {
            if (cuser == null) throw new ArgumentNullException("cuser");

            var result = GetUsers().Where(a => a.Username.Equals(user.Username) && a.Password.Equals(EncryptedPassword(user.Password)));
            if (!result.FirstOrDefault().Equals(null))
            {
                cuser = result.FirstOrDefault();
                return true;
            }
            return false;
        }

        public string EncryptedPassword(string str)
        {
            var alg = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            IBuffer buff = CryptographicBuffer.ConvertStringToBinary(str, BinaryStringEncoding.Utf8);
            var hashed = alg.HashData(buff);
            return  CryptographicBuffer.EncodeToHexString(hashed);
        }

        public UserHandler()
        {
            PositionsList = new List<string> {"Admin", "Manager"};
        }
    }
}
