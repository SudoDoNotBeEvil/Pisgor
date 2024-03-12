using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pisgor.Inventories {
    public class Inventory : MonoBehaviour {
        public Item _currentItem = null;

        [SerializeField] Transform _handPivot;

        public bool CanPickup(ItemSO so) {
            return _currentItem == null;
        }

        IEnumerator Start() {
            for(int i=0; i<3; i++)
                yield return new WaitForFixedUpdate();

            PersistentInventory.Instance.TryLoadItem(this);
        }

        public void Pickup(Item item) {
            if (item == null || item == _currentItem) {
                Debug.LogError(CanPickup(null) ? "Item is null" : "Already holding an item");
                return;
            }

            _currentItem = item;
            _currentItem.transform.position = _handPivot.position;
            _currentItem.transform.SetParent(transform);

            _currentItem.SetHolding(true);
            PersistentInventory.Instance.SetHoldingItem(item);
        }

        public void SilentDestroyItem() { 
            if (_currentItem == null) 
                return;

            var item = _currentItem;
            item.GetComponent<SpawnedObject_Item>().enabled = false;
            Destroy(item.GetComponent<SpawnedObject_Item>());
            Drop(silent: true);
            Destroy(item.gameObject);
        }

        public void Drop(bool silent = false) { 
            if (_currentItem == null)
                return;

            if(!silent)
                PersistentInventory.Instance.ItemDropped(_currentItem);

            _currentItem.transform.SetParent(null);
            //_currentItem.transform.parent = ItemManager.Instance?.transform;

            //set z to 0
            _currentItem.transform.position = new Vector3(_currentItem.transform.position.x,
                                                          _currentItem.transform.position.y, 0);

            _currentItem.SetHolding(false);
            _currentItem = null;
        }
    }
}