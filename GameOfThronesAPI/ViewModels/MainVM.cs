using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using GameOfThronesAPI.Models;
using GameOfThronesAPI.Services;
using GameOfThronesAPI.Views;
using System.ComponentModel;

namespace GameOfThronesAPI.ViewModels
{
    public class MainVM : ViewModelBase
    {

        private int _appiCallNums;
        public int ApiCallNums
        {
            get { return _appiCallNums; }
            set { Set(ref _appiCallNums, value); }
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            ApiCallNums = App.ApiCalls;
            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        public void NavigateToHouses()
        {
            NavigationService.Navigate(typeof(HousesPage), "https://www.anapioficeandfire.com/api/houses?page=1&pageSize=50");
        }

        public void NavigateToCharacters()
        {
            NavigationService.Navigate(typeof(CharactersPage), "https://www.anapioficeandfire.com/api/characters?page=2&pageSize=50");
        }

        public void NavigateToBooks()
        {
            NavigationService.Navigate(typeof(BooksPage), "https://www.anapioficeandfire.com/api/books?page=1&pageSize=50");
        }
    }
}
