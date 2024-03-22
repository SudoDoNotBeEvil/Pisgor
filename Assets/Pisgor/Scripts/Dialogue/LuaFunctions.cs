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
        Lua.RegisterFunction(nameof(DestroyItem), this, SymbolExtensions.GetMethodInfo(() => DestroyItem(string.Empty)));
        Lua.RegisterFunction(nameof(CanPickup), this, SymbolExtensions.GetMethodInfo(() => CanPickup()));
        Lua.RegisterFunction(nameof(TryGiveItem), this, SymbolExtensions.GetMethodInfo(() => TryGiveItem(string.Empty)));

        //        UnhandledTwineMacro("(CheckCrowbar)");
        Debug.Log("Registering Lua functions");
    }

    private void OnDisable() {
        // Note: If this script is on your Dialogue Manager & the Dialogue Manager is configured
        // as Don't Destroy On Load (on by default), don't unregister Lua functions.
        //Debug.LogWarning("Unregistering Lua functions");
        //Lua.UnregisterFunction(nameof(PlayerHasItem)); // <-- Only if not on Dialogue Manager.
        Debug.LogWarning("Unregister Lua functions might be required");
    }

    public bool PlayerHasItem(string name) {

        var so = PersistentInventory.Instance.CurrentItemSO;

        Debug.Log($"Lua: PlayerHasItem{name} -- real: {so?.name}");
        if (so == null) return false;

        return so.name == name;
    }

    public bool DestroyItem(string itemName) {
        return PersistentInventory.Instance.TryDestroyPlayerItem(itemName);
    }

    public bool TryGiveItem(string itemName) {
        return PersistentInventory.Instance.TrySpawnAndPickup(itemName);
    }

    public bool CanPickup() {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) {
            UnityEngine.Debug.LogError("No player found");
            return false;
        }

        var inv = player.GetComponent<Inventory>();
        if (inv == null) {
            UnityEngine.Debug.LogError("No inventory found");
            return false;
        }

        return inv.CanPickup();
    }

    public void ConsumeHeldItem(string name) {
        Debug.Log("Lua: Consume held item");
    }

}
