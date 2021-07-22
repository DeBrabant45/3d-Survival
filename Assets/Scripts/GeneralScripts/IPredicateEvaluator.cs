using System;
using System.Collections.Generic;

namespace AD.General
{
    public interface IPredicateEvaluator
    {
        bool? Evaluate(Predicate predicate, string[] parameters);
    }
}
