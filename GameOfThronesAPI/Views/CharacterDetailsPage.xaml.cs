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
    public sealed partial class CharacterDetailsPage : Page
    {
        public CharacterDetailsPage()
        {
            this.InitializeComponent();
        }

        private void FatherLink_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            CharacterDetailViewModel.NavigateToCharacterDetails(CharacterDetailViewModel.Father.Url);
        }

        private void MotherLink_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            CharacterDetailViewModel.NavigateToCharacterDetails(CharacterDetailViewModel.Mother.Url);
        }

        private void SpouseLink_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            CharacterDetailViewModel.NavigateToCharacterDetails(CharacterDetailViewModel.Spouse.Url);
        }

        private void House_OnClick(object sender, ItemClickEventArgs e)
        {
            var house = (House)e.ClickedItem;
            CharacterDetailViewModel.NavigateToHouseDetails(house.Url);
        }

        private void Book_OnClick(object sender, ItemClickEventArgs e)
        {
            var book = (Book)e.ClickedItem;
            CharacterDetailViewModel.NavigateToBookDetails(book.Url);
        }
    }
}
