using Assets.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy State Collection")]
public class EnemyStateCollection : StateCollection<HeckSquirrelState>
{
    private void OnEnable()
    {
        this.Clear();
    }
}
