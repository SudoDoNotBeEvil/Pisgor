using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pisgor.Movement {

    [CreateAssetMenu(fileName = "MovementSO", menuName = "Pisgor/MovementSO", order = 0)]
    public class MovementSO : ScriptableObject {
        public float runSpeed = 6.0f;
        public float walkSpeed = 3.0f;
        public float crouchSpeed = 1.0f;
        public float jumpForce = 5.0f;
        public float groundCheckRadius = 0.2f;

        public float airSpeed = 3.0f;

        public float groundControl = 0.1f;
        public float airControl = 0.5f;
    }

}