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
            var service = new ThroneService();
            var obj = await service.GetHousesAsync((String)parameter);
            List<House> houses = (List<House>) obj[0];
            foreach (House item in houses)
            {
                Houses.Add(item);
            }

            Buttons = (Linker)obj[1];

            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        public void NavigateToHouseDetails(String houseURL)
        {
            NavigationService.Navigate(typeof(HouseDetailsPage),houseURL);
        }

        public async void LoadPage(String parameter)
        {
            var service = new ThroneService();
            var obj = await service.GetHousesAsync(parameter);
            List<House> houses = (List<House>)obj[0];
            Houses.Clear();
            foreach (House item in houses)
            {
                Houses.Add(item);
            }

            Buttons = (Linker)obj[1];
        }
    }
}
