using CasuShop.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

using Pisgor.Interactable;
using Pisgor.Movement;

namespace Pisgor.SceneManagment {
    public class Portal : Triggerable {
        [SerializeField] int _sceneToLoad = -1;
        [SerializeField] string _sceneToLoadName = "";
        [SerializeField] Transform _spawnPoint;

        [SerializeField] EDestinations _destination;
        [SerializeField] EDestinations _targetDestination;
        [SerializeField] bool faceRight = true;

        public Fader Fader {
            get => default;
            set {
            }
        }

        public enum EDestinations { NONE, RED = 42001, BLUE = 42002, GREEN = 42003, YELLOW = 42004,
        LEFT = 42010, RIGHT = 42011, UP = 42012, DOWN = 42013,
        UPLEFT = 42014, UPRIGHT = 42015, DOWNLEFT = 42016, DOWNRIGHT = 42017,
        CENTER = 42018
        };

        public EDestinations GetDestination() => _destination;

        private void Awake() {
            if (_spawnPoint == null)
                Debug.LogError("Portal spawnPoint was not assigned");
        }

        public void StartTransition(GameObject go) {
            StartTransition();
        }

        [ContextMenu("TestTransition")]
        public void StartTransition() {
            StopAllCoroutines();
            StartCoroutine(Transition());
        }

        private IEnumerator Transition() {
            if (_sceneToLoad < 0 && String.IsNullOrEmpty(_sceneToLoadName)) {
                Debug.LogError($"Portal: scene to load is {_sceneToLoad}");
                yield break;
            }

            Fader fader = FindObjectOfType<Fader>();

            //DontDestroyOnLoad(gameObject);

            yield return fader.CFadeOut();

            if(String.IsNullOrEmpty(_sceneToLoadName))
                yield return SceneManager.LoadSceneAsync(_sceneToLoad);
            else
                yield return SceneManager.LoadSceneAsync(_sceneToLoadName);
            
            Debug.Log("Scene loaded");
            Portal otherPortal = GetOtherPortal();
            yield return new WaitForFixedUpdate();
            UpdatePlayer(otherPortal);

            fader.StartFade(false);
            Destroy(this.gameObject);
        }

        private void UpdatePlayer(Portal otherPortal) {
            if (otherPortal == null)
                return;

            GameObject player = GameObject.FindWithTag("Player");
            //player.GetComponent<NavMeshAgent>().Warp(otherPortal.GetSpawn().transform.position);
            player.transform.position = otherPortal.GetSpawn().transform.position;
            player.transform.rotation = otherPortal.GetSpawn().transform.rotation;
            player.GetComponent<MovementController>().SetFaceRight(otherPortal.faceRight);
        }

        private Transform GetSpawn() {
            return _spawnPoint;
        }

        private Portal GetOtherPortal() {
            foreach (Portal portal in FindObjectsOfType<Portal>()) {
                if (portal == this) continue;

                if (portal.GetDestination() == _targetDestination)
                    return portal;
            }

            //!!!!!!!!!!!
            //!!!TODO!!!!
            //!!!!!!!!!!!
            //Send to hub
            //!!!!!!!!!!!
            //!!PELIGRO!!

            Debug.LogError($"There is no corresponding portal: ");
            Debug.LogError($"scene: {_sceneToLoad}, destination: {_targetDestination}");
            return null;
        }

        public override void OnTrig(GameObject go) {
            StartTransition(go);
        }
    }
}