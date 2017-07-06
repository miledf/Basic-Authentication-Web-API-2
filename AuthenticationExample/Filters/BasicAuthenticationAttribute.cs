using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace AuthenticationExample.Filters
{
    public class BasicAuthenticationAttribute : IAuthenticationFilter
    {
        public string Realm { get; set; } = "ApiTeste";

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var request = context.Request;
            var authorization = request.Headers.Authorization;

            if (authorization == null)
            {
                return;
            }

            if (authorization.Scheme != "Basic")
            {
                return;
            }

            if (String.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Favor preencher as credenciais.", request);
                return;
            }

            var userNameAndPasword = HeaderHelper.ExtractUserNameAndPassword(authorization.Parameter);

            if (userNameAndPasword == null ||
                string.IsNullOrWhiteSpace(userNameAndPasword.Item1) ||
                string.IsNullOrWhiteSpace(userNameAndPasword.Item2))
            {
                context.ErrorResult = new AuthenticationFailureResult("credenciais inválidas.", request);
                return;
            }

            var userName = userNameAndPasword.Item1;
            var password = userNameAndPasword.Item2;

            var principal = await AuthenticateAsync(userName, password, cancellationToken);
            if (principal == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("usuario / senha invalidos.", request);
            }
            else
            {
                context.Principal = principal;
            }
        }

        private async Task<IPrincipal> AuthenticateAsync(string userName, string password, 
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (userName != "teste" || password != "teste")
            {
                return null;
            }

            var nameClaim = new Claim(ClaimTypes.Name, userName);
            var claims = new List<Claim> { nameClaim };
            var identity = new ClaimsIdentity(claims, "Custom");

            var principal = new ClaimsPrincipal(identity);
            return principal;
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            Challenge(context);
            return Task.FromResult(0);
        }

        private void Challenge(HttpAuthenticationChallengeContext context)
        {
            string parameter;

            if (String.IsNullOrEmpty(Realm))
            {
                parameter = null;
            }
            else
            {
                parameter = "realm=\"" + Realm + "\"";
            }

            context.ChallengeWith("Basic", parameter);
        }

        public virtual bool AllowMultiple
        {
            get { return false; }
        }
    }
}