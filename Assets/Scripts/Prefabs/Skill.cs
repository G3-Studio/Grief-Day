using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Skill : MonoBehaviour
{
    public JsonUtils.CollectableItemJson.Skill skill;
void Awake()
    {
        skill = Skills.getSkill();
    }
}
