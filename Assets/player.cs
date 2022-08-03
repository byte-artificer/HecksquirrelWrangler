using Assets.ai;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class player : BaseEntity
{
    protected override BehaviorTreeNode SetupTree()
    {
        return null;
    }

    void OnMove(InputValue value)
    {
        base.HandleMovement(value?.Get<Vector2>());
    }
}
