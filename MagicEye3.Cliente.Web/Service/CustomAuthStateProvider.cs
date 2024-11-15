using MagicEye3.Cliente.Web.Service.IService;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MagicEye3.Cliente.Web.Service
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ITokenProvider _tokenProvider;

        public CustomAuthStateProvider(ITokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        //public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        //{
        //    var token = await _tokenProvider.GetTokenAsync();

        //    if (string.IsNullOrEmpty(token))
        //    {
        //        // Usuario no autenticado
        //        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        //    }

        //    var handler = new JwtSecurityTokenHandler();
        //    var jwtToken = handler.ReadJwtToken(token);

        //    var identity = new ClaimsIdentity(jwtToken.Claims, "jwt", ClaimTypes.Name, ClaimTypes.Role);

        //    var user = new ClaimsPrincipal(identity);

        //    return new AuthenticationState(user);
        //}

        //el método de arriba funcionaba pero luego intente poner decoraciones para autorizar vistas
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _tokenProvider.GetTokenAsync();

            if (string.IsNullOrEmpty(token))
            {
                // Usuario no autenticado
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var identity = new ClaimsIdentity("jwt");

            // Mapear los claims manualmente
            foreach (var claim in jwtToken.Claims)
            {
                if (claim.Type == "role" || claim.Type == ClaimTypes.Role)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, claim.Value));
                }
                else if (claim.Type == JwtRegisteredClaimNames.Email)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Email, claim.Value));
                }
                else
                {
                    identity.AddClaim(claim);
                }
            }

            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }


        //public void NotifyUserAuthentication(string token)
        //{
        //    var handler = new JwtSecurityTokenHandler();
        //    var jwtToken = handler.ReadJwtToken(token);

        //    var identity = new ClaimsIdentity(jwtToken.Claims, "jwt", ClaimTypes.Name, ClaimTypes.Role);

        //    var user = new ClaimsPrincipal(identity);

        //    NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        //}
        public void NotifyUserAuthentication(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var identity = new ClaimsIdentity("jwt");

            // Mapear los claims manualmente (igual que arriba)
            foreach (var claim in jwtToken.Claims)
            {
                if (claim.Type == "role" || claim.Type == ClaimTypes.Role)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, claim.Value));
                }
                else if (claim.Type == JwtRegisteredClaimNames.Email)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Email, claim.Value));
                }
                else if (claim.Type == JwtRegisteredClaimNames.Sub)
                {
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, claim.Value));
                }
                else if (claim.Type == JwtRegisteredClaimNames.Name)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Name, claim.Value));
                }
                else
                {
                    identity.AddClaim(claim);
                }
            }

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void NotifyUserLogout()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
        }
    }

}
