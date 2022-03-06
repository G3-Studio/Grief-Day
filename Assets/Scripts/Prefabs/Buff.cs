using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Buff : MonoBehaviour
{
    private List<JsonUtils.CollectableItemJson.Buff> buffList;
    public JsonUtils.CollectableItemJson.Buff buff;
    public int offset = 0;

    void Awake()
    {
        string json = System.IO.File.ReadAllText("Assets/Scenes/CollectableItems.json");
        buffList = JsonUtils.LoadJson<JsonUtils.CollectableItemJson>(json).buff;
    }

    void Start() {
        buff = buffList[Random.Range(0, buffList.Count)];
        if (buff.buff.name == "pv") {
            SpriteRenderer component = gameObject.GetComponent<SpriteRenderer>();
            component.sprite = Sprites.HEALTH_BOOST;
            component.transform.position = new Vector3(component.transform.position.x, component.transform.position.y + 0.37f,
                component.transform.position.z);
            component.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        } else if (buff.buff.name == "speed") {
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        } else if (buff.buff.name == "attack") {
            SpriteRenderer component = gameObject.GetComponent<SpriteRenderer>();
            component.sprite = Sprites.DEMON_FINGER;
            component.transform.position = new Vector3(component.transform.position.x, component.transform.position.y - 0.26f,
                component.transform.position.z);
            component.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

}
