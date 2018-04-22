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

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var bookUrl = (string)parameter;
            var service = new ThroneService();
            Book = await service.GetBookAsync(bookUrl);

            if(Book.PovCharacters.Length != 0)
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

        public void NavigateToCharacterDetails(String bookURL)
        {
            NavigationService.Navigate(typeof(CharacterDetailsPage), bookURL);
        }

    }
}
