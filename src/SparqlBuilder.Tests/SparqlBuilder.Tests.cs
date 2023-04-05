using FluentAssertions;
using SparqlBuilder;

namespace SparqlBuilder.Tests;

public class SparqlBuilderTests
{
    [Fact]
    public void Can_Build_Sparql()
    {
        var sparql = new SparqlBuilder()
                        .Select("*")
                        .Where("?s", "?p", "?o")
                        .Build();
        sparql = NormalizeMultilineString(sparql);

        var answer = "SELECT * WHERE { ?s ?p ?o . }";
        sparql.Should().Be(answer);
    }

    [Fact]
    public void Can_Build_Sparql_With_Namespace()
    {
        dynamic ex = new Namespace("https://example.com/"); 
        string thing = ex.Thing;

        var sparql = new SparqlBuilder()
                        .Select("*")
                        .Where("?s", "?p", thing)
                        .Build();

        sparql = NormalizeMultilineString(sparql);


        var answer = "SELECT * WHERE { ?s ?p <https://example.com/Thing> . }";
        sparql.Should().Be(answer);       
    }

    private string NormalizeMultilineString(string multilineString)
    {
        var lines = multilineString
                        .Replace("\n", " ")
                        .Replace("\t", "")
                        .Replace("\r", "");

        return string.Join(Environment.NewLine, lines);
    }
}

