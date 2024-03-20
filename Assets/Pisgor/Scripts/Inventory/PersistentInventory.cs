using UnityEngine;
using System.Diagnostics;
using System;

namespace Pisgor.Inventories {
    class InventorySaveData { 
        public string testString;

        public InventorySaveData(PersistentInventory inventory) {
            this.testString = inventory.testString;
        }

        public string GetJson() {
            return JsonUtility.ToJson(this);
        }
    }

    public class PersistentInventory : PixelCrushers.Saver {
        //singleton
        public static PersistentInventory Instance { get; private set; }

        private void Awake() {
            if (_itemPrefab == null) {
                UnityEngine.Debug.LogError("You must assign an item");
                return;
            }


            if (Instance != null && Instance != this) {
                UnityEngine.Debug.LogError("More than one PersistentInventory in the scene!");
                return;
            }
            Instance = this;
        }

        public string testString = "initial";
        public ItemSO CurrentItemSO { get; private set; }
        public bool HasItem => CurrentItemSO != null;
        [SerializeField] Item _itemPrefab;

        internal void SetHoldingItem(Item item) {
            UnityEngine.Debug.Log("PersInv: item picked");

            //item.gameObject.GetComponent<SpawnedObject_Item>().enabled = false;
            CurrentItemSO = item.SO;
        }

        internal void ItemDropped(Item item) {
            UnityEngine.Debug.Log("PersInv: item dropped");
            //item.gameObject.GetComponent<SpawnedObject_Item>().enabled = true;
            CurrentItemSO = null;
        }

        internal bool TryLoadItem(Inventory inventory) {
            if (!HasItem) return false;

            var instance = Instantiate(_itemPrefab, transform.position, Quaternion.identity);
            instance.SetSO(CurrentItemSO);
            inventory.Pickup(instance);

            return true;
        }

        public bool HoldsItem(ItemSO item) {
            return CurrentItemSO == item;
        }

        #region PixelCrashersSaver
        public override void ApplyData(string data) {
            UnityEngine.Debug.Log($"PersistentInventory.Load: >>{data}<<");

            InventorySaveData itemData = JsonUtility.FromJson<InventorySaveData>(data);
            testString = itemData.testString;
        }

        public override string RecordData() {
            InventorySaveData data = new InventorySaveData(this);

            UnityEngine.Debug.Log($"PersistentInventory.Save: >>{data.GetJson()}<<");

            return data.GetJson();
        }
        #endregion
    }
}