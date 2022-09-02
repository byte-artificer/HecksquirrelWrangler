using Assets.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.ai.hecksquirrel
{
    public class FeelThreatened : BehaviorTreeNode<HeckSquirrelState>
    {
        readonly float _threatDistance;
        bool _playedSound;
        int _soundIndex;
        public FeelThreatened(Transform transform, HeckSquirrelState state, float threatDistance, int soundIndex) : base(transform, state) 
        { 
            _threatDistance = threatDistance;
            _soundIndex = soundIndex;
        }

        public override eNodeState Evaluate()
        {
            var distance = Vector3.Distance(_state.Player.transform.position, _transform.position);

            if (distance <= _threatDistance)
            {
                if(!_playedSound)
                {
                    var sound = _state.ScaredSounds[_soundIndex];
                    _state.AudioRequester.RequestedAudioClips.Enqueue(sound);
                    _playedSound = true;
                }

                _state.IsFleeing = true;

                Debug.Log($"Feels threatened {_threatDistance}");

                RunState = eNodeState.SUCCESS;
                return RunState;
            }

            _playedSound = false;
            RunState = eNodeState.FAILURE;
            return RunState;
        }
    }
}
