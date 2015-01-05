using System;
using System.Net.NetworkInformation;

using Windows.ApplicationModel.DataTransfer;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using AppStudio.Services;
using AppStudio.ViewModels;

namespace AppStudio.Views
{
    public sealed partial class WhoIsPewDiePieDetail : Page
    {
        private NavigationHelper _navigationHelper;

        private DataTransferManager _dataTransferManager;

        public WhoIsPewDiePieDetail()
        {
            this.InitializeComponent();
            _navigationHelper = new NavigationHelper(this);

            WhoIsPewDiePieModel = new WhoIsPewDiePieViewModel();

            ApplicationView.GetForCurrentView().
                SetDesiredBoundsMode(ApplicationViewBoundsMode.UseVisible);
        }

        public WhoIsPewDiePieViewModel WhoIsPewDiePieModel { get; private set; }

        public NavigationHelper NavigationHelper
        {
            get { return _navigationHelper; }
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += OnDataRequested;

            _navigationHelper.OnNavigatedTo(e);

            await WhoIsPewDiePieModel.LoadItemsAsync();
            WhoIsPewDiePieModel.SelectItem(e.Parameter);

            if (WhoIsPewDiePieModel != null)
            {
                WhoIsPewDiePieModel.ViewType = ViewTypes.Detail;
            }
            DataContext = this;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedFrom(e);
            _dataTransferManager.DataRequested -= OnDataRequested;
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            if (WhoIsPewDiePieModel != null)
            {
                WhoIsPewDiePieModel.GetShareContent(args.Request);
            }
        }
    }
}
