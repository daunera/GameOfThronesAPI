using GameOfThronesAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Template10.Services.NavigationService;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace GameOfThronesAPI.Views
{
    public sealed partial class BooksPage : Page
    {
        public BooksPage()
        {
            this.InitializeComponent();
        }

        private void BookList_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var book = (Book)e.ClickedItem;
            BooksViewModel.NavigateToBookDetails(book.Url);
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            Grid parentGrid = (Grid)((Button)sender).Parent;
            object fromDate = ((ComboBox)parentGrid.FindName("FromDateCombo")).SelectionBoxItem;
            object toDate = ((ComboBox)parentGrid.FindName("ToDateCombo")).SelectionBoxItem;

            BooksViewModel.LoadFilteredPage((string)fromDate, (string)toDate);
        }

        private void Filter_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
    }
}
