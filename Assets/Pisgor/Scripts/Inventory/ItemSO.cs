using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pisgor.Inventories {
    [CreateAssetMenu(fileName = "New Item", menuName = "Pisgor/Item")]

    public class ItemSO : ScriptableObject {
        [SerializeField] string _itemName = "New Item";
        [SerializeField] string _itemDescription = "New Item";
        [SerializeField] Sprite _sprite = null;
        [SerializeField] Sprite _icon = null;

        public string ItemName { get { return _itemName; } }
        public Sprite Sprite { get { return _icon; } }
        public Sprite Icon { get { return _icon; } }

        public GameObject Spawn() {
            GameObject go = new GameObject();
            var item = go.AddComponent<Item>();
            item.SetSO(this);

            return go;
        }
    }
}