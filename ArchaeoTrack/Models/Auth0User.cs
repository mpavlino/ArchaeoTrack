using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ArchaeoTrack.Models {
    public class Auth0User {
        [JsonPropertyName( "user_id" )]
        public string UserId { get; set; }

        [JsonPropertyName( "email" )]
        public string Email { get; set; }

        [JsonPropertyName( "username" )]
        public string Username { get; set; }

        [JsonPropertyName( "name" )]
        public string FullName { get; set; }

        [JsonPropertyName( "picture" )]
        public string PictureUrl { get; set; }
    }

}
