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
            UnityEngine.Debug.Log("RecordData++");

            var item = GetComponent<Item>();

            if (item.SO != null) {
                string json = new ItemSaveData(item).GetJson();

                UnityEngine.Debug.Log("RecordData++" + json);

                return json;// "\"WUWUWU\":{\"xzzz\":999.0,\"yzzz\":999.0,\"zzzz\":999.0,\"wzzz\":1.0}";
            }
            return "dsfdsf";
        }

        public override void ApplyData(string data) {
            UnityEngine.Debug.LogError($"ApplyData >> {data} <<");
        }
    }
}