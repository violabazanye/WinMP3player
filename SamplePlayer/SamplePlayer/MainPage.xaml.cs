using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SamplePlayer.Resources;
using Microsoft.Phone.BackgroundAudio;

namespace SamplePlayer
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();

            BackgroundAudioPlayer.Instance.PlayStateChanged += new EventHandler(Instance_PlayStateChanged);
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (PlayState.Playing == BackgroundAudioPlayer.Instance.PlayerState)
            {
                playButton.Content = "pause";
                txtCurrentTrack.Text = BackgroundAudioPlayer.Instance.Track.Title +
                                 " by " +
                                 BackgroundAudioPlayer.Instance.Track.Artist;

            }
            else
            {
                playButton.Content = "play";
                txtCurrentTrack.Text = "";
            }
        }

        #region Button Click Event Handlers

        private void prevButton_Click(object sender, RoutedEventArgs e)
        {
            BackgroundAudioPlayer.Instance.SkipPrevious();
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlayState.Playing == BackgroundAudioPlayer.Instance.PlayerState)
            {
                BackgroundAudioPlayer.Instance.Pause();
            }
            else
            {
                BackgroundAudioPlayer.Instance.Play();
            }
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            BackgroundAudioPlayer.Instance.SkipNext();
        }

        #endregion Button Click Event Handlers

        void Instance_PlayStateChanged(object sender, EventArgs e)
        {
            switch (BackgroundAudioPlayer.Instance.PlayerState)
            {
                case PlayState.Playing:
                    playButton.Content = "pause";
                    break;

                case PlayState.Paused:
                case PlayState.Stopped:
                    playButton.Content = "play";
                    break;
            }

            if (null != BackgroundAudioPlayer.Instance.Track)
            {
                txtCurrentTrack.Text = BackgroundAudioPlayer.Instance.Track.Title +
                                       " by " +
                                       BackgroundAudioPlayer.Instance.Track.Artist;
            }
        }
    }
}