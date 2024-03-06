using UnityEngine.InputSystem;
using System;
using UnityEngine;
using Pisgor.Interaction;
using Pisgor.Inventories;
//using static UnityEngine.InputSystem.InputAction;

namespace Pisgor.Control {

    public class PCUseController : MonoBehaviour {
        PCCharacterInputControl _csPlayerInput = null;

        private Trigger _currentTrigger = null;

        [SerializeField] GameObject _useIcon = null;
        Inventory _inventory = null;

        public void SetTrigger(Trigger trigger) {
            _currentTrigger = trigger;

            if (trigger != null) {
                _useIcon.SetActive(true);
            }
            else
                _useIcon.SetActive(false);
        }
        public void ResetTrigger(Trigger trigger) {
            if (_currentTrigger == trigger) {
                _currentTrigger = null;
                _useIcon.SetActive(false);
            }
        }

        private void Awake() {
            _csPlayerInput = new PCCharacterInputControl();
            _csPlayerInput.PlayerUse.Enable();

            _csPlayerInput.PlayerUse.Use.performed += OnInputUse;
            _csPlayerInput.PlayerUse.Drop.performed += OnInputDrop;

            _inventory = GetComponent<Inventory>();
        }

        private void OnInputDrop(InputAction.CallbackContext context) {
            _inventory?.Drop();
        }

        private void OnInputUse(InputAction.CallbackContext context) {
            _currentTrigger?.Use(gameObject);
        }
    }

}