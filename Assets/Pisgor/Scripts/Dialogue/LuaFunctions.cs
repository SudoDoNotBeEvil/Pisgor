using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PixelCrushers.DialogueSystem;
using Pisgor.Inventories;

public class LuaFunctions : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable() {
        Lua.RegisterFunction(nameof(PlayerHasItem), this, SymbolExtensions.GetMethodInfo(() => PlayerHasItem(string.Empty)));

        //        UnhandledTwineMacro("(CheckCrowbar)");
        Debug.Log("Registering Lua functions");
    }

    private void OnDisable() {
        // Note: If this script is on your Dialogue Manager & the Dialogue Manager is configured
        // as Don't Destroy On Load (on by default), don't unregister Lua functions.
        Debug.LogWarning("Unregistering Lua functions");
        Lua.UnregisterFunction(nameof(PlayerHasItem)); // <-- Only if not on Dialogue Manager.
    }

    public bool PlayerHasItem(string name) {

        var so = PersistentInventory.Instance.CurrentItemSO;

        Debug.Log($"Lua: PlayerHasItem{name} -- real: {so?.name}");
        if (so == null) return false;

        return so.name == name;
    }

    
    public void ConsumeHeldItem(string name) {
        Debug.Log("Lua: Consume held item");
    }

}
