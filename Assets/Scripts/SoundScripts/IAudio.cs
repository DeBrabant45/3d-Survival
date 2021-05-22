using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudio 
{
    public bool IsSoundRandom { get; }
    public void PlayRandomSound();
    public void PlaySoundInOrder();
}
