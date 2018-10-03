using KazenLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KazenClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var kaas = new Kaas();
            Console.WriteLine("Naam:");
            kaas.Naam = Console.ReadLine();
            Console.WriteLine("Smaak:");
            kaas.Smaak = Console.ReadLine();
            Console.WriteLine("Type:");
            kaas.Type = Console.ReadLine();
            var client = new HttpClient();
            var url = "http://localhost:58427/api/kazen";
            var response = client.PostAsJsonAsync<Kaas>(url, kaas).Result;
            if(response.IsSuccessStatusCode)
            {
                response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var kazen = response.Content.ReadAsAsync<Kazen>().Result;
                    kazen.ForEach(x => Console.WriteLine(x.Id + ":" + x.Naam));
                }
                else
                    Console.WriteLine(url + "is niet bereikbaar");
            }
            else
                Console.WriteLine("Kan niet toegevoegd worden:" + response.StatusCode);
        }
    }
}
