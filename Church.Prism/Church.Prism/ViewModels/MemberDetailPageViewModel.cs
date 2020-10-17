using Church.Common.Responses;
using Prism.Navigation;

namespace Church.Prism.ViewModels
{
    public class MemberDetailPageViewModel : ViewModelBase
    {
        private UserResponse _member;

        public MemberDetailPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Member";
        }

        public UserResponse Member
        {
            get => _member;
            set => SetProperty(ref _member, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("member"))
            {
                Member = parameters.GetValue<UserResponse>("member");
                Title = Member.FullName;
            }
        }

    }
}