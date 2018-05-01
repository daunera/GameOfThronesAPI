using GameOfThronesAPI.Models;
using GameOfThronesAPI.Services;
using GameOfThronesAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

    public sealed partial class HousesPage : Page
    {
        public HousesPage()
        {
            this.InitializeComponent();
        }

        private void HouseList_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var house = (House)e.ClickedItem;
            HousesViewModel.NavigateToHouseDetails(house.Url);
        }

        private void Last_Click(object sender, RoutedEventArgs e)
        {
            if (HousesViewModel.Buttons.Last == null) { }
            else
                HousesViewModel.LoadPage(HousesViewModel.Buttons.Last);
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (HousesViewModel.Buttons.Next == null) { }
            else
                HousesViewModel.LoadPage(HousesViewModel.Buttons.Next);
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            if (HousesViewModel.Buttons.Prev == null) { }
            else
                HousesViewModel.LoadPage(HousesViewModel.Buttons.Prev);
        }

        private void First_Click(object sender, RoutedEventArgs e)
        {
            if (HousesViewModel.Buttons.First == null) { }
            else
                HousesViewModel.LoadPage(HousesViewModel.Buttons.First);
        }

        private void Filter_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            Grid parentGrid = (Grid)((Button)sender).Parent;
            int wordsState = ((ComboBox) parentGrid.FindName("WordsCombo")).SelectedIndex;
            int titlesState = ((ComboBox)parentGrid.FindName("TitlesCombo")).SelectedIndex;
            int seatsState = ((ComboBox)parentGrid.FindName("SeatsCombo")).SelectedIndex;
            int diedState = ((ComboBox)parentGrid.FindName("DiedOutCombo")).SelectedIndex;
            int weaponsState = ((ComboBox)parentGrid.FindName("WeaponsCombo")).SelectedIndex;

            HousesViewModel.LoadFilteredPage(wordsState, titlesState, seatsState, diedState, weaponsState);
        }
    }
}
