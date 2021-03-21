using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAmmo
{
    public int CurrentAmmoCount { get; }
    public void ReloadAmmoCount();
    public void RemoveFromCurrentAmmoCount();
    public bool IsAmmoEmpty();
    public bool IsAmmoFull();
}
