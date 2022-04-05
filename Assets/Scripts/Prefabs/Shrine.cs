using UnityEngine;
using Utils;

public class Shrine : MonoBehaviour
{
    public JsonUtils.CollectableItemJson.Skill skill;

    void Awake()
    {
        skill = Skills.getSkill();
    }

    
}
