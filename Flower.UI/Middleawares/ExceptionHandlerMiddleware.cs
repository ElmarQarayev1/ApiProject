﻿using System;
using Flower.UI.Exception;

namespace Flower.UI.Middleawares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpException hex)
            {
                if (hex.Status == System.Net.HttpStatusCode.Unauthorized)
                    context.Response.Redirect("/account/login");
                else
                    context.Response.Redirect("/home/error?message=" + hex.Message);
            }
            catch (System.Exception ex)
            {
                context.Response.Redirect("/home/error?message=" + ex.Message);
            }
        }
    }
}

