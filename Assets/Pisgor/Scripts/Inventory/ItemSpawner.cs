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

            if (!_hasSpawned)
                SpawnItem();
        }

        private void SpawnItem() {
            _itemPrefab = Instantiate(_itemPrefab, transform.position, Quaternion.identity);
            _itemPrefab.SetSO(_so);

            //item.transform.position = transform.position;
            _hasSpawned = true;
        }
    }
}