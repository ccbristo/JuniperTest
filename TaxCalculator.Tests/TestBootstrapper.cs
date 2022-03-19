using Microsoft.Extensions.Configuration;
using Xunit;

namespace TaxCalculator.Tests;

public class ConfigFixture
{
    static ConfigFixture()
    {
        Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.unittests.json")
            .Build();
    }

    public static readonly IConfigurationRoot Configuration;

}

[CollectionDefinition(Name)]
public class ConfigCollection : ICollectionFixture<ConfigFixture>
{
    public const string Name = "Config Collection";
}