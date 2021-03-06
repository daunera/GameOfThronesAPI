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
    class BookDetailsVM : ViewModelBase
    {
        private Book _book;

        public Book Book
        {
            get { return _book; }
            set { Set(ref _book, value); }
        }

        public ObservableCollection<Character> PovCharacters { get; set; } = new ObservableCollection<Character>();
        public ObservableCollection<Character> Characters { get; set; } = new ObservableCollection<Character>();

        /// <summary>
        /// When navigated to the page, gets the book details via api request, and then some url-base data requested too
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="mode"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            try
            {
                var bookUrl = (string)parameter;
                var service = new ThroneService();
                Book = await service.GetBookAsync(bookUrl);

                if (Book.PovCharacters.Length != 0)
                {
                    PovCharacters.Clear();
                    foreach (String url in Book.PovCharacters)
                    {
                        Character newCharacter = await service.GetCharacterAsync(url);
                        PovCharacters.Add(newCharacter);
                    }
                }

                if (Book.Characters.Length != 0)
                {
                    Characters.Clear();
                    foreach (String url in Book.Characters)
                    {
                        Character newCharacter = await service.GetCharacterAsync(url);
                        Characters.Add(newCharacter);
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
        /// Navigate to another character page
        /// </summary>
        /// <param name="bookURL">the character's url id</param>
        public void NavigateToCharacterDetails(String bookURL)
        {
            NavigationService.Navigate(typeof(CharacterDetailsPage), bookURL);
        }

    }
}
