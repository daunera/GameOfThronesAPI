using System;
using Windows.UI.Xaml.Controls;

namespace GameOfThronesAPI.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        private void HousesButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            MainViewModel.NavigateToHouses();
        }

        private void BooksButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            MainViewModel.NavigateToBooks();
        }

        private void CharactersButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            MainViewModel.NavigateToCharacters();
        }
    }
}