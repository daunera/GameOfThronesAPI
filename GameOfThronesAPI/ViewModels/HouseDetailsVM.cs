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


        /// <summary>
        /// When navigated to this page, get the given house details
        /// </summary>
        /// <param name="parameter">url</param>
        /// <param name="mode"></param>
        /// <param name="state"></param>
        /// <returns></returns>
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
            catch(RedirectMainException)
            {
                NavigationService.Navigate(typeof(MainPage));
            }
        }

        /// <summary>
        /// Navigate to another house details page
        /// </summary>
        /// <param name="houseURL">other house's url</param>
        public void NavigateToHouseDetails(String houseURL)
        {
            NavigationService.Navigate(typeof(HouseDetailsPage), houseURL);

        }

        /// <summary>
        /// Navigate to another character details pge
        /// </summary>
        /// <param name="characterURL">character's url</param>
        public void NavigateToCharacterDetails(String characterURL)
        {
            NavigationService.Navigate(typeof(CharacterDetailsPage), characterURL);
        }

    }
}
