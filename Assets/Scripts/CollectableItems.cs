using UnityEngine;

public class CollectableItems : MonoBehaviour
{
    [SerializeField] public string ItemName;  // The item name
    [SerializeField] public string ItemBuffName;  // The buff this item give
    [SerializeField] public string ItemBuffValue;  // The value of the given buff
}
