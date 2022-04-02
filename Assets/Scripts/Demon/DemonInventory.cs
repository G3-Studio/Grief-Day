using Utils;

public class DemonInventory {

    private JsonUtils.CollectableItemJson.Skill[] skills;

    public DemonInventory() {
        this.skills = new JsonUtils.CollectableItemJson.Skill[] { Skills.getSkill(), Skills.getSkill() };
    }
    
    public JsonUtils.CollectableItemJson.Skill GetSkill(int i) {
        return this.skills[i];
    }

    public void SetSkill(int i, JsonUtils.CollectableItemJson.Skill skill) {
        this.skills[i] = skill;
    }

}
