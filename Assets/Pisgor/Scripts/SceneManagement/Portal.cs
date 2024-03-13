// Copyright (c) Pixel Crushers. All rights reserved.

using UnityEngine;

using PixelCrushers;
using System.Collections;

namespace Pisgor.SceneManagement{

    /// <summary>
    /// This wrapper for PixelCrushers.ScenePortal keeps references intact if you switch 
    /// between the compiled assembly and source code versions of the original class.
    /// </summary>
    [AddComponentMenu("Pixel Crushers/Save System/Misc/Scene Portal")]
    public class ScenePortal : PixelCrushers.ScenePortal {
        public void Use() { 
            StartCoroutine(StartTransition());
        }

        IEnumerator StartTransition() { 
            //find player tag
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player?.GetComponent<Pisgor.Inventories.Inventory>()?.SilentDestroyItem();

            yield return new WaitForFixedUpdate();
            UsePortal();
        }
        /*
        public override void UsePortal() {
            base.UsePortal();
            /*
                if (isLoadingScene) return;
                isLoadingScene = true;
                onUsePortal.Invoke();
                LoadScene();
             
        }*/
    }
}
