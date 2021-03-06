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
        string json = Resources.Load<TextAsset>("JSON/CollectableItems").text;
        buffList = JsonUtils.LoadJson<JsonUtils.CollectableItemJson>(json).buff;
    }

    void Start() {
        buff = buffList[Random.Range(0, buffList.Count)];
        if (buff.buff.name == "pv") {
            SpriteRenderer component = gameObject.GetComponent<SpriteRenderer>();
            component.sprite = Sprites.HEALTH_BOOST;
            component.transform.position = new Vector3(component.transform.position.x, component.transform.position.y + 0.14f,
                component.transform.position.z);
            component.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        } else if (buff.buff.name == "speed") {
            SpriteRenderer component = gameObject.GetComponent<SpriteRenderer>();
            component.sprite = Sprites.SWIFTNESS_BOOTS;
            component.transform.position = new Vector3(component.transform.position.x, component.transform.position.y + 0.04f,
                component.transform.position.z);
            component.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        } else if (buff.buff.name == "attack") {
            SpriteRenderer component = gameObject.GetComponent<SpriteRenderer>();
            component.sprite = Sprites.DEMON_FINGER;
            component.transform.position = new Vector3(component.transform.position.x, component.transform.position.y - 0.26f,
                component.transform.position.z);
            component.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

}
