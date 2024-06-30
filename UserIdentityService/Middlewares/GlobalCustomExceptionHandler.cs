namespace UserIdentityService.Middlewares
{
    public class GlobalCustomExceptionHandler
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public GlobalCustomExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// exception handler method as extension method fires once exception founded.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;
            ExceptionResponse exResult = new ExceptionResponse();

            switch (exception)
            {
                case ApplicationException ex:
                    exResult.ResponseCode = (int)HttpStatusCode.BadRequest;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    exResult.ResponseMessage = exception.Message;
                    break;

                default:
                    exResult.ResponseCode = (int)HttpStatusCode.InternalServerError;
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    exResult.ResponseMessage = exception.Message;
                    break;
            }
            var exResponse = JsonConvert.SerializeObject(exResult);
            await context.Response.WriteAsync(exResponse);
        }
    }
}
