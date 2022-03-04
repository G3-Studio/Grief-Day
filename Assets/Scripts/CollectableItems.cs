using UnityEngine;

public class CollectableItems : MonoBehaviour
{
    [SerializeField] public string ItemName;  // The item name (must be in CollectableItems.json)
    public GameObject item;
    public int coinCount = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Coins")
        {
            // collision.gameObject.GetComponent<Movement>().CollectItem(ItemName);
            item = collision.gameObject;
            coinCount ++;
            Destroy(item);
        }
    }
}
