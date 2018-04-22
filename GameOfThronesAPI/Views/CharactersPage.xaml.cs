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

    public sealed partial class CharactersPage : Page
    {
        public CharactersPage()
        {
            this.InitializeComponent();
        }

        private void CharacterList_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var character = (Character)e.ClickedItem;
            CharactersViewModel.NavigateToCharacterDetails(character.Url);
        }

        private void Last_Click(object sender, RoutedEventArgs e)
        {
            if (CharactersViewModel.Buttons.Last == null) { }
            else
                CharactersViewModel.LoadPage(CharactersViewModel.Buttons.Last);
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (CharactersViewModel.Buttons.Next == null) { }
            else
                CharactersViewModel.LoadPage(CharactersViewModel.Buttons.Next);
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            if (CharactersViewModel.Buttons.Prev == null) { }
            else
                CharactersViewModel.LoadPage(CharactersViewModel.Buttons.Prev);
        }

        private void First_Click(object sender, RoutedEventArgs e)
        {
            if (CharactersViewModel.Buttons.First == null) { }
            else
                CharactersViewModel.LoadPage(CharactersViewModel.Buttons.First);
        }
    }
}
