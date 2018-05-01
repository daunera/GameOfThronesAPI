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
    class BooksVM : ViewModelBase
    {
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();

        public List<String> FromDateList { get; set; } = new List<string>();
        public List<String> ToDateList { get; set; } = new List<string>();

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            FromDateList.Add("Whatever");
            ToDateList.Add("Whatever");
            for (int year = 1996; year < DateTime.Now.Year; year++)
            {
                FromDateList.Add(year.ToString());
                ToDateList.Add(year.ToString());
            }

            String link = ((String)parameter).Replace("%3D", "=").Replace("%26", "&");
            SetBooksList(link);
            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        public void NavigateToBookDetails(String bookURL)
        {
            NavigationService.Navigate(typeof(BookDetailsPage), bookURL);
        }

        public void LoadPage(String parameter)
        {
            Books.Clear();
            SetBooksList(parameter);
        }

        public void LoadFilteredPage(string fromDate, string toDate)
        {
            String baseUrl = "https://www.anapioficeandfire.com/api/books?";
            if (fromDate != null)
            {
                if (!fromDate.Equals("Whatever"))
                {
                    baseUrl += "fromReleaseDate=" + fromDate + "-01-01T00:00:00&";
                }
            }
            if (toDate != null)
            {
                if (!toDate.Equals("Whatever"))
                {
                    baseUrl += "toReleaseDate=" + toDate + "-12-31T00:00:00&";
                }
            }

            LoadPage(baseUrl + "pageSize=50");
        }

        private async void SetBooksList(String url)
        {
            try
            {
                var service = new ThroneService();
                var obj = await service.GetBooksAsync(url);
                if (obj[0] != null)
                {
                    List<Book> books = (List<Book>)obj[0];
                    foreach (Book item in books)
                    {
                        Books.Add(item);
                    }
                }
            }
            catch (RedirectMainException)
            {
                NavigationService.Navigate(typeof(MainPage));
            }
        }
    }
}
