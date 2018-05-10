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

        /// <summary>
        /// When navigate to this page, start a request for the houses list
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="mode"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            String link = ((String)parameter).Replace("%3D", "=").Replace("%26", "&");
            SetHousesList(link);
            await base.OnNavigatedToAsync(parameter, mode, state);
        }


        /// <summary>
        /// Navigate to a house's details page
        /// </summary>
        /// <param name="houseURL">house's url</param>
        public void NavigateToHouseDetails(String houseURL)
        {
            NavigationService.Navigate(typeof(HouseDetailsPage),houseURL);
        }

        /// <summary>
        /// Refresh the list property
        /// </summary>
        /// <param name="parameter"></param>
        public void LoadPage(String parameter)
        {
            Houses.Clear();
            SetHousesList(parameter);
        }

        /// <summary>
        /// Create the filtered url for request based on the given parameters
        /// </summary>
        /// <param name="words"></param>
        /// <param name="titles"></param>
        /// <param name="seats"></param>
        /// <param name="died"></param>
        /// <param name="weapons"></param>
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

            LoadPage(baseUrl + "pageSize=50");
        }


        /// <summary>
        /// Set the list with houses from the response
        /// </summary>
        /// <param name="url"></param>
        private async void SetHousesList(String url)
        {
            try
            {
                var service = new ThroneService();
                PagedResponse<List<House>> obj = await service.GetHousesAsync(url);
                if (obj.result != null)
                {
                    List<House> houses = obj.result;
                    foreach (House item in houses)
                    {
                        Houses.Add(item);
                    }
                }

                Buttons = obj.links;
            }
            catch (RedirectMainException)
            {
                NavigationService.Navigate(typeof(MainPage));
            }

        }
    }
}
