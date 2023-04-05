# SparqlBuilder
This is a C# library for building SPARQL queries as [described by W3C](https://www.w3.org/TR/sparql11-query/). 

## SparqlBuilder
This class helps you build SPARQL queries.

### Code
```cs
var sparql = new SparqlBuilder()
                .Select("*")
                .Where("?s", "?p", "?o")
                .Build();
```
### SPARQL
```sparql
SELECT * WHERE { ?s ?p ?o . }
```

## Namespace
This class is meant to help with maintaining longer IRI references for your SPARQL queries.

### Code
```cs 
dynamic ex = new Namespace("https://example.com/");
Console.WriteLine(ex.Object)
```

### Console Output
```
<https://example.com/Object>
```