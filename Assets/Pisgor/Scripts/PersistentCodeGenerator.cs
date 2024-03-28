using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PixelCrushers;

public class PersistentCodeGenerator : Saver {
    private static PersistentCodeGenerator _instance = null;

    public static PersistentCodeGenerator Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<PersistentCodeGenerator>();
            }
            return _instance;
        }
    }

    public bool generated = false;
    public int[] digits = new int[4];

    private void Awake() {
        if (_instance != null && _instance != this) { 
            Debug.LogError("More than one PersistentCodeGenerator in the scene!");
            Destroy(gameObject);
            return;
        }
        _instance = this;

        if(!generated)
            GenerateCode();
    }

    public int[] GetCode() { 
        if (!generated)
            GenerateCode();

        return digits;
    }

    void GenerateCode() {
        for (int i = 0; i < digits.Length; i++) {
            digits[i] = Random.Range(0, 10);
        }
        generated = true;
    }

    [ContextMenu("ToFromJSONtest")]
    void ToFromJSONtest() {
        var json = RecordData();
        Debug.Log(json);
        ApplyData(json);
    }

    public override void ApplyData(string s) {
        if (string.IsNullOrEmpty(s)) return;

        Debug.Log(s);

        var data = JsonUtility.FromJson<CodeSaveData>(s);//Cannot deserialize JSON to new instances of type 'PersistentCodeGenerator.'

        digits = data.digits;
        generated = data.generated;
    }

    [ContextMenu("Show")]
    public override string RecordData() {
        var data = new CodeSaveData(this);
        return data.GetJson();
    }
}

class CodeSaveData {
    public bool generated = false;
    public int[] digits;

    public CodeSaveData(PersistentCodeGenerator codeGenerator) {
        this.generated = codeGenerator.generated;
        this.digits = codeGenerator.digits;
    }

    public string GetJson() {
        return JsonUtility.ToJson(this);
    }
}
