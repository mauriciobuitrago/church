﻿using Church.Common.Responses;
using Church.Prism.Views;
using Prism.Commands;
using Prism.Navigation;

namespace Church.Prism.ItemViewModels
{
    public class MemberItemViewModel : UserResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectMemberCommand;
        public MemberItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
         public DelegateCommand SelectMemberCommand => _selectMemberCommand ??
            (_selectMemberCommand = new DelegateCommand(SelectMemberAsync));

        private async void SelectMemberAsync()
        {
            NavigationParameters parameters = new NavigationParameters
                {
                    { "member", this }
                };

            await _navigationService.NavigateAsync(nameof(MemberDetailPage), parameters);
        }

    }
}