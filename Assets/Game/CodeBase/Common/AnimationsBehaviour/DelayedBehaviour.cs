using UnityEngine;

namespace Game.CodeBase.Common.AnimationsBehaviour
{
    public class DelayedBehaviour : StateMachineBehaviour
    {
        [SerializeField] private float _animationDelay;
        [SerializeField] private string _nextAnimationName;
        private float _delay;
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _delay = _animationDelay;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_delay < 0) 
                animator.Play(_nextAnimationName);

            _delay -= Time.deltaTime;
        }
    }
}
