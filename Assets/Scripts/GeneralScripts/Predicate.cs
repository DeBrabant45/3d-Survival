using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD.General
{
    public enum Predicate
    {
        HasActiveQuest,
        DoesNotHaveQuest,
        HasInventoryItem,
        HasCompletedQuest,
        HasCompletedObjective,
        None,
        HasEverAcceptedQuest,
    }
}
