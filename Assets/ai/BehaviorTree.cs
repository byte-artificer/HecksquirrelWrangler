using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.ai
{
    public abstract class BehaviorTree : MonoBehaviour
    {
        public BehaviorTreeNode Root { get; private set; }

        protected virtual void Start()
        {
            Root = SetupTree();
        }

        protected virtual void Update()
        {
            if (Root != null)
                Root.Evaluate();
        }

        protected abstract BehaviorTreeNode SetupTree();
    }
}
