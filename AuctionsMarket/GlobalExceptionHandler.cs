using AuctionsMarket.Exceptions;
using DTO.ErrorDTO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace HumanResourceProject
{
    

    public class GlobalExceptionHandler
    {
        public static void ConfigureExceptionHandler(IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";

                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                    if (exceptionHandlerPathFeature?.Error != null)
                    {
                        var response = HandleException(exceptionHandlerPathFeature.Error, exceptionHandlerPathFeature);
                        
                        context.Response.StatusCode = response.StatusCode;
                        await context.Response.WriteAsync(response.ToString());
                    }
                });
            });
        }

        private static ErrorDTO HandleException(Exception exception, IExceptionHandlerPathFeature? exceptionHandlerPathFeature )
        {
            Log.Error($"An error occurred: {exceptionHandlerPathFeature.Error}");

            // Handle different types of exceptions
            if (exception is ArgumentException argumentException)
            {
                return new ErrorDTO
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Bad Request: " + argumentException.Message
                };
            }
            else if (exception is BidException bidTooLowException)
            {
                return new ErrorDTO
                {
                    StatusCode = StatusCodes.Status400BadRequest, // Or any other appropriate status code
                    Message = "Bid Error: " + bidTooLowException.Message
                };
            }
            else if (exception is TransactionException transactionException)
            {
                return new ErrorDTO
                {
                    StatusCode = StatusCodes.Status400BadRequest, // Or any other appropriate status code
                    Message = "Transaction Error: " + transactionException.Message
                };
            }
            else if (exception is System.IO.IOException ioException)
            {
                return new ErrorDTO
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "IO Error: " + ioException.Message
                };
            }
            else if (exception is System.Net.Http.HttpRequestException httpRequestException)
            {
                return new ErrorDTO
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Connection Error: " + httpRequestException.Message
                };
            }
           
            else
            {
                // Default error handling for other exceptions
                return new ErrorDTO
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Internal Server Error"
                };
            }
        }
    }
}

