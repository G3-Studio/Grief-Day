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
    }

}
