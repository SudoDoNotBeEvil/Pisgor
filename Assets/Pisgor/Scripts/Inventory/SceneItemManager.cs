using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PixelCrushers;
using Pisgor.Inventories;
using JetBrains.Annotations;

//using SudoNo.Singletons;

namespace Pisgor.Inventories {
    class SaveItemManager { }

/*
    class ItemCollectionSaveData { 
        public List<string> saveStrings = new List<string>();


        public ItemCollectionSaveData() { }
        public ItemCollectionSaveData(string str) { 
            ApplyJson(str);
        }

        public void AddItem(Item item)
            => saveStrings.Add(new ItemSaveData(item).GetJson());

        public string GetJson()
            => JsonUtility.ToJson(this);

        public void ApplyJson(string json) {
            ItemCollectionSaveData saveData = JsonUtility.FromJson<ItemCollectionSaveData>(json);

            saveStrings = saveData?.saveStrings;
        }

        // iterator
        public IEnumerable<string> GetNext() {
            foreach (var itemData in saveStrings) {
                yield return itemData;
            }
        }
    }

    class ItemSaveData {
        public string soName;
        public Vector3 position;
        public bool isHolding;

        public ItemSaveData(Item item) {
            this.soName = item.SO.name;
            this.position = item.transform.position;
            //this.isHolding = item.IsHolding;
        }

        public string GetJson() {
            return JsonUtility.ToJson(this);
        }
    }

    public class SceneItemManager : Saver, IItemManager {
        //Singleton
        public static SceneItemManager Instance { get; private set; }

        [SerializeField] Item _itemPrefab;

        [SerializeField] List<Item> _items = new List<Item>();

        void Awake() {
            if (_itemPrefab == null) { 
                Debug.LogError("SceneItemManager: _itemPrefab is null");
                gameObject.SetActive(false);
                return;
            }

            if(Instance != null && Instance != this) {
                Debug.LogError("SceneItemManager: Instance already exists");
                Destroy(this);
                return;
            }
            else
                Instance = this;
        }

        #region
        public void RegisterItem(Item newItem) {
            if (newItem == null) {
                Debug.LogError("SceneItemManager: RegisterItem: newItem is null");
                return;
            }

            if (_items.Contains(newItem)) {
                Debug.LogError("SceneItemManager: RegisterItem: newItem is already registered");
                return;
            }

            _items.Add(newItem);
        }

        public void UnregisterItem(Item item) {
            if (item == null) {
                Debug.LogError("SceneItemManager: UnregisterItem: item is null");
                return;
            }

            if (!_items.Contains(item)) {
                Debug.LogWarning("SceneItemManager: UnregisterItem: item is not registered");
                return;
            }

            _items.Remove(item);
        }
        #endregion

        //SaveSystem by PixelCrushers
        #region SaveSystem
        public override void ApplyData(string s) {
            StartCoroutine(LoadState(s));
        }

        IEnumerator LoadState(string s) {
            if(s == null)
                yield break;

            yield return new WaitForSeconds(0.3f);

            foreach (var item in _items)
                Destroy(item.gameObject);

            // get all Item objects in the scene
            var items = FindObjectsOfType<Item>();
            foreach (var item in items)
                Destroy(item.gameObject);


            ItemCollectionSaveData saveData = new ItemCollectionSaveData(s);

            foreach (var jsonData in saveData.GetNext()) {
                Debug.Log($"Loading item: {jsonData}");

                ItemSaveData itemData = JsonUtility.FromJson<ItemSaveData>(jsonData);
                Item newItem = Instantiate(_itemPrefab, itemData.position, Quaternion.identity);
                ItemSO so = Resources.Load<ItemSO>("Items/" + itemData.soName);
                newItem.SetSO(so);
                newItem.SetHolding(itemData.isHolding);
            }
        }


        public override string RecordData() {
            ItemCollectionSaveData saveData = new ItemCollectionSaveData();

            foreach (var item in _items)
                saveData.AddItem(item);

            Debug.Log($"Saving: {saveData.GetJson()}");

            return saveData.GetJson();
        }

        /*


        PixelCrushers.SaveSystem.saveCurrentScene

        
#endregion
}
*/
}//namespace
