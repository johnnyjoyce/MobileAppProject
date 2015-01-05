using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.NetworkInformation;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class MainViewModel : BindableBase
    {
       private WhoIsPewDiePieViewModel _whoIsPewDiePieModel;
       private VideosViewModel _videosModel;
       private YoutubeViewModel _youtubeModel;
       private FacebookViewModel _facebookModel;
       private TwitterViewModel _twitterModel;
       private BingViewModel _bingModel;
        private PrivacyViewModel _privacyModel;

        private ViewModelBase _selectedItem = null;

        public MainViewModel()
        {
            _selectedItem = WhoIsPewDiePieModel;
            _privacyModel = new PrivacyViewModel();

        }
 
        public WhoIsPewDiePieViewModel WhoIsPewDiePieModel
        {
            get { return _whoIsPewDiePieModel ?? (_whoIsPewDiePieModel = new WhoIsPewDiePieViewModel()); }
        }
 
        public VideosViewModel VideosModel
        {
            get { return _videosModel ?? (_videosModel = new VideosViewModel()); }
        }
 
        public YoutubeViewModel YoutubeModel
        {
            get { return _youtubeModel ?? (_youtubeModel = new YoutubeViewModel()); }
        }
 
        public FacebookViewModel FacebookModel
        {
            get { return _facebookModel ?? (_facebookModel = new FacebookViewModel()); }
        }
 
        public TwitterViewModel TwitterModel
        {
            get { return _twitterModel ?? (_twitterModel = new TwitterViewModel()); }
        }
 
        public BingViewModel BingModel
        {
            get { return _bingModel ?? (_bingModel = new BingViewModel()); }
        }

        public void SetViewType(ViewTypes viewType)
        {
            WhoIsPewDiePieModel.ViewType = viewType;
            VideosModel.ViewType = viewType;
            YoutubeModel.ViewType = viewType;
            FacebookModel.ViewType = viewType;
            TwitterModel.ViewType = viewType;
            BingModel.ViewType = viewType;
        }

        public ViewModelBase SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                UpdateAppBar();
            }
        }

        public Visibility AppBarVisibility
        {
            get
            {
                return SelectedItem == null ? AboutVisibility : SelectedItem.AppBarVisibility;
            }
        }

        public Visibility AboutVisibility
        {

         get { return Visibility.Visible; }
        }

        public void UpdateAppBar()
        {
            OnPropertyChanged("AppBarVisibility");
            OnPropertyChanged("AboutVisibility");
        }

        /// <summary>
        /// Load ViewModel items asynchronous
        /// </summary>
        public async Task LoadDataAsync(bool forceRefresh = false)
        {
            var loadTasks = new Task[]
            { 
                WhoIsPewDiePieModel.LoadItemsAsync(forceRefresh),
                VideosModel.LoadItemsAsync(forceRefresh),
                YoutubeModel.LoadItemsAsync(forceRefresh),
                FacebookModel.LoadItemsAsync(forceRefresh),
                TwitterModel.LoadItemsAsync(forceRefresh),
                BingModel.LoadItemsAsync(forceRefresh),
            };
            await Task.WhenAll(loadTasks);
        }

        //
        //  ViewModel command implementation
        //
        public ICommand RefreshCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    await LoadDataAsync(true);
                });
            }
        }

        public ICommand AboutCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateToPage("AboutThisAppPage");
                });
            }
        }

        public ICommand PrivacyCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateTo(_privacyModel.Url);
                });
            }
        }
    }
}
