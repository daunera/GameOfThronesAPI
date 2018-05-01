using GameOfThronesAPI.Exceptions;
using GameOfThronesAPI.Models;
using GameOfThronesAPI.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GameOfThronesAPI.Services
{
    class ThroneService 
    {
        private async Task<Object[]> GetPagedAsync<T>(Uri uri)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(uri);
                    Linker links = LinksTransform(((String[])response.Headers.GetValues("link"))[0]);
                    var json = await response.Content.ReadAsStringAsync();
                    T result = JsonConvert.DeserializeObject<T>(json);

                    if (((string)json).Equals("[]"))
                    {
                        throw new EmptyJsonException();
                    }

                    Object[] array = new Object[] { result, links };
                    App.ApiCalls++;
                    return array;
                }
                catch (HttpRequestException)
                {
                    ContentDialog noConnectionDialog = new ContentDialog
                    {
                        Title = "No internet connection",
                        Content = "Check your connection and try again.",
                        CloseButtonText = "Ok"
                    };

                    ContentDialogResult result = await noConnectionDialog.ShowAsync();
                    throw new RedirectMainException();
                }
                catch (EmptyJsonException)
                {
                    ContentDialog noItemDialog = new ContentDialog
                    {
                        Title = "No result",
                        Content = "No item found with these filters.",
                        CloseButtonText = "Ok"
                    };

                    ContentDialogResult result = await noItemDialog.ShowAsync();
                    return new Object[] { null, new Linker() };
                }
            }
        }

        //Gets houses list with matching parameter on generic method
        public async Task<Object[]> GetHousesAsync(String url)
        {
            return await GetPagedAsync<List<House>>(new Uri(url));
        }

        //Gets books list with matching parameter on generic method
        public async Task<Object[]> GetBooksAsync(String url)
        {
            return await GetPagedAsync<List<Book>>(new Uri(url));
        }

        //Gets characters list with matching parameter on generic method
        public async Task<Object[]> GetCharactersAsync(String url)
        {
            return await GetPagedAsync<List<Character>>(new Uri(url));
        }

        private async Task<T> GetSimpleAsync<T>(Uri uri)
        {

            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(uri);
                    var json = await response.Content.ReadAsStringAsync();
                    T result = JsonConvert.DeserializeObject<T>(json);
                    App.ApiCalls++;
                    return result;
                }
                catch (HttpRequestException)
                {
                    ContentDialog noConnectionDialog = new ContentDialog
                    {
                        Title = "No internet connection",
                        Content = "Check your connection and try again.",
                        CloseButtonText = "Ok"
                    };

                    ContentDialogResult result = await noConnectionDialog.ShowAsync();
                    throw new RedirectMainException();
                }
            }
        }

        //Gets a detailed house with matching parameter on generic method
        public async Task<House> GetHouseAsync(String url)
        {
            if (url.Equals(""))
                return null;
            return await GetSimpleAsync<House>(new Uri(url));
        }

        //Gets a detailed character with matching parameter on generic method
        public async Task<Character> GetCharacterAsync(String url)
        {
            if (url.Equals(""))
                return null;
            return await GetSimpleAsync<Character>(new Uri(url));
        }

        //Gets a detailed book with matching parameter on generic method
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

            if (linker.First.Equals(linker.Last))
            {
                linker.First = null;
                linker.Last = null;
            }

            return linker;
        }
    }
}
