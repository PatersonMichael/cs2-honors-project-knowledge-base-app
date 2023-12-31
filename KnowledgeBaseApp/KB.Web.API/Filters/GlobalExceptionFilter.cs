﻿using KB.Common.Exceptions;
using KB.Web.API.DtoModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using KB.Common.Exceptions;

namespace GT.Web.Api.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context == null)
            {
                return;
            }

            var exception = context.Exception;
            _logger.LogError(exception.Message);

            var statusCode = exception switch
            {
                ArgumentNullException _ => StatusCodes.Status400BadRequest,
                ArgumentOutOfRangeException _ => StatusCodes.Status400BadRequest,
                ArgumentException _ => StatusCodes.Status400BadRequest,
                BadRequestException _ => StatusCodes.Status400BadRequest,
                NotFoundException _ => StatusCodes.Status404NotFound,
                ConflictException _ => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            var response = context.HttpContext.Response;
            response.StatusCode = statusCode;
            response.ContentType = "application/json";

            var errorDetails = new ErrorDetails
            {
                Message = exception.Message,
                StatusCode = statusCode
            };

            var json = JsonSerializer.Serialize<ErrorDetails>(errorDetails, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            response.WriteAsync(json);
        }
    }
}