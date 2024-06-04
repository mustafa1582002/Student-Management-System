﻿using HandwrittenTextRecognitionSystem.Errors;
using System.Net;
using System.Text.Json;

namespace HandwrittenTextRecognitionSystem.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate Next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = Next;
            _logger = logger;
            _env = env;
        }
        public async void Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                //Production => Log In Db
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;//we can replace the after qual by 500
                //if(_env.IsDevelopment())
                //{
                //    var Response = new ApiExceptionResponse(500,ex.Message,ex.StackTrace.ToString());
                //}
                //else
                //{
                //    var Response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
                //}
                var Response = _env.IsDevelopment() ?
                                new ApiExceptionResponse(500, ex.Message, ex.StackTrace.ToString())
                                : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
                var Options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var JsonResponse = JsonSerializer.Serialize(Response, Options);
                context.Response.WriteAsync(JsonResponse);
            }
        }

    }
}
