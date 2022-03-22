using System.Runtime.CompilerServices;
using UnityEngine;
using Utils;

public class TradingManager {

    public static bool isTrading { get; private set; }
    public static int playerSkill { get; private set; }
    public static int demonSkill { get; private set; }
    public static int sideSelected;
    public static PlayerInventory playerInventory;
    public static DemonInventory demonInventory;
    public static Player player;

    public static void StartTrading(Player pPlayer, DemonInventory pDemonInventory) {
        isTrading = true;
        playerSkill = -1;
        demonSkill = -1;
        player = pPlayer;
        playerInventory = player.inventory;
        demonInventory = pDemonInventory;
        SelectSkillUI.instance.Enable(playerInventory.GetSkillInSlot(0), playerInventory.GetSkillInSlot(1));
        player.currentUI = CurrentUI.CHOOSE_DEMON_ITEM;
    }

    public static void ChangeSelection(int side) {
        sideSelected = side;
        SelectSkillUI.instance.SetSelectedSkill(side);
    }

    public static void ConfirmChoice() {
        if (playerSkill == -1) {
            playerSkill = sideSelected;
            SelectSkillUI.instance.Enable(demonInventory.GetSkill(0), demonInventory.GetSkill(1));
        } else {
            demonSkill = sideSelected;
            JsonUtils.CollectableItemJson.Skill playerSkillObject = playerInventory.GetSkillInSlot(playerSkill);
            JsonUtils.CollectableItemJson.Skill demonSkillObject = demonInventory.GetSkill(demonSkill);
            demonInventory.SetSkill(sideSelected, playerSkillObject);
            playerInventory.SetSkill(sideSelected, demonSkillObject);
            SelectSkillUI.instance.Disable();
            isTrading = false;
            player.currentUI = CurrentUI.NONE;
        }
    }



}