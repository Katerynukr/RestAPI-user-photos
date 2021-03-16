using ConsoleRestAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRestAPI.Services
{
    public class JSONService
    {
        private readonly HttpClient _httpClient;
        private const string UsersUrl = "https://jsonplaceholder.typicode.com/users";
        private const string AlbumsUrl = "https://jsonplaceholder.typicode.com/albums";
        private const string PhotosUrl = "https://jsonplaceholder.typicode.com/photos";
        public JSONService()
        {
            _httpClient = new HttpClient();
        }



        public async Task<List<Photo>> GetPhotosAsync(List<Album> albums)
        {
            var httpResponcePhotos = await _httpClient.GetAsync(PhotosUrl);
            if (httpResponcePhotos.IsSuccessStatusCode)
            {

                var contentStringPhotos = await httpResponcePhotos.Content.ReadAsStringAsync();

                var AllPhotos = JsonConvert.DeserializeObject<List<Photo>>(contentStringPhotos);
                List<Photo> photos = new List<Photo>();
                foreach (var album in albums)
                {
                    photos = AllPhotos.Where(photo => photo.AlbumId == album.Id).ToList();
                }
                return photos;
            }
            else
            {
                return null;
            }
        }
        public async Task<User> GetUserByName()
        {
            var httpResponce = await _httpClient.GetAsync(UsersUrl);
            if (httpResponce.IsSuccessStatusCode)
            {
                var contentString = await httpResponce.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<User>>(contentString);
                var user = users.Where(u => u.Name == "Mrs. Dennis Schulist").FirstOrDefault();
                // var userId = users.Where(u => u.Name == "Mrs. Dennis Schulist").Select(u => u.Id).FirstOrDefault();
                return user;
            }
            else
            {
                return null;
            }
        }
        public async Task<List<Album>> GetAlbumsAsync(User user)
        {
            var httpResponceAlbums = await _httpClient.GetAsync(AlbumsUrl);
            if (httpResponceAlbums.IsSuccessStatusCode)
            {
                var contentStringAlbum = await httpResponceAlbums.Content.ReadAsStringAsync();
                var allAlbums = JsonConvert.DeserializeObject<List<Album>>(contentStringAlbum);
                var albums = allAlbums.Where(album => album.UserId == user.Id).ToList();
                return albums;
            }
            else 
            { 
                return null; 
            }
        }
    }
}
