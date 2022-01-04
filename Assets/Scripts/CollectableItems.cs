using UnityEngine;

public class CollectableItems : MonoBehaviour
{
    [SerializeField] public string ItemName;  // The item name (must be in CollectableItems.json)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            collision.gameObject.GetComponent<Player>().CollectItem(ItemName);
            Destroy(this);
        }
    }
}
