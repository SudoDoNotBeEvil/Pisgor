using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WritingOnTheWall : MonoBehaviour
{
    [SerializeField] Sprite[] digitsPalette_white;
    [SerializeField] Sprite[] digitsPalette_black;
    [SerializeField] SpriteRenderer[] digitsRenderers;
    private Sprite[] _activePalette;

    [SerializeField ]bool _isBlack = false;

    void Awake() { 
        if(_isBlack)
            _activePalette = digitsPalette_black;
        else
            _activePalette = digitsPalette_white;
    }

    IEnumerator Start()
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        var code = PersistentCodeGenerator.Instance.GetCode();
        for (int i = 0; i < digitsRenderers.Length; i++) {
            digitsRenderers[i].sprite = _activePalette[code[i]];
        }
    }
}
