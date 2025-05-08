using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IEnable.cs
public interface IEnable
{
    bool IsEnable { get; }
    void Enable();
    void Disable();
}