using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Meeting_Scheduler_App.Model;

namespace Meeting_Scheduler_App.Common
{
    class Database
    {
        public static IEnumerable<Room> GetRooms()
        {
            var client = GetClient();

            HttpResponseMessage response = client.GetAsync("api/Rooms").Result;

            if (response.IsSuccessStatusCode)
            {
                var rooms = response.Content.ReadAsAsync<IEnumerable<Room>>().Result;
                return rooms;
            }
            else
            {
                MessageDialog md = new MessageDialog("Not");
                md.ShowAsync();
            }
            return null;
        }

        public static IEnumerable<Meeting> GetMeetings()
        {
            var client = GetClient();

            HttpResponseMessage response = client.GetAsync("api/Meetings").Result;

            if (response.IsSuccessStatusCode)
            {
                var meetings = response.Content.ReadAsAsync<IEnumerable<Meeting>>().Result;
                return meetings;
            }
            else
            {
                MessageDialog md = new MessageDialog("Not");
                md.ShowAsync();
            }
            return null;
        }

        public static void CreateRoom(Room newRoom)
        {
            var client = GetClient();
            var response = client.PostAsJsonAsync("api/Rooms", newRoom).Result;

            if (response.IsSuccessStatusCode)
            {
                MessageDialog md = new MessageDialog("Room added");
                md.ShowAsync();
            }
            else
            {
                MessageDialog md = new MessageDialog(response.StatusCode.ToString());
                md.ShowAsync();
            }
        }

        private static HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9786/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
