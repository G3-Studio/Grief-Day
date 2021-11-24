using UnityEngine;

public class CollectableItems : MonoBehaviour
{
    [SerializeField] public string ItemName;  // The item name (must be in CollectableItems.json)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            collision.gameObject.GetComponent<Movement>().CollectItem(ItemName);
            Destroy(this);
        }
    }
}
