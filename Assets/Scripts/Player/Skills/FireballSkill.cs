using System;
using System.Resources;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class FireballSkill : SkillEffect {

    [CanBeNull] private GameObject fireball;
    
    protected override TimeSpan duration { get => TimeSpan.Zero; }
    protected override TimeSpan cooldown { get => new TimeSpan(0, 0, 10); }
    public override string skillName => "fireball";
    
    public bool isValid {
        get => fireball != null;
    }

    protected override void tickStart(Player player, Rigidbody2D rigidBody, Vector2 movement) {
        this.fireball = Object.Instantiate(Utils.Prefabs.FIREBALL);
        fireball.transform.position = rigidBody.transform.position;
        fireball.GetComponent<Rigidbody2D>().velocity = rigidBody.velocity;
    }

    protected override void tick(Player player, Rigidbody2D rigidBody, Vector2 movement) {
        RaycastHit2D[] hits;

        //We raycast down 1 pixel from this position to check for a collider
        Vector2 positionToCheck = this.fireball.transform.position;
        hits = Physics2D.RaycastAll(positionToCheck, this.fireball.GetComponent<Rigidbody2D>().velocity, 0.1f);

        foreach (RaycastHit2D hit in hits) {
            if (!hits[0].collider.tag.EndsWith("Stair") && !hits[0].collider.tag.EndsWith("Player")) {
                Object.Destroy(fireball);
            }
        }
    }

    protected override void tickEnd(Player player, Rigidbody2D rigidBody, Vector2 movement) { }
}