using ConsoleRestAPI.Models;
using ConsoleRestAPI.Services;
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

        static async Task Main(string[] args)
        {
            JSONService service = new JSONService();
            var user = await service.GetUserByName();
            var albums = await service.GetAlbumsAsync(user);
            var photos = await service.GetPhotosAsync(albums);
            photos.ForEach(photo => Console.WriteLine($"{photo.Url}"));
        }
    }
}
