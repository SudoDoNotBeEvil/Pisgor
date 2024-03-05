using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pisgor.Interactable;

public class PrintTrigger : Triggerable {
    public override void OnPlayerEnter(GameObject player, float timeEnter = -1f) {
        base.OnPlayerExit(player, timeEnter);

        Debug.Log("Player Enter");
    }

    public override void OnPlayerExit(GameObject player, float deltaTime = -1f) {
        Debug.Log("Player Exit");
    }
}
