using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Objective : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] private string _iD;
    [SerializeField] private string _description;
    protected bool isCompleted = false;
    public Action<Objective> OnComplete;
    public bool IsCompleted { get => isCompleted; }
    public string ID { get => _iD; }
    public string Description { get => _description; }
    public abstract void Initialize();
    public abstract void Evaluate();
    public abstract void Complete();
    public abstract void Terminate();

    public virtual void OnBeforeSerialize()
    {
        if (string.IsNullOrEmpty(this._iD))
        {
            this._iD = Guid.NewGuid().ToString("N");
        }
        if (string.IsNullOrEmpty(_description))
        {
            _description = this.name;
        }
    }

    public virtual void OnAfterDeserialize()
    {
        isCompleted = false;
    }
}
