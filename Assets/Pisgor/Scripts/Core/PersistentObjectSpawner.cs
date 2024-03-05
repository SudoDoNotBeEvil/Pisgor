using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasuShop.Core {
    public class PersistentObjectSpawner : MonoBehaviour {
        [SerializeField] GameObject _perisitentObject_pref;
        
        static bool hasSpawned = false;

        void Awake() {
            if (hasSpawned) return;

            SpawnPersistentObjects();
        }

        private void SpawnPersistentObjects() {
            hasSpawned = true;

            GameObject instance = Instantiate(_perisitentObject_pref);
            DontDestroyOnLoad(instance);
        }
    }
}