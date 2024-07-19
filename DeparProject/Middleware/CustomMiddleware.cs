using DeparProject.Interfaces;
using DeparProject.Services;

namespace DeparProject.Middleware
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        private IEmailServiceSender _emailService;
        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
            //this.signInManager = signInManager;
            //this.userManger = userManger;
        }

        public async Task Invoke(HttpContext context, IEmailServiceSender emailService)
        {
            
            this._emailService = emailService;

            await _next.Invoke(context);
        }
    }
}
