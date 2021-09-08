using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerTemp : MonoBehaviour
{
    public int exp = 0;
    public int xp = 0;
    public int lp = 0;
    public int level = 1;
    public int hpCurrent = 4;
    public int hpConstant = 4;
    public int maxStamina = 100;
    public int maxFood = 100;
    public int swordLvl = 1;
    public int axeLvl = 0;
    public int pickaxeLvl = 0;
    public int hammerLvl = 0;
    public List<PlayerInventoryListModel> inventoryList = new List<PlayerInventoryListModel>();
    public int craftTableLevel = 0;
    public int forgeLevel = 0;
    #region Resources
    public int berry = 0;
    public int wood = 0;
    public int stone = 0;
    public int fiber = 0;
    public int vine = 0;
    public int rope = 0;
    public int log = 0;
    public int leather = 0;
    public int cookedmeat = 0;
    public int meat = 0;
    public int flint = 0;
    public int metal = 0;
    public int metalIngot = 0;
    public int charcoal = 0;
    #endregion
    public void SavePlayer()
    {
        PlayerData newData = new PlayerData()
        {
            exp = exp,
            xp = xp,
            lp = lp,
            level = level,
            hpCurrent = hpCurrent,
            hpConstant = hpConstant,
            maxStamina = maxStamina,
            maxFood = maxFood,
            swordLvl = swordLvl,
            axeLvl = axeLvl,
            pickaxeLvl = pickaxeLvl,
            hammerLvl = hammerLvl,
            berry = berry,
            wood = wood,
            stone = stone,
            fiber = fiber,
            vine = vine,
            rope = rope,
            log = log,
            leather = leather,
            cookedmeat = cookedmeat,
            meat = meat,
            flint = flint,
            metal = metal,
            metalIngot = metalIngot,
            charcoal = charcoal,
            inventoryList = inventoryList,
            craftTableLevel = craftTableLevel,
            forgeLevel = forgeLevel
        };
        SaveSystem.SavePlayer(newData);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        exp = data.exp;
        xp = data.xp;
        lp = data.lp;
        level = data.level;
        hpCurrent = data.hpCurrent;
        hpConstant = data.hpConstant;
        maxStamina = data.maxStamina;
        maxFood = data.maxFood;
        swordLvl = data.swordLvl;
        axeLvl = data.axeLvl;
        pickaxeLvl = data.pickaxeLvl;
        hammerLvl = data.hammerLvl;
        berry = data.berry;
        wood = data.wood;
        stone = data.stone;
        fiber = data.fiber;
        vine = data.vine;
        rope = data.rope;
        log = data.log;
        leather = data.leather;
        cookedmeat = data.cookedmeat;
        meat = data.meat;
        flint = data.flint;
        metal = data.metal;
        metalIngot = data.metalIngot;
        charcoal = data.charcoal;
        inventoryList = data.inventoryList ?? new List<PlayerInventoryListModel>();
        craftTableLevel = data.craftTableLevel;
        forgeLevel = data.forgeLevel;
    }
    public void ResetPlayerData()
    {
        SaveSystem.SavePlayer(new PlayerData());
        exp = 0;
        xp = 0;
        lp = 0;
        level = 1;
        hpCurrent = 4;
        hpConstant = 4;
        maxStamina = 100;
        maxFood = 100;
        swordLvl = 0;
        axeLvl = 0;
        pickaxeLvl = 0;
        hammerLvl = 0;
        wood = 0;
        stone = 0;
        fiber = 0;
        vine = 0;
        rope = 0;
        log = 0;
        leather = 0;
        cookedmeat = 0;
        meat = 0;
        flint = 0;
        metal = 0;
        metalIngot = 0;
        charcoal = 0;
        inventoryList = new List<PlayerInventoryListModel>();
        craftTableLevel = 0;
        forgeLevel = 0;
    }
}
