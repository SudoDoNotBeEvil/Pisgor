using System.Diagnostics;
using UnityEngine;

namespace Pisgor.Inventories {

    class ItemSaveData {
        public string soName;
        //public Vector3 position;
        public bool isHolding;

        public ItemSaveData(Item item) {
            this.soName = item.SO.name;
            //this.position = item.transform.position;
            this.isHolding = item.IsHolding;
        }

        public string GetJson() {
            return JsonUtility.ToJson(this);
        }
    }

    [RequireComponent(typeof(Item))]
    public class SpawnedObject_Item : PixelCrushers.SpawnedObject {
        public override string RecordData() {
            string xx = base.RecordData();
            //UnityEngine.Debug.Log("RecordData++");

            var item = GetComponent<Item>();

            if (item.SO != null) {
                string json = new ItemSaveData(item).GetJson();

                //UnityEngine.Debug.Log("RecordData++" + json);
                UnityEngine.Debug.Log($"Save item {json}");


                return xx + json;// "\"WUWUWU\":{\"xzzz\":999.0,\"yzzz\":999.0,\"zzzz\":999.0,\"wzzz\":1.0}";
            }
            return xx + "";
        }

        public override void ApplyData(string data) {
            base.ApplyData(data);

            if (string.IsNullOrEmpty(data))
                return;

            UnityEngine.Debug.Log($"Load item {data}");

            ItemSaveData itemData = JsonUtility.FromJson<ItemSaveData>(data);

            ItemSO so = Resources.Load<ItemSO>("Items/" + itemData.soName);
            if (so == null) { 
                UnityEngine.Debug.LogError($"Can't'load an item: ItemSO not found: {itemData.soName}, {data}");
                return;
            }

            GetComponent<Item>().SetSO(so);
            GetComponent<Item>().SetHolding( itemData.isHolding );
        }
    }
}