﻿using Microsoft.AspNetCore.Http;
using System.Text.Json;


namespace Web.Utils
{
    public static class SessionExtensions
    {
        private static readonly JsonSerializerOptions _json = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };


        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value, _json));
        }


        public static T? GetObject<T>(this ISession session, string key)
        {
            var str = session.GetString(key);
            return str is null ? default : JsonSerializer.Deserialize<T>(str, _json);
        }
    }
}