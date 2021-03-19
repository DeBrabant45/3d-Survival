
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlockable
{
    int BlockLevel { get; }
    Action OnBlockSuccessful { get; set; }
    bool IsBlockHitSuccessful();
}
