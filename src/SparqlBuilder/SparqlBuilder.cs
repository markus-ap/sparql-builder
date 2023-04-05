using System;
using System.Linq;
using System.Collections.Generic;

namespace SparqlBuilder;

internal record States
{
    internal List<string> lines = new();
    internal List<Operator> operators = new();
    internal Operator previousOperator = Operator.NONE;

    internal void SetOperator(Operator op)
    {
        previousOperator = op;
        operators.Add(op);
    }
}

public record SparqlBuilder
{
    private States states = new();


    public SparqlBuilder() { }

    public SparqlBuilder Select(params string[] select)
    {
        switch (states.previousOperator)
        {
            case Operator.WHERE:
            case Operator.NONE:
                var selects = string.Join(", ", select);
                var line = $"SELECT {selects}";
                var newLines = states.lines;
                newLines.Add(line);
                states.SetOperator(Operator.SELECT);
                return this with 
                {
                    states = states with
                    {
                        lines = newLines
                    }
                };
            default:
                throw new Exception("SELECT can only occur after WHERE or at start.");
        }

    }


    public SparqlBuilder Where(string s, string p, string o)
    {
        switch (states.previousOperator)
        {
            case Operator.NONE: 
                throw new Exception("Cannot start with a where clause.");
            case Operator.SELECT:
            case Operator.WHERE:
                var newLines = states.lines;
                newLines.Add($"{s} {p} {o} .");
                states.SetOperator(Operator.WHERE);
                return this with
                {
                    states = states with
                    {
                        lines = newLines
                    }
                };
            default:
                throw new Exception("Unsupported operator prior to WHERE.");
        }
    }

    public SparqlBuilder OrderBy(params string[] orderBy)
    {
        switch (states.previousOperator)
        {
            case Operator.NONE:
            case Operator.SELECT:
                 throw new Exception("You can only group after a where clause.");
            case Operator.WHERE:
                var orders = string.Join(", ", orderBy);
                var newLines = states.lines;
                newLines.Add($"ORDER BY {orders}");
                states.SetOperator(Operator.ORDERBY);
                return this with
                {
                    states = states with
                    {
                        lines = newLines
                    }
                };
            default:
                throw new Exception("Unsupported clause prior to ORDER BY");
        }
    }

    public string Build()
    {
        var res = string.Empty;
        Operator? previousOperator = null;

        var openBraces = 0;

        for(var i = 0; i < states.operators.Count(); i++)
        {
            var line = states.lines[i];
            var op = states.operators[i];

            switch (op){
                case Operator.SELECT:
                    res += line + " ";
                    break;
                case Operator.WHERE:
                    if(previousOperator == Operator.WHERE)
                    {
                        res += line + " ";
                    }
                    else
                    {
                        res += $"WHERE {{ {line} ";
                        openBraces += 1;
                    }
                    break;
                case Operator.GRAPH:
                    break;
                case Operator.ORDERBY:
                    break;
                default:
                    throw new Exception("Something weird happened");
            }

            previousOperator = op;
        }

        for(var i = 0; i < openBraces; i++)
        {
            res += "}";
            if(i != openBraces-1) res += "\n";
        }

        return res;
    }
}