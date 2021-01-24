using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Net.Http;
using AppointmentReminder.Core.Models.Zoom;
using AppointmentReminder.Core.Util;

namespace AppointmentReminder.Core
{
    /// <summary>
    /// Service for interfacing with Zoom API with JWT authentication
    /// </summary>
    public class ZoomService
    {
        const string ZOOM_HOST = "https://api.zoom.us/v2";

        string apiKey;
        string apiSecret;
        HttpClient client;

        /// <summary>
        /// Creates Zoom service with Zoom API Credentials
        /// </summary>
        /// <param name="ApiKey"></param>
        /// <param name="ApiSecret"></param>
        public ZoomService(string ApiKey, string ApiSecret)
        {
            this.apiKey = ApiKey;
            this.apiSecret = ApiSecret;
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {this.CreateJWT()}");
        }

        /// <summary>
        /// List zoom users
        /// </summary>
        /// <returns></returns>
        public async Task<UserPageResponse> ListUsers()
        {
            var resp = await client.GetAsync(ZOOM_HOST + "/users");
            var data = await resp.Content.ReadAsStringAsync();

            return UserPageResponse.FromJSON(data);
        }

        /// <summary>
        /// List zoom meetings
        /// </summary>
        /// <returns></returns>
        public async Task<MeetingListResponse> ListMeetings(string UserId, int PageSize = 300)
        {
            var resp = await client.GetAsync(ZOOM_HOST + $"/users/{UserId}/meetings?page_size={PageSize}");
            var data = await resp.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<MeetingListResponse>(data);
            return model;
        }

        /// <summary>
        /// Creates a meeting in Zoom
        /// </summary>
        /// <param name="UserId">Host of the User</param>
        /// <param name="request">Meeting details</param>
        /// <returns></returns>
        public async Task<MeetingModel> CreateMeeting(string UserId, CreateMeetingRequest request)
        {
            var resp = await client.PostAsync(ZOOM_HOST + $"/users/{UserId}/meetings", toJson(request));
            var body = await resp.Content.ReadAsStringAsync();


            if ((int)resp.StatusCode != 201)
                throw new Exception(body);

            var model = JsonConvert.DeserializeObject<MeetingModel>(body);
            return model;
        }

        /// <summary>
        /// Deletes a meeting in Zoom
        /// </summary>
        /// <param name="UserId">Host of the User</param>
        /// <param name="request">Meeting details</param>
        /// <returns></returns>
        public async Task DeleteMeeting(long MeetingId)
        {
            var resp = await client.DeleteAsync(ZOOM_HOST + $"/meetings/{MeetingId.ToString()}");
            var body = await resp.Content.ReadAsStringAsync();

            if ((int)resp.StatusCode != 204)
                throw new Exception(body);
        }

        /// <summary>
        /// Update a meeting in Zoom
        /// </summary>
        /// <param name="UserId">Host of the User</param>
        /// <param name="request">Meeting details</param>
        /// <returns></returns>
        public async Task<bool> UpdateMeeting(long MeetingId, UpdateMeetingRequest request)
        {
            var resp = await client.PatchAsync(ZOOM_HOST + $"/meetings/{MeetingId.ToString()}", toJson(request));
            var body = await resp.Content.ReadAsStringAsync();

            if((int)resp.StatusCode != 204)
                throw new Exception(body);

            return true;
        }

        /// <summary>
        /// Create JWT for API Calls
        /// </summary>
        /// <returns></returns>
        public string CreateJWT()
        {

            try
            {
                // Generating the token 
                var now = DateTime.UtcNow;
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64),
                };

                var handler = new JwtSecurityTokenHandler();
                var signingKey = new SigningCredentials(
                   new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(this.apiSecret)),
                   SecurityAlgorithms.HmacSha256
                ); ;

                //create actual token
                var token = new JwtSecurityToken(
                    issuer: apiKey,
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: signingKey
                );

                return handler.WriteToken(token);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine(
                     new System.Diagnostics.StackTrace().ToString()
                );
            }
            return null;

        }

        //Helper
        private int ToUnixEpochDate(DateTime Time)
        {
            TimeSpan t = Time - new DateTime(1970, 1, 1);
            return (int)t.TotalSeconds;
        }


        private StringContent toJson(object body)
        {
            var model = JsonConvert.SerializeObject(body);
            return new StringContent(model, Encoding.UTF8, "application/json");
        }
    }
}
