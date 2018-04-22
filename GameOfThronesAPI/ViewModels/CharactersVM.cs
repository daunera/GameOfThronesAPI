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
            var service = new ThroneService();
            var obj = await service.GetCharactersAsync(link);
            List<Character> characters = (List<Character>)obj[0];
            foreach (Character item in characters)
            {
                Characters.Add(item);
            }

            Buttons = (Linker)obj[1];

            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        public void NavigateToCharacterDetails(String characterURL)
        {
            NavigationService.Navigate(typeof(CharacterDetailsPage), characterURL);
        }

        public async void LoadPage(String parameter)
        {
            var service = new ThroneService();
            var obj = await service.GetCharactersAsync((String)parameter);
            List<Character> characters = (List<Character>)obj[0];
            Characters.Clear();
            foreach (Character item in characters)
            {
                Characters.Add(item);
            }

            Buttons = (Linker)obj[1];
        }
    }
}
