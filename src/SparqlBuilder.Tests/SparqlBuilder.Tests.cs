using FluentAssertions;
using MAP.SPARQL.Builder;

namespace MAP.SPARQL.Builder.Tests;

public class SparqlBuilderTests
{
    [Fact]
    public void Can_Build_Sparql()
    {
        var sparql = new SparqlBuilder()
                        .Select("*")
                        .Where("?s", "?p", "?o")
                        .Build();

        var answer = "SELECT * WHERE { ?s ?p ?o . }";
        sparql.Should().Be(answer);
    }

    [Fact]
    public void Can_Build_Sparql_With_Namespace()
    {
        dynamic ex = new Namespace("https://example.com/"); 

        var sparql = new SparqlBuilder()
                        .Select("*")
                        .Where("?s", "?p", ex.Thing as string)
                        .Build();

        var answer = "SELECT * WHERE { ?s ?p <https://example.com/Thing> . }";
        sparql.Should().Be(answer);       
    }

    [Fact]
    public void Can_Build_Sparql_With_Multiple_Wheres()
    {
        dynamic ex = new Namespace("https://example.com/");

        var sparql = new SparqlBuilder()
                    .Select("*")
                    .Where("?s", "?p", ex.Thing as string)
                    .Where("?s", "?p2", "?o2")
                    .Where("?s", "?p2", ex.Weird as string)
                    .Build();
        
        sparql.Should().Be("SELECT * WHERE { ?s ?p <https://example.com/Thing> . ?s ?p2 ?o2 . ?s ?p2 <https://example.com/Weird> . }");

    }
}

