using System.Security.Claims;

namespace Pets_Project_Backend.CustomMiddilWare
{
    public class UserIdMiddleware
    {

        private readonly RequestDelegate _next;
  

        public UserIdMiddleware(RequestDelegate next)
        {
            _next = next;
            
        }


        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                await _next(context);
                return;
            }

            if(context.User.Identity?.IsAuthenticated==true)
            {
                var UserIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

                if (UserIdClaim != null)
                {
                    context.Items["Id"]=UserIdClaim.Value;
                }
            }

            await _next(context);
        }
    }
}
