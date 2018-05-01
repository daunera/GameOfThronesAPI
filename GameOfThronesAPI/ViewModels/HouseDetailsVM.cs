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
    public class HouseDetailsVM : ViewModelBase
    {
        private House _house;

        public House House
        {
            get { return _house; }
            set { Set(ref _house, value); }
        }

        private Character _currentLord;

        public Character CurrentLord
        {
            get { return _currentLord; }
            set {Set (ref _currentLord,value); }
        }

        private Character _heir;

        public Character Heir
        {
            get { return _heir; }
            set { Set(ref _heir, value); }
        }

        private House _overlord;

        public House Overlord
        {
            get { return _overlord; }
            set { Set(ref _overlord, value); }
        }

        private Character _founder;

        public Character Founder
        {
            get { return _founder; }
            set { Set(ref _founder, value); }
        }

        public ObservableCollection<House> Cadets { get; set; } = new ObservableCollection<House>();
        public ObservableCollection<Character> Sworns { get; set; } = new ObservableCollection<Character>();

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            try
            {
                var houseUrl = (String)parameter;
                var service = new ThroneService();
                House = await service.GetHouseAsync(houseUrl);
                CurrentLord = await service.GetCharacterAsync(House.CurrentLord);
                Heir = await service.GetCharacterAsync(House.Heir);
                Overlord = await service.GetHouseAsync(House.Overlord);
                Founder = await service.GetCharacterAsync(House.Founder);

                if (House.CadetBranches.Length != 0)
                {
                    Cadets.Clear();
                    foreach (String url in House.CadetBranches)
                    {
                        House newHouse = await service.GetHouseAsync(url);
                        Cadets.Add(newHouse);
                    }
                }

                if (House.SwornMembers.Length != 0)
                {
                    Sworns.Clear();
                    foreach (String url in House.SwornMembers)
                    {
                        Character newGuy = await service.GetCharacterAsync(url);
                        Sworns.Add(newGuy);
                    }
                }

                await base.OnNavigatedToAsync(parameter, mode, state);
            }
            catch(RedirectMainException e)
            {
                NavigationService.Navigate(typeof(MainPage));
            }
        }

        public void NavigateToHouseDetails(String houseURL)
        {
            NavigationService.Navigate(typeof(HouseDetailsPage), houseURL);

        }

        public void NavigateToCharacterDetails(String characterURL)
        {
            NavigationService.Navigate(typeof(CharacterDetailsPage), characterURL);
        }

    }
}
