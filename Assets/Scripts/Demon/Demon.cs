using System;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using Utils;

public class Demon : MonoBehaviour {

    public DemonInventory inventory { get; private set; }

    private void Awake() {
        this.inventory = new DemonInventory();
    }
}