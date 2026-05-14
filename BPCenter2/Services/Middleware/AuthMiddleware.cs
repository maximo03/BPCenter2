using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace BPCenter2.Services.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate next;

        public static IDictionary<Guid, ClaimsPrincipal> Logins { get; set; } = new Dictionary<Guid, ClaimsPrincipal>();

        public AuthMiddleware(RequestDelegate next)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == "/AccountLogin" && context.Request.Query.ContainsKey("key"))
            {
                var key = Guid.Parse(context.Request.Query["key"]!);
                var claim = Logins[key];
                await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claim);
                context.Response.Redirect("/");
            }
            else
            {
                await next(context);
            }

        }
    }
}
