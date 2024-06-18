using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

SecretClientOptions options = new SecretClientOptions()
	{
		Retry =
		{
			Delay= TimeSpan.FromSeconds(2), 
			MaxDelay = TimeSpan.FromSeconds(16),
			MaxRetries = 5,
			Mode = RetryMode. Exponential
		}	
	};
var client = new SecretClient (new Uri("https://azure1key.vault.azure.net/"), new DefaultAzureCredential(), options);

KeyVaultSecret secret = client.GetSecret("<mysecret>");

string sValue =secret.Value;
app.MapGet("/", () => sValue);
app.Run();


﻿

