using Assets.ai;
using Assets.state;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class player : BaseEntity<BaseEntityState>
{
    bool win;
    public EnemyStateCollection HeckSquirrelStates;
    public FloatValue PlayerStamina;

    float _maxStamina = 5f;
    float _stamina = 5f;
    bool _sprinting = false;
    bool _sprintHeld = false;
    protected override BehaviorTreeNode<BaseEntityState> SetupTree()
    {
        return null;
    }

    protected override void Update()
    {
        base.Update();

        if (_sprinting)
            _stamina -= Time.deltaTime;
        else if(!_sprintHeld)
            _stamina += 0.85f * Time.deltaTime;

        _stamina = Mathf.Clamp(_stamina, 0, _maxStamina);

        if (_stamina == 0)
            ToggleSprint(false);

        PlayerStamina.Value = _stamina / _maxStamina;

        if (HeckSquirrelStates.All(x => x.SafeInPen))
        {
            _animator.SetBool("win", true);
            win = true;
            foreach (var s in HeckSquirrelStates)
                s.SquirrelsLose = true;

            PlayerStamina.Value = 1;

            base.HandleMovement(Vector2.zero);
        }
    }

    void OnMove(InputValue value)
    {
        if (win)
            return;

        base.HandleMovement(value?.Get<Vector2>());
    }

    void OnSprint(InputValue value)
    {
        if (win)
            return;

        _sprintHeld = value.isPressed;

        var sprint = value.isPressed && PlayerStamina.Value > 0.15f;

        ToggleSprint(sprint);
    }

    void ToggleSprint(bool sprint)
    {
        if (sprint)
            State.CurrentSpeed = State.BaseSpeed * 1.95f;
        else
            State.CurrentSpeed = State.BaseSpeed;

        _sprinting = sprint;
    }
}
