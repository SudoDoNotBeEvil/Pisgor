using PixelCrushers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pisgor.Inventories {
    public class ItemSpawner : MonoBehaviour {
        [SerializeField] ItemSO _so = null;
        [SerializeField] Item _itemPrefab = null;

        [SerializeField] bool _hasSpawned = false;

        private void Awake() {
            if (_so == null || _itemPrefab == null) {
                Debug.LogError("ItemSpawner: Missing ItemSO or Item Prefab");
                gameObject.SetActive(false);
                return;
            }
        }

        IEnumerator Start() {
            //yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(0.1f);

            if (!_hasSpawned)
                SpawnItem();
        }

        private void SpawnItem() {
            _itemPrefab = Instantiate(_itemPrefab, transform.position, Quaternion.identity);
            _itemPrefab.SetSO(_so);
            //guid
            //_itemPrefab.gameObject.GetComponent<SpawnedObject>().key = Guid.NewGuid().ToString();
            //_itemPrefab.gameObject.name = _itemPrefab.gameObject.GetComponent<SpawnedObject>().key;
            //_itemPrefab.gameObject.name = _so.name;

            //item.transform.position = transform.position;
            _hasSpawned = true;
        }

        public void SetSpawnState(bool state)
            => _hasSpawned = state;
        public bool GetSpawnState() => _hasSpawned;
    }
}