using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace WebForm.Models
{
    public class ZoomFunctions
    {

        public string GetToken(Meeting meeting)
        {
            string key = meeting.ApiSecrate;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(credentials);
            var payload = new JwtPayload
            {
                { "iss", meeting.ApiKey},
                { "iat", DateTimeOffset.Now.ToUnixTimeSeconds() },
                { "exp", DateTimeOffset.Now.ToUnixTimeSeconds() + 1400 },
            };

            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();
            var tokenString = handler.WriteToken(secToken);
            var client = new RestClient("https://api.zoom.us/v2/users?status=active&page_size=30&page_number=1");
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", "Bearer " + tokenString.ToString());
            IRestResponse response = client.Execute(request);
            dynamic jsonObj = JsonConvert.DeserializeObject(response.Content);
            string id = Convert.ToString(jsonObj.users[0].id);
            return id;
        }
        public string CreateMeeting(Meeting meeting)
        {
            string userid = GetToken(meeting);
            string key = meeting.ApiSecrate;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(credentials);
            var payload = new JwtPayload
            {
                { "iss", meeting.ApiKey},
                { "topic", meeting.topic},
                { "type", meeting.type},
                { "duration", meeting.duration},
                { "agenda", meeting.agenda},
                { "end_date_time", meeting.end_time},
                { "start_time", meeting.start_time},
            };
            //var secToken = new JwtSecurityToken(header, payload);
            //var handler = new JwtSecurityTokenHandler();
            //var tokenString = handler.WriteToken(secToken);
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.zoom.us/v2/users/" + userid + "/meetings");
            RestRequest request = new RestRequest();
            //request.AddParameter(userid, ParameterType.UrlSegment);
            request.AddHeader("content-type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new Meeting() { topic = meeting.topic, duration = meeting.duration, agenda = meeting.agenda, start_time = meeting.start_time, type = meeting.type });
            request.AddHeader("authorization", "Bearer " + meeting.jwtToken);
            request.Method = Method.POST;
            var response = client.Execute(request);
            dynamic jsonObj = JsonConvert.DeserializeObject(response.Content);
            string data = Convert.ToString(jsonObj);
            return data;

        }

        public string GetMeetings(Meeting meeting)
        {
            object token = GetToken(meeting);
                string userId = Convert.ToString(token);
                string key = meeting.ApiSecrate;
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var header = new JwtHeader(credentials);
                var payload = new JwtPayload
                {
                    { "iss", meeting.ApiKey},
                    { "meetingId",meeting.MeetingId},

                };

                var secToken = new JwtSecurityToken(header, payload);
                var handler = new JwtSecurityTokenHandler();
                var tokenString = handler.WriteToken(secToken);
                RestClient client = new RestClient();
                client.BaseUrl = new Uri("https://api.zoom.us/v2/users/" + userId + "/meetings");
                var request = new RestRequest(Method.GET);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", "Bearer " + meeting.jwtToken);
                IRestResponse response = client.Execute(request);
                dynamic jsonObj = JsonConvert.DeserializeObject(response.Content);
                string data = Convert.ToString(jsonObj.meetings);
            //JArray textArray = JArray.Parse(data);
            //List<Meeting> meetings = new List<Meeting>();
            //for (int i = 0; i < textArray.Count(); i++)
            //{
            //    Meeting m = new Meeting();
            //    m.MeetingId = textArray[i]["id"].Value<string>();
            //    m.topic = textArray[i]["topic"].Value<string>();
            //    m.start_time = textArray[i]["start_time"].Value<string>();
            //    m.agenda = textArray[i]["agenda"].Value<string>();
            //    m.duration = textArray[i]["duration"].Value<string>();
            //    meetings.Add(m);
            //}

            return data;

        }

        //public void OnDelete(Meeting meeting)
        public void DeleteMeetings(Meeting meeting)
        {
            string meetingid = meeting.MeetingId;
            string key = meeting.ApiSecrate;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(credentials);
            var payload = new JwtPayload
            {
                { "iss", meeting.ApiKey},
                { "meetingId", meetingid},
            };
            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();
            var tokenString = handler.WriteToken(secToken);
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.zoom.us/v2/meetings/" + meetingid);
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", "Bearer " + meeting.jwtToken);
            IRestResponse response = client.Execute(request);
            dynamic jsonObj = JsonConvert.DeserializeObject(response.Content);
            var data = Convert.ToString(jsonObj);
            //return data;

        }
    }

    
}