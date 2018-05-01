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

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            String link = ((String)parameter).Replace("%3D", "=").Replace("%26", "&");
            SetCharactersList(link);
            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        public void NavigateToCharacterDetails(String characterURL)
        {
            NavigationService.Navigate(typeof(CharacterDetailsPage), characterURL);
        }

        public void LoadPage(String parameter)
        {
            Characters.Clear();
            SetCharactersList(parameter);
        }

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
