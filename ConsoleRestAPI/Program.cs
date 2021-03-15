using ConsoleRestAPI.Models;
using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleRestAPI
{
    class Program
    {

        //Why error if trying to equal status ok
        //how to make clean code here
        //should there be a new socket for each request?
        static async Task Main(string[] args)
        {
            //get info
            var httpClient = new HttpClient();
            var httpResponce = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");
            //check if the resonce was successful
            if (httpResponce.IsSuccessStatusCode)
            {
                //read content as string 
                var contentString = await httpResponce.Content.ReadAsStringAsync();
                //maping 
                var users = JsonConvert.DeserializeObject<List<User>>(contentString);
                var UserId = users.Where(u => u.Name == "Mrs. Dennis Schulist").Select(u =>u.Id).FirstOrDefault();
               

                var httpResponceAlbums = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/albums");
                if (httpResponceAlbums.IsSuccessStatusCode)
                {
                 
                    var contentStringAlbum = await httpResponceAlbums.Content.ReadAsStringAsync();
                  
                    var AllAlbums = JsonConvert.DeserializeObject<List<Album>>(contentStringAlbum);
                    var Albums = AllAlbums.Where(album => album.UserId == UserId).ToList();

                    var httpResponcePhotos = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/photos");
                    if (httpResponceAlbums.IsSuccessStatusCode)
                    {
                        
                        var contentStringPhotos = await httpResponcePhotos.Content.ReadAsStringAsync();
                     
                        var AllPhotos = JsonConvert.DeserializeObject<List<Photo>>(contentStringPhotos);
                        List<Photo> Photos = new List<Photo>();
                        foreach (var album in Albums)
                        {
                            Photos = AllPhotos.Where(photo => photo.AlbumId == album.Id).ToList();
                        }
                        Photos.ForEach(photo => Console.WriteLine($"{photo.Url}"));
                    }
                }
            }


        }
    }
}
