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
using Meeting_Scheduler_App.Common;
using Meeting_Scheduler_App.Model;

namespace Meeting_Scheduler_App
{
    class UserHandler
    {
        public List<string> PositionsList;

        public void AddUser()
        {

        }

        public bool CheckUserExists(User user, User cuser)
        {
            var result = Database.GetUsers().FirstOrDefault(
                        a => a.Username.Equals(user.Username) && a.Password.Equals(EncryptedPassword(user.Password)));

            if (result != null)
            {
                cuser = result;
                return true;
            }
            return false;
        }

        public string EncryptedPassword(string str)
        {
            var alg = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            IBuffer buff = CryptographicBuffer.ConvertStringToBinary(str, BinaryStringEncoding.Utf8);
            var hashed = alg.HashData(buff);
            return CryptographicBuffer.EncodeToHexString(hashed);
        }

        public UserHandler()
        {
            PositionsList = new List<string> { "Admin", "Manager" };
        }
    }
}
