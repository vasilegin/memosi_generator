using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MemesApi;
using MemesApi.Db;
using MemesApi.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

namespace IntegrationTests;

public class ApiTests
{
	private const string ApiUrl = "http://127.0.0.1:9999/api/images";
	
	private const string EstimateUrl = "estimate/{0}";
	private const string GetNextUrl = "next";

	private static readonly Dictionary<string, string> TestConfiguration = new()
	{
		{"API_URL", "http://127.0.0.1:9999"},
		{"CONNECTION_STRING", "Data Source=Tests.db;"}
	};
	
	private readonly Random _random = new();

	private WebApplicationFactory<Program> _application = null!;
	private MemeContext _db = null!;
	private HttpClient _client = null!;


	[SetUp]
	public void SetupApplication()
	{
		_application = new WebApplicationFactory<Program>()
			
			.WithWebHostBuilder(builder =>
			{
				builder.ConfigureServices(services =>
				{
					var sp = services.BuildServiceProvider();
					using var scope = sp.CreateScope();
					var db = scope.ServiceProvider.GetService<MemeContext>()!;
					db.Database.EnsureDeleted();
					
				});
				
				builder.ConfigureAppConfiguration((context, configurationBuilder) =>
				{
					configurationBuilder.AddInMemoryCollection(TestConfiguration);
				});
				
			});

		var scope = _application.Services.CreateScope();
		_db = scope.ServiceProvider.GetService<MemeContext>()!;
		_client = _application.CreateClient();
	}
	[Test]
	public async Task Estimate_NotFound()
	{
		var request = new EstimateRequest(10, Guid.NewGuid().ToString());
		var randomImageId = _random.Next(1000000, 5000000);

		var response = await _client.PostAsJsonAsync(string.Format(EstimateUrl, randomImageId), request);

		Assert.False(response.IsSuccessStatusCode);
		Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
	}
}