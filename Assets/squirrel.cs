//using Assets.ai;
//using Assets.ai.hecksquirrel;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEngine;

//class squirrel
//{
//    GameObject player;
//    Collision2D collision;
//    public int stress;
//    public int confidence;
//    public int tired;
//    public int bored;
//    public int fleeThreshold;
//    public int huntThreshold;
//    bool fleeing;
//    bool hunting;
//    bool rest;
//    bool fleeingCollision;

//    BehaviorTree _tree;
//    protected void Start()
//    {
        
//        player = GameObject.FindGameObjectsWithTag("Player").First();
//    }

//    protected void Update()
//    {
//        TickAI();
        
//    }

//    void TickAI()
//    {
//        _tree.Update();
//        HandleMovement(moveVal);
//        //var movement = Vector2.zero;
//        //if (fleeingCollision)
//        //{
//        //    var distanceToCollision = Vector3.Distance(transform.position, collision.contacts.First().point);
//        //    if(distanceToCollision > 2)
//        //    {
//        //        fleeingCollision = false;
//        //    }
//        //}
//        //else
//        //{
//        //    var distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
//        //    if (distanceToPlayer < 4)
//        //    {
//        //        stress++;
//        //    }
//        //    if (distanceToPlayer < 2.5)
//        //    {
//        //        stress++;
//        //        if(!hunting)
//        //            confidence--;
//        //    }
//        //    if (distanceToPlayer > 4)
//        //        stress--;
//        //    if (distanceToPlayer > 5.5)
//        //    {
//        //        stress--;
//        //        confidence++;
//        //    }

//        //    int attitude = confidence - stress + bored;
            
//        //    if (attitude > huntThreshold)
//        //    {
//        //        hunting = true;
//        //        moveSpeed = 2.2f;
//        //    }

//        //    if (attitude > fleeThreshold/4)
//        //    {
//        //        fleeing = false;
//        //        moveSpeed = 1.5f;
//        //    }
//        //    else if (hunting && attitude < -fleeThreshold*1.5)
//        //    {
//        //        bored = 0;
//        //        hunting = false;
//        //        fleeing = true;
//        //        moveSpeed = 1.5f;
//        //    }
//        //    else if (!hunting && attitude < -fleeThreshold)
//        //    {
//        //        hunting = false;
//        //        fleeing = true;
//        //        moveSpeed = 1.5f;
//        //    }
//        //    else if(!hunting && attitude < -fleeThreshold * 1.5)
//        //    {
//        //        fleeing = true;
//        //        moveSpeed = 2.5f;
//        //    }

//        //    if(!fleeing && !hunting)
//        //    {
//        //        stress--;
//        //    }

//        //    stress = Math.Min(stress, fleeThreshold * 5);
//        //    stress = Math.Max(stress, 0);
//        //    confidence = Math.Min(confidence, huntThreshold * 2);
//        //    confidence = Math.Max(confidence, 0);

//        //    if (tired > 1000)
//        //    {
//        //        rest = true;
//        //    }
//        //    if(rest && tired <= 700)
//        //    {
//        //        rest = false;
//        //        tired = 0;
//        //    }    

//        //    if(rest)
//        //    { 
//        //        tired--;
//        //        movement = Vector2.zero;
//        //    }
//        //    else if (fleeing)
//        //    {
//        //        tired++;
//        //        var vect = (transform.position - player.transform.position).normalized;
//        //        movement = new Vector2(vect.x, vect.y);
//        //    }
//        //    else if (hunting)
//        //    {
//        //        var vect = (player.transform.position - transform.position).normalized;
//        //        movement = new Vector2(vect.x, vect.y);
//        //    }
//        //    else
//        //        bored++;

//        //    HandleMovement(movement);
//        //}
//    }

//    private void OnCollisionEnter2D(Collision2D col)
//    {
//        //collision = col;
//        //var vect = Vector2.zero;
//        //foreach (var c in col.contacts)
//        //    vect += c.normal;

//        //var movement = moveVal + vect;
//        //movement = movement.normalized;
//        //if (movement.Equals(Vector2.zero))
//        //    movement = Quaternion.Euler(0, 0, 45) * vect;

//        //fleeingCollision = true;
//        //HandleMovement(movement);
//    }
//}