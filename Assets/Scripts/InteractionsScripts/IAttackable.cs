using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    Action OnAttackSuccessful { get; set; }
    void DetectColliderInFront();
    void PreformAttack();
}
