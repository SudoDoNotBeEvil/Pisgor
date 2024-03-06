using Pisgor.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pisgor.Inventories {
    public class Item : MonoBehaviour {
        [SerializeField] ItemSO _so = null;
        [SerializeField] SpriteRenderer _spriteRenderer = null;

        [SerializeField] Trigger _trigger = null;

        public ItemSO SO { get { return _so; } }

        public void SetSO(ItemSO so) {
            CheckSpriteRenderer();

            _so = so;
            _spriteRenderer.sprite = so.Sprite;
        }

        private void CheckSpriteRenderer() {
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();

            if (_spriteRenderer == null)
                _spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }

        public void Use(GameObject go) {
            int xx = 42;

            var inv = go.GetComponent<Inventory>();
            if (inv == null || !inv.CanPickup(_so))
                return;

            go.GetComponent<Inventory>().Pickup(this);
        }

        internal void SetHolding(bool isHolding) {
            Debug.LogWarning("throw new NotImplementedException()");
        }
    }
}
