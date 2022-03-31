using HealthCareApi.Services;
using HealthCareApi.Services.Interfaces;

namespace HealthCareApi.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public JwtMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtService jwtService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtService.ValidateToken(token);
            if (userId != null)
            {
                context.Items["User"] = await userService.GetById(userId.Value);
            }

            await _requestDelegate(context);
        }
    }
}
