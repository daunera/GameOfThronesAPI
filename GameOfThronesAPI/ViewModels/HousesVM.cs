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
    public class HousesVM : ViewModelBase
    {
        public ObservableCollection<House> Houses { get; set; } = new ObservableCollection<House>();

        public Linker Buttons { get; set; } = new Linker();

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            String link = ((String)parameter).Replace("%3D", "=").Replace("%26", "&");
            SetHousesList(link);
            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        public void NavigateToHouseDetails(String houseURL)
        {
            NavigationService.Navigate(typeof(HouseDetailsPage),houseURL);
        }

        public void LoadPage(String parameter)
        {
            Houses.Clear();
            SetHousesList(parameter);
        }

        public void LoadFilteredPage(int words, int titles, int seats, int died, int weapons)
        {
            String baseUrl = "https://www.anapioficeandfire.com/api/houses?";

            switch (words)
            {
                case 0:
                    baseUrl += "hasWords=true&";
                    break;
                case 2:
                    baseUrl += "hasWords=false&";
                    break;
                default:
                    break;
            }

            switch (titles)
            {
                case 0:
                    baseUrl += "hasTitles=true&";
                    break;
                case 2:
                    baseUrl += "hasTitles=false&";
                    break;
                default:
                    break;
            }

            switch (seats)
            {
                case 0:
                    baseUrl += "hasSeats=true&";
                    break;
                case 2:
                    baseUrl += "hasSeats=false&";
                    break;
                default:
                    break;
            }

            switch (died)
            {
                case 0:
                    baseUrl += "hasDiedOut=true&";
                    break;
                case 2:
                    baseUrl += "hasDiedOut=false&";
                    break;
                default:
                    break;
            }

            switch (weapons)
            {
                case 0:
                    baseUrl += "hasAncestralWeapons=true&";
                    break;
                case 2:
                    baseUrl += "hasAncestralWeapons=false&";
                    break;
                default:
                    break;
            }

            LoadPage(baseUrl);
        }

        private async void SetHousesList(String url)
        {
            try
            {
                var service = new ThroneService();
                var obj = await service.GetHousesAsync(url);
                if (obj[0] != null)
                {
                    List<House> houses = (List<House>)obj[0];
                    foreach (House item in houses)
                    {
                        Houses.Add(item);
                    }
                }

                Buttons = (Linker)obj[1];
            }
            catch (RedirectMainException e)
            {
                NavigationService.Navigate(typeof(MainPage));
            }

        }
    }
}
