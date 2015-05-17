using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Windows.UI.Popups;
using Meeting_Scheduler_App.Model;

namespace Meeting_Scheduler_App.Common
{
    internal class Database
    {
        public static void AddUser(User newUser)
        {
            //todo: add user to db
            throw new NotImplementedException();
        }

        public static void AddMeeting(Meeting newMeeting)
        {
            //todo: add meeting to db
            throw new NotImplementedException();
        }

        public static void UpdateMeeting(Meeting meeting)
        {
            //todo: update meeting in db (match by ID)
            throw new NotImplementedException();
        }

        public static void DeleteMeeting(Meeting meeting)
        {
            //todo: delete meeting from db (match by ID)
            throw new NotImplementedException();
        }
        

        public static IEnumerable<Room> GetRooms()
        {
            var client = GetClient();

            var response = client.GetAsync("api/Rooms").Result;

            if (response.IsSuccessStatusCode)
            {
                var rooms = response.Content.ReadAsAsync<IEnumerable<Room>>().Result;
                return rooms;
            }
            var md = new MessageDialog("Could not get rooms. Status code: " + response.StatusCode);
            md.ShowAsyncQueue();
            return Enumerable.Empty<Room>();
        }

        public static IEnumerable<Meeting> GetMeetings()
        {
            var client = GetClient();

            var response = client.GetAsync("api/Meetings").Result;

            if (response.IsSuccessStatusCode)
            {
                var meetings = response.Content.ReadAsAsync<IEnumerable<Meeting>>().Result;
                return meetings;
            }
            var md = new MessageDialog("Could not get meetings. Status code: " + response.StatusCode);
            md.ShowAsyncQueue();
            return Enumerable.Empty<Meeting>();
        }

        public static void CreateRoom(Room newRoom)
        {
            var client = GetClient();
            var response = client.PostAsJsonAsync("api/Rooms", newRoom).Result;

            if (response.IsSuccessStatusCode)
            {
                var md = new MessageDialog("Room added");
                md.ShowAsyncQueue();
            }
            else
            {
                var md = new MessageDialog("Could not add room. Status code: " + response.StatusCode);
                md.ShowAsyncQueue();
            }
        }

        public static List<User> GetUsers()
        {
            var users = new List<User>();
            var client = GetClient();

            var response = client.GetAsync("api/Users").Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<IEnumerable<User>>().Result;
                users = new List<User>(result);
            }
            else
            {
                var md = new MessageDialog("Could not get users. Status code: " + response.StatusCode);
                md.ShowAsync();
            }
            return users;
        }

        private static HttpClient GetClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9786/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}