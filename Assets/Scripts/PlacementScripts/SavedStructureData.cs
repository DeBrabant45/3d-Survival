using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public struct SavedStructureData
{
    //Position Vector3
    public float PositionX;
    public float PositionY;
    public float PositionZ;
    //Rotation Euler angles
    public float RotationX;
    public float RotationY;
    public float RotationZ;
    //ID
    public string ID;
}