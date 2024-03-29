﻿using JaVisitei.Brasil.Business.ViewModels.Response.Base;
using System;
using System.Text.Json.Serialization;

namespace JaVisitei.Brasil.Business.ViewModels.Response.User
{
    public class UserResponse : BasicResponse
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("surname")]
        public string Surname { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("user_role_id")]
        public int UserRoleId { get; set; }

        [JsonPropertyName("security_stamp")]
        public string SecurityStamp { get; set; }

        [JsonPropertyName("registry_date")]
        public DateTime RegistryDate { get; set; }

        [JsonPropertyName("actived")]
        public bool Actived { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("newsletter")]
        public bool Newsletter { get; set; }
    }
}
