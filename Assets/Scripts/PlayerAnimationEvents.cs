using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public delegate void PlayerAnimationEventHandler();
    public PlayerAnimationEventHandler OnMovementReady;

    public void DoMovementReady()
    {
        OnMovementReady();
    }
}
