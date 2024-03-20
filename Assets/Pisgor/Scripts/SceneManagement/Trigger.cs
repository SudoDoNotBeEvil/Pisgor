using Pisgor.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

using Pisgor.Control;
using Unity.XR.Oculus.Input;
using Pisgor.Inventories;

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

        [SerializeField] bool _requiresItem = false;
        [SerializeField] bool _destroyRequiredItem = true;
        [SerializeField] ItemSO _requiredItem = null;

        private void OnTriggerEnter2D(Collider2D other) {
            if (!other.CompareTag("Player"))
                return;

            Debug.Log("OnTriggerEnter2D");

            _onPlayerEnter.Invoke(other.gameObject);
            _target?.OnPlayerEnter(other.gameObject);

            if (!CheckItem()) {
                return;
            }

            if (_triggerOnEnter)
                CallTrigger(other.gameObject);
            else if (_triggerOnUse) {
                other.GetComponent<PCUseController>()?.SetTrigger(this);
            }
        }

        public void Use(GameObject go) {
            if (!CheckItem())
                return;

            if (_triggerOnUse)
                CallTrigger(go);
            else
                Debug.LogWarning("Trigger not set to use");
        }

        private void CallTrigger(GameObject go) {
            _target?.OnTrig(go);
            _onTrigger.Invoke(go);
        }

        public bool CheckItem() {
            if (_requiresItem && _requiredItem != null)
                return PersistentInventory.Instance.HoldsItem(_requiredItem);

            return true;
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
