using UnityEngine;
using PixelCrushers;

namespace Pisgor.Interactive {
    [RequireComponent(typeof(TwoStateObject))]
    public class TwoStateObjectSaver : Saver {
        public override void ApplyData(string s) {
            if (string.IsNullOrEmpty(s)) return;
            GetComponent<TwoStateObject>().SetState(s == "1");
        }

        public override string RecordData() {
            return GetComponent<TwoStateObject>().State ? "1" : "0";
        }
    }
}