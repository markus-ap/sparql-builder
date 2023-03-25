using System.Collections.Generic;
using System.Linq;

namespace SparqlBuilder;

public record SparqlBuilder
{

    private List<string> lines = new();
    private List<Operator> operators = new();
    private Oprator previousOperator = Operator.NONE;

    public SparqlBuilder() { }

    private void SetOperator(Operator operator)
    {
        previousOperator = operator;
        operators.Add(operator);
    }

    public void Select(params string[] select)
    {
        switch (previousOperator)
        {
            case Operator.WHERE:
            case Operator.NONE:
                var selects = string.Join(select, ", ");
                var line = $"SELECT {selects}";
                lines.Add(line);
                SetOperator(Operator.Select);
                break;
        }
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
                SetOperator(Operator.WHERE);
                break;
        }
    }

    public void OrderBy(params string[] orderBy)
    {
        throw new NotImplementedException();
        SetOperator(Operator.ORDERBY);
    }

    public string Build()
    {
        return string.Join(lines, "\n");
    }
}