using Assets.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.ai
{
    public enum eNodeState { RUNNING, SUCCESS, FAILURE }
    public class BehaviorTreeNode<T> where T : BaseEntityState
    {
        protected eNodeState RunState;
        public BehaviorTreeNode<T> Parent;
        protected List<BehaviorTreeNode<T>> _children = new List<BehaviorTreeNode<T>>();

        protected readonly T _state;
        protected readonly Transform _transform;

        public BehaviorTreeNode(Transform transform, T state) 
        { 
            Parent = null;
            _transform = transform;
            _state = state;
        }

        public BehaviorTreeNode(Transform transform, T state, params BehaviorTreeNode<T>[] children) : this(transform, state)
        { 
            foreach (var c in children) 
                Add(c); 
        }

        public virtual eNodeState Evaluate() => eNodeState.FAILURE;

        public void Add(BehaviorTreeNode<T> child)
        {
            child.Parent = this;
            _children.Add(child);
        }
    }
}
