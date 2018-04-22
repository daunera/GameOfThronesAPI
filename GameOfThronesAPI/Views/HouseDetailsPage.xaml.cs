using GameOfThronesAPI.Models;
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
    public sealed partial class HouseDetailsPage : Page
    {
        public HouseDetailsPage()
        {
            this.InitializeComponent();
        }

        private void House_OnClick(object sender, ItemClickEventArgs e)
        {
            var house = (House)e.ClickedItem;
            HouseDetailViewModel.NavigateToHouseDetails(house.Url);
        }

        private void Character_OnClick(object sender, ItemClickEventArgs e)
        {
            var character = (Character)e.ClickedItem;
            HouseDetailViewModel.NavigateToCharacterDetails(character.Url);
        }

        private void CurrentLordLink_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            HouseDetailViewModel.NavigateToCharacterDetails(HouseDetailViewModel.CurrentLord.Url);
        }

        private void HeirLink_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            HouseDetailViewModel.NavigateToCharacterDetails(HouseDetailViewModel.Heir.Url);
        }

        private void OverlordLink_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            HouseDetailViewModel.NavigateToHouseDetails(HouseDetailViewModel.Overlord.Url);
        }

        private void FounderLink_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            HouseDetailViewModel.NavigateToCharacterDetails(HouseDetailViewModel.Founder.Url);
        }
    }
}
