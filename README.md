# sparql-builder
A library that helps you build SPARQL queries.

## Examples
```cs
var pizza = new Namespace(new Uri("https://example.com/pizza#"));

var query = new SparqlBuilder()
             .Select("*")
             .Where("?s", "a", pizza.Vegan as string)
             .Where("?s", "ex:costs", "?price")
             .OrderBy("?price")
             .Build();

Console.WriteLine(query);
/*
SELECT *
WHERE {
    ?s a <https://example.com/pizza#Vegan> ;
       ex:costs ?price .
} ORDER BY ?price
*/
```
