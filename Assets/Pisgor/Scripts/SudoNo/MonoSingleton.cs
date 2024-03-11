using UnityEngine;

namespace SudoNo.Singletons {
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour {
        private static T _instance = null;
        public static T Instance {
            get {
                if (_instance == null) {
                    // This will only happen the first time this reference is used.
                    _instance = FindObjectOfType<T>();

                    if (_instance == null) {
                        Debug.LogError($"Singleton {typeof(T).Name} is missing");

                        GameObject newObj = new GameObject(typeof(T).Name);
                        _instance = newObj.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }

        protected virtual void Awake() {
            if (_instance != null && _instance != this) {
                Debug.LogError($"Singleton: Another instance of {typeof(T).Name} already exists. Destroying this one.");
                Destroy(this.gameObject);
            }
            else {
                _instance = this as T;
            }
        }
    }
}