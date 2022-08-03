using Assets.ai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseEntity : BehaviorTree
{
    public bool isMoving;
    bool facingLeft;
    public float moveSpeed;
    public Vector2 moveVal;
    Animator animator;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    protected override void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(Root != null)
            Root.SetData("moveVal", moveVal);
        base.Update();
        transform.Translate(new Vector3(moveVal.x, moveVal.y, 0) * moveSpeed * Time.deltaTime);
    }

    protected virtual void PrepareForUpdate()
    {

    }

    protected virtual void HandleMovement(Vector2? movement)
    {
        moveVal = movement ?? Vector2.zero;
        isMoving = !moveVal.Equals(Vector2.zero);
        animator.SetBool("isMoving", isMoving);
        bool nowFacingLeft = moveVal.x < 0;
        if (moveVal.x != 0 && facingLeft != nowFacingLeft)
        {
            spriteRenderer.flipX = nowFacingLeft;
            facingLeft = nowFacingLeft;
        }
    }


}