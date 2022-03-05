using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Buff : MonoBehaviour
{
    private List<JsonUtils.CollectableItemJson.Buff> buffList;
    public JsonUtils.CollectableItemJson.Buff buff;

    void Awake()
    {
        string json = System.IO.File.ReadAllText("Assets/Scenes/CollectableItems.json");
        buffList = JsonUtils.LoadJson<JsonUtils.CollectableItemJson>(json).buff;
    }

    void Start() {
        buff = buffList[Random.Range(0, buffList.Count)];
        if (buff.buff.name == "pv") {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        } else if (buff.buff.name == "speed") {
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        } else if (buff.buff.name == "attack") {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

}
