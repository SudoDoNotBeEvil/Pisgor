using Pisgor.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

using Pisgor.Control;

namespace Pisgor.Interaction {
    //UnityEvent subclass with a GameObject parameter
    [System.Serializable]
    public class GameObjectEvent : UnityEvent<GameObject> { }

    public class Trigger : MonoBehaviour {
        [SerializeField] Triggerable _target = null;

        [SerializeField] GameObjectEvent _onPlayerEnter;
        [SerializeField] GameObjectEvent _onTrigger;
        [SerializeField] GameObjectEvent _onPlayerExit;

        [SerializeField] bool _triggerOnEnter = true;
        [SerializeField] bool _triggerOnUse = true;
        //GameObjectEvent _onTrigger;

        private void OnTriggerEnter2D(Collider2D other) {
            if (!other.CompareTag("Player"))
                return;

            _onPlayerEnter.Invoke(other.gameObject);
            _target?.OnPlayerEnter(other.gameObject);

            if (_triggerOnEnter)
                _target?.OnTrig(other.gameObject);
            else if (_triggerOnUse) { 
                other.GetComponent<PCUseController>()?.SetTrigger(this);
            }
        }

        public void Use(GameObject go) {
            if (_triggerOnUse) {
                _target?.OnTrig(go);
                _onTrigger.Invoke(go);
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (!other.CompareTag("Player"))
                return;

            _onPlayerExit.Invoke(other.gameObject);
            _target?.OnPlayerExit(other.gameObject);


            if (_triggerOnUse)
                other.GetComponent<PCUseController>()?.ResetTrigger(this);
        }
    }
}
