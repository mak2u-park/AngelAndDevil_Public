using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseUIController : MonoBehaviour
{
    protected abstract UIState GetUIState();

    protected virtual void OnEsc()
    {

    }
}
