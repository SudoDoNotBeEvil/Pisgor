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

        public void Pickup(Item item) {
            _currentItem = item;
            _currentItem.transform.position = _handPivot.position;
            _currentItem.transform.SetParent(transform);

            _currentItem.SetHolding(true);

        }

        public void Drop() { 
            if (_currentItem == null)
                return;

            _currentItem.transform.SetParent(null);
            _currentItem.transform.parent = ItemManager.Instance?.transform;

            //set z to 0
            _currentItem.transform.position = new Vector3(_currentItem.transform.position.x,
                                                          _currentItem.transform.position.y, 0);

            _currentItem.SetHolding(false);
            _currentItem = null;
        }
    }
}