
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlockable
{
    int BlockLevel { get; }
    bool IsBlocking { get; set; }
    Action OnBlockSuccessful { get; set; }
    GameObject Attacker { get; set; }
    bool IsBlockHitSuccessful();
}
