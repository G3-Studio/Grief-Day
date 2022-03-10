using System;
using UnityEngine;

namespace Utils {
    public class Prefabs : MonoBehaviour {

        public static Prefabs instance { get; private set; }
        
        public static GameObject FIREBALL = Resources.Load<GameObject>("Prefabs/fireball.prefab");

        private void Awake() {
            Prefabs.instance = this;
        }
    }
}