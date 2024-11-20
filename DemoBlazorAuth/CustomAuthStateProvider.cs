using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace DemoBlazorAuth
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private AuthenticationState _authenticationState;

        public CustomAuthStateProvider(CustomAuthenticationService customAuthenticationService)
        {
            _authenticationState = new AuthenticationState(customAuthenticationService.CurrentUser);

            customAuthenticationService.UserChanged += (newUser) =>
            {
                _authenticationState = new AuthenticationState(newUser);
                NotifyAuthenticationStateChanged(Task.FromResult(_authenticationState));
            };
        }
        
        public override Task<AuthenticationState> GetAuthenticationStateAsync() => 
            Task.FromResult(_authenticationState);

        //public void AuthenticateUser(string userIdentifier)
        //{
        //    var identity = new ClaimsIdentity(new Claim[]
        //    {
        //        new Claim(ClaimTypes.Name, userIdentifier),
        //    }, "Custom Authentication");

        //    var user = new ClaimsPrincipal(identity);

        //    NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        //}
    }
}
