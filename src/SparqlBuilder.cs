using System.Collections.Generic;

namespace SparqlBuilder;

public record SparqlBuilder
{
    private List<string> lines;
    private Operator previousOperator = Operator.NONE;

    public SparqlBuilder()
    {

    }

    public void Select(params string[] select)
    {
        if(previousOperator == Operator.NONE){
            var selects = string.Join(select, ", ");
            var line = $"SELECT {selects}";
            lines.Add(line);
        }

        previousOperator = Operator.SELECT;
    }

    public void Where(params string[] where)
    {
        switch (previousOperator)
        {
            case Operator.None: 
                throw new Exception("Cannot start with a where clause.");
            case Operator.SELECT:
            case Operator.WHERE:
                lines.Add(string.Join(where, ", "));
                break;
        }
        previousOperator = Operator.WHERE;
    }

    public void OrderBy(params string[] orderBy)
    {
        throw new NotImplementedException();
        previousOperator = Operator.ORDERBY;
    }

    public string Build()
    {
        return string.Join(lines, "\n");
    }
}