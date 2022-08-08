using Assets.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.ai
{
    public abstract class BehaviorTree<T> : MonoBehaviour where T : BaseEntityState
    {
        public BehaviorTreeNode<T> Root { get; private set; }

        protected virtual void Start()
        {
            Root = SetupTree();
        }

        protected virtual void Update()
        {
            if (Root != null)
                Root.Evaluate();
        }

        protected abstract BehaviorTreeNode<T> SetupTree();
    }
}
