using System;
using System.Windows;
using System.Windows.Input;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class BingViewModel : ViewModelBase<BingSchema>
    {
        private RelayCommandEx<BingSchema> itemClickCommand;
        public RelayCommandEx<BingSchema> ItemClickCommand
        {
            get
            {
                if (itemClickCommand == null)
                {
                    itemClickCommand = new RelayCommandEx<BingSchema>(
                        (item) =>
                        {
                            string link = item.GetValue("Link");
                            if (!string.IsNullOrEmpty(link))
                            {
                                NavigationServices.NavigateTo(new Uri(item.GetValue("Link")));
                            }
                        });
                }

                return itemClickCommand;
            }
        }

        override protected DataSourceBase<BingSchema> CreateDataSource()
        {
            return new BingDataSource(); // BingDataSource
        }


        override public Visibility RefreshVisibility
        {
            get { return ViewType == ViewTypes.List ? Visibility.Visible : Visibility.Collapsed; }
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
            NavigationServices.NavigateToPage("BingList");
        }

        override protected void NavigateToSelectedItem()
        {
            var currentItem = GetCurrentItem();
            if (currentItem != null)
            {
                NavigationServices.NavigateTo(new Uri(currentItem.GetValue("Link")));
            }
        }
    }
}
