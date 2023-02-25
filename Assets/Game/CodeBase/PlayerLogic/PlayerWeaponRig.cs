using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Game.CodeBase.PlayerLogic
{
    public class PlayerWeaponRig : MonoBehaviour
    {
        [SerializeField] private MultiParentConstraint _multiAnimConstraint;

        private float _handWeight;
        private float _backWeight;

        private void Start() => SetActiveBack();

        private void ChangeWeight(int index, float value)
        {
            var sources = _multiAnimConstraint.data.sourceObjects;
            sources.SetWeight(index, value);
            _multiAnimConstraint.data.sourceObjects = sources;
        }

        public void SetActiveHand()
        {
            _backWeight = 0;
            _handWeight = 1;
        }

        public void SetActiveBack()
        {
            _backWeight = 1;
            _handWeight = 0;
        }

        public void OnUpdate()
        {
            ChangeWeight(0, _backWeight);
            ChangeWeight(1, _handWeight);
        }
    }
}
