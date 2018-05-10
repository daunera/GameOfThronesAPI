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

        /// <summary>
        /// When navigated to this page, set the combobox lists, request the books list
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="mode"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            FromDateList.Add("Whatever");
            ToDateList.Add("Whatever");
            for (int year = 1996; year < DateTime.Now.Year; year++)
            {
                FromDateList.Add(year.ToString());
                ToDateList.Add(year.ToString());
            }

            //When we navigate from hamburger menu, we have to decode some extra char
            String link = ((String)parameter).Replace("%3D", "=").Replace("%26", "&");
            SetBooksList(link);
            await base.OnNavigatedToAsync(parameter, mode, state);
        }

        /// <summary>
        /// Navigate to a book detailed page
        /// </summary>
        /// <param name="bookURL">book's url</param>
        public void NavigateToBookDetails(String bookURL)
        {
            NavigationService.Navigate(typeof(BookDetailsPage), bookURL);
        }

        /// <summary>
        /// Refresh the books list
        /// </summary>
        /// <param name="parameter"></param>
        public void LoadPage(String parameter)
        {
            Books.Clear();
            SetBooksList(parameter);
        }

        /// <summary>
        /// Set the filtered url address based on paramters, with some error handling
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
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

        /// <summary>
        /// Request the url predicted books list, then add to the list property, 
        /// if no internet connection redirect to mainpage
        /// </summary>
        /// <param name="url"></param>
        private async void SetBooksList(String url)
        {
            try
            {
                var service = new ThroneService();
                PagedResponse<List<Book>> obj = await service.GetBooksAsync(url);
                if (obj.result != null)
                {
                    List<Book> books = obj.result;
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
