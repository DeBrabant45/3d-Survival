using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour, IAttackable
{
    public Action OnAttackSuccessful { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void DetectColliderInFront()
    {
        throw new NotImplementedException();
    }

    public void PreformAttack()
    {
        throw new NotImplementedException();
    }
}
