using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MemesApi.Controllers.Filters
{
	public class LoggingFilter: IActionFilter
	{
		private const string RequestLogTemplate = "REQUEST {Method} {Path} args: {Arguments}";
		private const string ResponseLogTemplate = "RESPONSE {HttpCode} {Method} {Path} result: {Response}";
		
		private readonly ILogger<LoggingFilter> _logger;

		public LoggingFilter(ILogger<LoggingFilter> logger)
		{
			_logger = logger;
		}
		public void OnActionExecuting(ActionExecutingContext context)
		{
			_logger.LogInformation(
				RequestLogTemplate, 
				context.HttpContext.Request.Method,
				context.HttpContext.Request.Path,
				context.ActionArguments);
		}
		public void OnActionExecuted(ActionExecutedContext context)
		{
			object? result = null;
			var statusCode = context.HttpContext.Response.StatusCode;
			
			if (context.Result is ObjectResult objectResult && objectResult.Value != null)
			{
				result = objectResult.Value;
			}
			if (context.Result is StatusCodeResult statusCodeResult)
			{
				statusCode = statusCodeResult.StatusCode;
			}
			_logger.LogInformation(
				ResponseLogTemplate,
				statusCode,
				context.HttpContext.Request.Method,
				context.HttpContext.Request.Path,
				result);
		}
	}
}