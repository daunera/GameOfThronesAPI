using GameOfThronesAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GameOfThronesAPI.Services
{
    class ThroneService
    {
        private async Task<Object[]> GetPagedAsync<T>(Uri uri)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                Linker links = LinksTransform(((String[]) response.Headers.GetValues("link"))[0]);
                var json = await response.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(json);
                Object[] array = new Object[] { result, links };
                return array;
            }
        }

        public async Task<Object[]> GetHousesAsync(String url)
        {
            return await GetPagedAsync<List<House>>(new Uri(url));
        }

        public async Task<Object[]> GetBooksAsync(String url)
        {
            return await GetPagedAsync<List<Book>>(new Uri(url));
        }

        public async Task<Object[]> GetCharactersAsync(String url)
        {
            return await GetPagedAsync<List<Character>>(new Uri(url));
        }


        private async Task<T> GetSimpleAsync<T>(Uri uri)
        {

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
        }

        public async Task<House> GetHouseAsync(String url)
        {
            if (url.Equals(""))
                return null;
            return await GetSimpleAsync<House>(new Uri(url));
        }

        public async Task<Character> GetCharacterAsync(String url)
        {
            if (url.Equals(""))
                return null;
            return await GetSimpleAsync<Character>(new Uri(url));
        }

        public async Task<Book> GetBookAsync(String url)
        {
            if (url.Equals(""))
                return null;
            return await GetSimpleAsync<Book>(new Uri(url));
        }

        private Linker LinksTransform(String text)
        {
            String[] texts = text.Split(new[] { ", " }, StringSplitOptions.None);
            Linker linker = new Linker();
            foreach(string s in texts)
            {
                String[] tmp = s.Split(new[] { "; " }, StringSplitOptions.None);
                if (tmp[1].Equals("rel=\"next\""))
                    linker.Next = tmp[0].Substring(1,tmp[0].Length-2);
                else if (tmp[1].Equals("rel=\"prev\""))
                    linker.Prev = tmp[0].Substring(1, tmp[0].Length - 2);
                else if (tmp[1].Equals("rel=\"first\""))
                    linker.First = tmp[0].Substring(1, tmp[0].Length - 2);
                else if (tmp[1].Equals("rel=\"last\""))
                    linker.Last = tmp[0].Substring(1, tmp[0].Length - 2);
            }
            return linker;
        }
    }
}
