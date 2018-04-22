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
    public sealed partial class BookDetailsPage : Page
    {
        public BookDetailsPage()
        {
            this.InitializeComponent();
        }

        private void Character_OnClick(object sender, ItemClickEventArgs e)
        {
            var character = (Character)e.ClickedItem;
            BookDetailViewModel.NavigateToCharacterDetails(character.Url);
        }
    }
}
