using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Shrine : MonoBehaviour
{
    public JsonUtils.CollectableItemJson.Skill skill;

    private bool isPlayer1;
    private GameInputs inputs;
    void Awake()
    {
        skill = Skills.getSkill();
        inputs = new GameInputs();
    }

    private void Start() {
        Debug.Log(skill.name);
    }

    
}
