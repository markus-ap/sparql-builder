# sparql-builder
A library that helps you build SPARQL queries.

## Examples
```cs
var veganPizza = "ex:VeganPizza";

var query = new SparqlBuilder()
             .Select("*")
             .Where("?s", "a", veganPizza)
             .Where("?s", "ex:costs", "?price")
             .OrderBy("?price")
             .Build();

Console.WriteLine(query);
/*
SELECT *
WHERE {
    ?s a ex:VeganPizza ;
       ex:costs ?price .
} ORDER BY ?price
*/
```

```cs
var pizza = new Namespace("pizza", new Uri("https://example.com/pizza#"));

Console.WriteLine(pizza.Vegan); 
/*
"https://example.com/pizza#Vegan"
*/
```
