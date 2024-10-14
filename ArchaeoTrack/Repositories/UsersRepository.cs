using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ArchaeoTrack.Models;
using ArcheoTrack.DAL;
using ArcheoTrack.Model;
using Auth0.OidcClient;
using SQLite;
using Microsoft.EntityFrameworkCore;

namespace ArchaeoTrack.Repositories {
    public class UsersRepository {

        private readonly AppDbContext _context;

        public UsersRepository( AppDbContext context ) { 
            _context = context;
        }

        public async Task<List<Auth0User>> GetAuth0Users() {
            try {
                string domain = "dev-cykt662hye6rkget.eu.auth0.com"; // Your Auth0 domain
                string clientId = "a7OyQYZJrtjpu7RdqcoMN1yPyHuX38fg";
                string clientSecret = "gvzUOwLYMP0KM6hNTiUYwjWMTPJ_4FRxqJzmv2WwPvaWGwo4TOvSv9ACj0FBf1bv";
                string audience = $"https://{domain}/api/v2/";
                string usersResponse = "";

                // Get Management API token
                var tokenUrl = $"https://{domain}/oauth/token";
                using( var httpClient = new HttpClient() ) {
                    var tokenResponse = await httpClient.PostAsync( tokenUrl, new StringContent( JsonSerializer.Serialize( new {
                        client_id = clientId,
                        client_secret = clientSecret,
                        audience = audience,
                        grant_type = "client_credentials"
                    } ), Encoding.UTF8, "application/json" ) );

                    var tokenContent = await tokenResponse.Content.ReadAsStringAsync();
                    var tokenJson = JsonDocument.Parse( tokenContent );
                    var accessToken = tokenJson.RootElement.GetProperty( "access_token" ).GetString();

                    // Get users
                    var usersUrl = $"https://{domain}/api/v2/users";
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", accessToken );
                    usersResponse = await httpClient.GetStringAsync( usersUrl );                   
                }
                var users = JsonSerializer.Deserialize<List<Auth0User>>( usersResponse );

                return users;
            }
            catch( Exception ex ) {
                Console.WriteLine( $"Error retrieving users from Auth0: {ex.Message}" );
                return new List<Auth0User>();
            }
        }

        public async Task SaveUsersToLocalDb( List<Auth0User> auth0Users ) {
            foreach( var auth0User in auth0Users ) {
                var user = new User {
                    Auth0UserId = auth0User.UserId,
                    Email = auth0User.Email,
                    FullName = auth0User.FullName,
                    Nickname = auth0User.Nickname
                };
                await _context.AddAsync( user );
            }
            await _context.SaveChangesAsync();
        }


    }
}
