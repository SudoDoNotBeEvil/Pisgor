using Pisgor.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace Pisgor.Interaction {
    //UnityEvent subclass with a GameObject parameter
    [System.Serializable]
    public class GameObjectEvent : UnityEvent<GameObject> { }

    public class Trigger : MonoBehaviour {
        [SerializeField] Triggerable _target = null;

        [SerializeField] GameObjectEvent _onPlayerEnter;
        [SerializeField] GameObjectEvent _onPlayerExit;
        //GameObjectEvent _onTrigger;

        private void OnTriggerEnter2D(Collider2D other) {
            if (!other.CompareTag("Player"))
                return;

            _onPlayerEnter.Invoke(other.gameObject);
            _target?.OnPlayerEnter(other.gameObject);


            _target.OnTrig(other.gameObject);
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (!other.CompareTag("Player"))
                return;

            _onPlayerExit.Invoke(other.gameObject);
            _target?.OnPlayerExit(other.gameObject);
        }
    }
}
