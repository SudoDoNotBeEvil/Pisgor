using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Tilemaps;
using UnityEngine;

namespace Pisgor.Movement { //!!!!PELIGRO!!!!! rename namespace

    [RequireComponent(typeof(Rigidbody2D))]
    public class MovementController : MonoBehaviour {
        [SerializeField] MovementSO _movementSO = null;

        Rigidbody2D _rb = null;

        private bool _isFacingRight = true;
        private bool _isGrounded = false;

        [SerializeField] private LayerMask groundMask;
        [SerializeField] Transform _groundCheckCenter;

        private Vector3 currentVelocity = Vector3.zero;

        [SerializeField] Animator _animator;

        private float _speed = 0.0f;
        private float _kControl = 0.0f;

        void Awake() {
            _rb = GetComponent<Rigidbody2D>();

            if(_animator == null)
                Debug.LogError("Animator is not assigned in " + gameObject.name);

            if (_groundCheckCenter == null)
                Debug.LogError("GroundCheckCenter is not assigned in " + gameObject.name);
        }

        private void FixedUpdate() {
            CheckGrounded();

            _animator.SetFloat("xVelocity", Mathf.Abs(_rb.velocity.x));
            _animator.SetFloat("yVelocity", _rb.velocity.y);
            _animator.SetBool("isGrounded", _isGrounded);
        }

        private void CheckGrounded() {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheckCenter.position, _movementSO.groundCheckRadius, groundMask);

            foreach (var coll in colliders)
                if (coll.gameObject != gameObject) {
                    _isGrounded = true;
                    return;
                };
            _isGrounded = false;
        }

        /*
       // Move the character by finding the target velocity
       Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
       // And then smoothing it out and applying it to the character
       m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
*/
        public void SetVInput(Vector2 inputRes) {


            if (_isGrounded) {
                _speed = _movementSO.runSpeed;
                _kControl = _movementSO.groundControl;
            }
            else {
                _speed = _movementSO.airSpeed;
                _kControl = _movementSO.airControl;
            }




            float velHorizontal = inputRes.x * _speed;
            Vector3 vTarget = new Vector2(velHorizontal, _rb.velocity.y);

            _rb.velocity = Vector3.SmoothDamp(_rb.velocity, vTarget, ref currentVelocity, _kControl);

            UpdateFaceDirection(velHorizontal);

            //_rb.velocity = new Vector2(inputRes.x * _movementSO.runSpeed, _rb.velocity.y);
        }

        private void UpdateFaceDirection(float velHorizontal) {
            if (velHorizontal > 0 && !_isFacingRight)
                Flip();

            if (velHorizontal < 0 && _isFacingRight)
                Flip();
        }
        private void Flip() {
            _isFacingRight = !_isFacingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        void Start() { }
        void Update() { }

        internal void JumpInput() {
            Debug.Log($"JumpInput, grounded:{_isGrounded}");

            if (_isGrounded)
                _rb.AddForce(Vector2.up * _movementSO.jumpForce, ForceMode2D.Impulse);

            _animator.GetComponent<Animator>().SetTrigger("Jump");
        }

        internal void SetFaceRight(bool faceRight) {
            _isFacingRight = faceRight;
            Vector3 scale = transform.localScale;
            scale.x = faceRight ? 1 : -1;
            transform.localScale = scale;
        }
    }
}