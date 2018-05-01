using GameOfThronesAPI.Exceptions;
using GameOfThronesAPI.Models;
using GameOfThronesAPI.Services;
using GameOfThronesAPI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;

namespace GameOfThronesAPI.ViewModels
{
    class CharactersVM: ViewModelBase
    {
        public ObservableCollection<Character> Characters { get; set; } = new ObservableCollection<Character>();

        public Linker Buttons { get; set; } = new Linker();

        /// <summary>
        /// When navigated to this page, request the characters list.
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="mode"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            //if the command arrived from hambruger menu, have to decode some char
            String link = ((String)parameter).Replace("%3D", "=").Replace("%26", "&");
            SetCharactersList(link);
            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        /// <summary>
        /// Navigate to a selected character page
        /// </summary>
        /// <param name="characterURL">characters url</param>
        public void NavigateToCharacterDetails(String characterURL)
        {
            NavigationService.Navigate(typeof(CharacterDetailsPage), characterURL);
        }

        /// <summary>
        /// Refresh the characters list based on the parameter
        /// </summary>
        /// <param name="parameter"></param>
        public void LoadPage(String parameter)
        {
            Characters.Clear();
            SetCharactersList(parameter);
        }

        /// <summary>
        /// Create the filtered url, from the given paramters
        /// </summary>
        /// <param name="gender"></param>
        /// <param name="alive"></param>
        public void LoadFilteredPage(int gender, int alive)
        {
            String baseUrl = "https://www.anapioficeandfire.com/api/characters?";

            switch (gender)
            {
                case 0:
                    baseUrl += "gender=male&";
                    break;
                case 2:
                    baseUrl += "gender=female&";
                    break;
                default:
                    break;
            }

            switch (alive)
            {
                case 0:
                    baseUrl += "isAlive=true&";
                    break;
                case 2:
                    baseUrl += "isAlive=false&";
                    break;
                default:
                    break;
            }

            LoadPage(baseUrl + "pageSize=50");
        }

        /// <summary>
        /// Set the characterlist, which get from api request, with some error handling
        /// </summary>
        /// <param name="url"></param>
        private async void SetCharactersList(String url)
        {
            try
            {
                var service = new ThroneService();
                var obj = await service.GetCharactersAsync(url);
                if (obj[0] != null)
                {
                    List<Character> characters = (List<Character>)obj[0];
                    foreach (Character item in characters)
                    {
                        Characters.Add(item);
                    }
                }

                Buttons = (Linker)obj[1];
            }
            catch(RedirectMainException)
            {
                NavigationService.Navigate(typeof(MainPage));
            }
        }
    }

}
