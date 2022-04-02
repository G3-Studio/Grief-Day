using UnityEngine;
using UnityEngine.U2D;

public class Sprites
{
    /// Skill sprites
    public static Sprite DASH = Resources.Load<Sprite>("Sprites/Items/dash");
    public static Sprite STUN = Resources.Load<Sprite>("Sprites/Items/stun");
    public static Sprite DOUBLE_JUMP = Resources.Load<Sprite>("Sprites/Items/double_jump");

    /// Boost sprites
    public static Sprite DEMON_FINGER = Resources.Load<Sprite>("Sprites/Items/demon_finger");
    public static Sprite HEALTH_BOOST = Resources.Load<Sprite>("Sprites/Items/health_boost");
    public static Sprite SWIFTNESS_BOOTS = Resources.Load<Sprite>("Sprites/Items/swiftness_boots");

    public static Sprite FromName(string name) {
        switch (name) {
            case null: return null;
            case "dash": return Sprites.DASH;
            case "demon_finger": return Sprites.DEMON_FINGER;
            case "double_jump": return Sprites.DOUBLE_JUMP;
            case "health_boost": return Sprites.HEALTH_BOOST;
            case "swiftness_boots": return Sprites.SWIFTNESS_BOOTS;
            default:
                Debug.LogWarning("This sprite is not implemented");
                return null;
        }
    }
}