using Church.Common.Responses;
using Church.Common.Services;
using Church.Prism.ItemViewModels;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;
namespace Church.Prism.ViewModels
{
    public class MembersPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private ObservableCollection<MemberItemViewModel> _members;
        private bool _isRunning;
        private string _search;
        private List<UserResponse> _member;
        private DelegateCommand _searchCommand;
        public MembersPageViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Members";
            LoadMembersAsync();
        }
        public ObservableCollection<MemberItemViewModel> Members
        {
            get => _members;
            set => SetProperty(ref _members, value);
        }
        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(ShowMembers));
        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                ShowMembers();
            }
        }
        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }
        private async void LoadMembersAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Check the internet connection.", "Accept");
                return;
            }
            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<UserResponse>(
                url,
                "/api",
                "/Members");
            IsRunning = false;
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }
            _member = (List<UserResponse>)response.Result;
            ShowMembers();
        }
        private void ShowMembers()
        {
            if (string.IsNullOrEmpty(Search))
            {
                Members = new ObservableCollection<MemberItemViewModel>(_member.Select(p => new MemberItemViewModel(_navigationService)
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Document = p.Document,
                    Email = p.Email,
                    Address = p.Address,
                    PhoneNumber = p.PhoneNumber,
                    ImageId = p.ImageId
                })
                   .ToList());
            }
            else
            {
                Members = new ObservableCollection<MemberItemViewModel>(_member.Select(p => new MemberItemViewModel(_navigationService)
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Document = p.Document,
                    Email = p.Email,
                    Address = p.Address,
                    PhoneNumber = p.PhoneNumber,
                    ImageId = p.ImageId

                })
                   .Where(p => p.FirstName.ToLower().Contains(Search.ToLower()))
                   .ToList());
            }
        }
    }
}