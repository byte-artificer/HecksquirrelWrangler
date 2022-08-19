using Assets.ai;
using Assets.extensions;
using Assets.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseEntity<T> : BehaviorTree<T> where T:BaseEntityState
{
    public T State;
    protected Animator _animator;
    SpriteRenderer spriteRenderer;
    const string IgnoreCollisionTag = "_Ignore";

    public BoolValue GamePaused;

    // Start is called before the first frame update
    protected override void Start()
    {
        State.Reset();
        _animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        try
        {
            var blockersToIgnore = GameObject.FindGameObjectsWithTag($"{gameObject.tag}{IgnoreCollisionTag}");
            foreach (var obj in blockersToIgnore)
                Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        catch { /* eat the exception, because Unity doesn't give me a real way to check if a tag exists as far as google can tell me... */ }

        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        transform.Translate(State.MoveVal.ToVector3() * State.CurrentSpeed * Time.deltaTime);
    }

    protected virtual void PrepareForUpdate()
    {

    }

    protected sealed override BehaviorTreeNode<T> SetupTree()
    {
        return new SelectorNode<T>(State, new PausedNode<T>(transform, State, GamePaused), SetupTreeCore());
    }

    protected abstract BehaviorTreeNode<T> SetupTreeCore();

    protected virtual void HandleMovement(Vector2? movement)
    {
        State.MoveVal = movement ?? Vector2.zero;
        State.IsMoving = !State.MoveVal.Equals(Vector2.zero);
        _animator.SetBool("isMoving", State.IsMoving);
        bool nowFacingLeft = State.MoveVal.x < 0;
        if (State.MoveVal.x != 0 && State.FacingLeft != nowFacingLeft)
        {
            spriteRenderer.flipX = nowFacingLeft;
            State.FacingLeft = nowFacingLeft;
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        State.ActiveCollision = col;
    }

}