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
        } else if (collision.tag == "Buff") {
            item = collision.gameObject;
            gameObject.GetComponent<Player>().CollectItem(item.GetComponent<Buff>().buff);
            Destroy(item);
        } else if (collision.tag == "Skill") {
            Debug.Log("Collision skill");
            if (gameObject.GetComponent<Player>().inventory.GetSkillInSlot(0) != null) return;
            item = collision.gameObject;
            gameObject.GetComponent<Player>().inventory.AddSkill(item.GetComponent<Skill>().skill);
            Destroy(item);
        }
    }
}
