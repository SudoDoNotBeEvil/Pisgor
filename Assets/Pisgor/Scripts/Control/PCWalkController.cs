using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using Pisgor.Movement;
using System;
//using static UnityEngine.InputSystem.InputAction;

namespace Pisgor.Control {
    
    [RequireComponent(typeof(MovementController))]
    public class PCWalkController : MonoBehaviour
    {
        PCCharacterInputControl _csPlayerInput = null;
        MovementController _csMovement = null;

        void Awake() {
            _csPlayerInput = new PCCharacterInputControl();
            _csPlayerInput.PlayerMovement.Enable();

            _csMovement = GetComponent<MovementController>();

            _csPlayerInput.PlayerMovement.Jump.performed += OnInputJump;
        }

        private void FixedUpdate() {
            UpdateMovement();
        }

        private void OnInputJump(InputAction.CallbackContext callbackContext) {
            _csMovement.JumpInput();
        }

        private void UpdateMovement() {
            Vector2 inputRes = _csPlayerInput.PlayerMovement.Movement.ReadValue<Vector2>();

            _csMovement.SetVInput(inputRes);
        }

        private void OnDestroy() {
            _csPlayerInput.PlayerMovement.Jump.performed -= OnInputJump;
        }
    }

}


/*
         private void FixedUpdate() {
            var res = _csPlayerInput.PlayerMovement.Movement.ReadValue<Vector2>();
            Vector3 vTarget = transform.position + new Vector3(res.x, 0, res.y);

            _aiCharacterBT.blackboard.vtTransform = new SudoNo.Tools.VirtualTransform(vTarget, null);
        }
 */