using System;
using UnityEngine;

namespace AD.Quests
{
    [Serializable]
    public class Reward
    {
        [Min(1)]
        public int Number;
        public ItemSO Item;
    }
}
