﻿namespace NHS111.Domain.Dos.Api.Models.Request
{
    public class DosUserInfo
    {
        public DosUserInfo(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; private set; }
        public string Password { get; private set; }
    }
}
