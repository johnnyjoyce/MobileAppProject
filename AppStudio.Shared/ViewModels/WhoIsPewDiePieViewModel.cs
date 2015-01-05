using System;
using System.Windows;
using System.Windows.Input;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class WhoIsPewDiePieViewModel : ViewModelBase<WhoIsPewDiePieSchema>
    {
        private RelayCommandEx<WhoIsPewDiePieSchema> itemClickCommand;
        public RelayCommandEx<WhoIsPewDiePieSchema> ItemClickCommand
        {
            get
            {
                if (itemClickCommand == null)
                {
                    itemClickCommand = new RelayCommandEx<WhoIsPewDiePieSchema>(
                        (item) =>
                        {
                            NavigationServices.NavigateToPage("WhoIsPewDiePieDetail", item);
                        });
                }

                return itemClickCommand;
            }
        }

        override protected DataSourceBase<WhoIsPewDiePieSchema> CreateDataSource()
        {
            return new WhoIsPewDiePieDataSource(); // CollectionDataSource
        }


        override public Visibility PinToStartVisibility
        {
            get { return ViewType == ViewTypes.Detail ? Visibility.Visible : Visibility.Collapsed; }
        }

        override protected void PinToStart()
        {
            base.PinToStart("WhoIsPewDiePieDetail", "{DefaultTitle}", "{DefaultSummary}", "{DefaultImageUrl}");
        }

        override public Visibility ShareItemVisibility
        {
            get { return ViewType == ViewTypes.Detail ? Visibility.Visible : Visibility.Collapsed; }
        }

        override public Visibility TextToSpeechVisibility
        {
            get { return ViewType == ViewTypes.Detail ? Visibility.Visible : Visibility.Collapsed; }
        }

        override protected async void TextToSpeech()
        {
            await base.SpeakText("Description");
        }

        public RelayCommandEx<Slider> IncreaseSlider
        {
            get
            {
                return new RelayCommandEx<Slider>(s => s.Value++);
            }
        }

        public RelayCommandEx<Slider> DecreaseSlider
        {
            get
            {
                return new RelayCommandEx<Slider>(s => s.Value--);
            }
        }

        override public void NavigateToSectionList()
        {
            NavigationServices.NavigateToPage("WhoIsPewDiePieList");
        }

        override protected void NavigateToSelectedItem()
        {
            NavigationServices.NavigateToPage("WhoIsPewDiePieDetail");
        }
    }
}
