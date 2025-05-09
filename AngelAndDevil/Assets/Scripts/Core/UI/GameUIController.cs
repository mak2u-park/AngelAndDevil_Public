using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIController : BaseUIController
{
    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}
