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
    class BooksVM : ViewModelBase
    {
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            String link = ((String)parameter).Replace("%3D", "=").Replace("%26", "&");
            var service = new ThroneService();
            var obj = await service.GetBooksAsync(link);
            List<Book> books = (List<Book>)obj[0];
            foreach (Book item in books)
            {
                Books.Add(item);
            }

            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        public void NavigateToBookDetails(String bookURL)
        {
            NavigationService.Navigate(typeof(BookDetailsPage), bookURL);
        }
    }
}
