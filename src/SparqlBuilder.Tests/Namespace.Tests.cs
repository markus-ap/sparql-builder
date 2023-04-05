using System;
using FluentAssertions;
using SparqlBuilder;

namespace SparqlBuilder.Tests;

public class NamespaceTests
{
    [Fact]
    public void Can_Use_Namespace()
    {
        var pizzaNs = "https://example.com/pizza/";
        dynamic pizza = new Namespace(pizzaNs);
        
        string veganPizza = pizza.Vegan;

        veganPizza.Should().Be($"<{pizzaNs}Vegan>");
    }
}