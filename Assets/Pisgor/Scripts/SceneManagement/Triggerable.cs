using UnityEngine;

namespace Pisgor.Interactable {
	public abstract class Triggerable : MonoBehaviour {
        bool playerInside = false;
        public float timePlayerEnter = 0;

		public virtual void OnTrig(GameObject go) {
            ;// Debug.LogError("trig not implemented");
		}

        public virtual void OnPlayerEnter(GameObject player, float timeEnter = -1f) {
            ;// Debug.Log("Player Enter");

            timePlayerEnter = timeEnter;
        }

        public virtual void OnPlayerExit(GameObject player, float deltaTime = -1f) {
            ;// Debug.Log("Player Exit");
        }
    }
}