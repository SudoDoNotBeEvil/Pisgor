using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pisgor.Interactive {
    public class TwoStateObject : MonoBehaviour {
        private bool _bState = false;
        public bool State { get => _bState; private set => _bState = value;}

        [SerializeField] GameObject _objectTrue = null;
        [SerializeField] GameObject _objectFalse = null;

        [SerializeField] bool _isSingleUseOnly = true;

        private void Awake() {
            SetState(State);
        }

        public void SetState(bool state) {
            _objectTrue.SetActive(state);
            _objectFalse.SetActive(!state);
            State = state;
        }

        public void Use() {
            if (_isSingleUseOnly) {
                if (State == false)
                    SetState(true);

                return;
            }

            // if multiple use
            SetState(!State);
        }
    }
}