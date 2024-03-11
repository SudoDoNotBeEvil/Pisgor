using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PixelCrushers;
using Pisgor.Inventories;
using System;

[RequireComponent(typeof(ItemSpawner))]
public class ItemSpawnerSaver : Saver {

    //generate GUID in editor for Key
    [ContextMenu("Generate GUID")]
    private void AssignNewGUID() {
        Debug.Log("Assigning new GUID");
        key = Guid.NewGuid().ToString();
    }

    public override void ApplyData(string s) {
        if (string.IsNullOrEmpty(s)) return;
        GetComponent<ItemSpawner>().SetSpawnState(s == "1");
        Debug.LogWarning($".SetSpawnState({s == "1"})");
    }

    public override string RecordData() {
        return GetComponent<ItemSpawner>().GetSpawnState() ? "1" : "0";
    }
}
