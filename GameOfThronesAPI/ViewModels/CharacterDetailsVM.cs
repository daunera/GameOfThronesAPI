﻿using GameOfThronesAPI.Exceptions;
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
    class CharacterDetailsVM : ViewModelBase
    {
        private Character _character;
        public Character Character
        {
            get { return _character; }
            set { Set(ref _character, value); }
        }

        private Character _father;
        public Character Father
        {
            get { return _father; }
            set { Set(ref _father, value); }
        }

        private Character _mother;
        public Character Mother
        {
            get { return _mother; }
            set { Set(ref _mother, value); }
        }

        private Character _spouse;
        public Character Spouse
        {
            get { return _spouse; }
            set { Set(ref _spouse, value); }
        }


        public ObservableCollection<House> Allies { get; set; } = new ObservableCollection<House>();
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();
        public ObservableCollection<Book> PovBooks { get; set; } = new ObservableCollection<Book>();

        /// <summary>
        /// When navigate to this page, request an api based on the given url
        /// If this character has url-based detail, requests those too
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="mode"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            try
            {
                var characterUrl = (string)parameter;
                var service = new ThroneService();
                Character = await service.GetCharacterAsync(characterUrl);
                Father = await service.GetCharacterAsync(Character.Father);
                Mother = await service.GetCharacterAsync(Character.Mother);
                Spouse = await service.GetCharacterAsync(Character.Spouse);

                if (Character.Allegiances.Length != 0)
                {
                    Allies.Clear();
                    foreach (String url in Character.Allegiances)
                    {
                        House newHouse = await service.GetHouseAsync(url);
                        Allies.Add(newHouse);
                    }
                }

                if (Character.Books.Length != 0)
                {
                    Books.Clear();
                    foreach (String url in Character.Books)
                    {
                        Book newBook = await service.GetBookAsync(url);
                        Books.Add(newBook);
                    }
                }

                if (Character.PovBooks.Length != 0)
                {
                    PovBooks.Clear();
                    foreach (String url in Character.PovBooks)
                    {
                        Book newBook = await service.GetBookAsync(url);
                        PovBooks.Add(newBook);
                    }
                }

                await base.OnNavigatedToAsync(parameter, mode, state);
            }
            catch (RedirectMainException)
            {
                NavigationService.Navigate(typeof(MainPage));
            }
        }

        /// <summary>
        /// Navigate to the given house url details page
        /// </summary>
        /// <param name="houseURL">house's url</param>
        public void NavigateToHouseDetails(String houseURL)
        {
            NavigationService.Navigate(typeof(HouseDetailsPage), houseURL);
        }

        /// <summary>
        /// Navigate to the given character url details page
        /// </summary>
        /// <param name="characterURL">character's url</param>
        public void NavigateToCharacterDetails(String characterURL)
        {
            NavigationService.Navigate(typeof(CharacterDetailsPage), characterURL);
        }

        /// <summary>
        /// Navigate too the given book url details page
        /// </summary>
        /// <param name="bookURL">book's url</param>
        public void NavigateToBookDetails(String bookURL)
        {
            NavigationService.Navigate(typeof(BookDetailsPage), bookURL);
        }
    }
}
