using System.Runtime.CompilerServices;
using UnityEngine;
using Utils;

public class TradingManager {

    public static bool isTrading { get; private set; }
    public static int playerSkill { get; private set; }
    public static int demonSkill { get; private set; }
    public static int sideSelected;
    public static PlayerInventory playerInventory;
    public static Demon demon;
    public static DemonInventory demonInventory;
    public static Player player;

    public static void StartTrading(Player pPlayer, Demon pDemon) {
        isTrading = true;
        playerSkill = -1;
        demonSkill = -1;
        player = pPlayer;
        playerInventory = player.inventory;
        demon = pDemon;
        demonInventory = demon.inventory;
        SelectSkillUI.instance.Enable(player.isPlayer1, playerInventory.GetSkillInSlot(0), playerInventory.GetSkillInSlot(1));
        player.currentUI = CurrentUI.CHOOSE_DEMON_ITEM;
    }

    public static void ChangeSelection(int side) {
        sideSelected = side;
        SelectSkillUI.instance.SetSelectedSkill(side);
    }

    public static void ConfirmChoice() {
        if (playerSkill == -1) {
            playerSkill = sideSelected;
            SelectSkillUI.instance.Enable(player.isPlayer1, demonInventory.GetSkill(0), demonInventory.GetSkill(1));
            sideSelected = 0;
        } else {
            demonSkill = sideSelected;
            JsonUtils.CollectableItemJson.Skill playerSkillObject = playerInventory.GetSkillInSlot(playerSkill);
            JsonUtils.CollectableItemJson.Skill demonSkillObject = demonInventory.GetSkill(demonSkill);
            demonInventory.SetSkill(demonSkill, playerSkillObject);
            playerInventory.SetSkill(playerSkill, demonSkillObject);
            SelectSkillUI.instance.Disable();
            demon.UpdateInventoryUI();
            isTrading = false;
            player.currentUI = CurrentUI.NONE;
            sideSelected = 0;
        }
    }

    /*
     * Returns 0 if the player is choosing his skill, 1 if he is choosing the demon skill
    */
    public static int GetTradingPhase() {
        return playerSkill == -1 ? 0 : 1;
    }



}