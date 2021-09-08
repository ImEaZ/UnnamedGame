using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    public int exp { get; set; } = 0;
    public int xp { get; set; } = 0;
    public int lp { get; set; } = 0;
    public int level { get; set; } = 1;
    public int hpCurrent { get; set; } = 4;
    public int hpConstant { get; set; } = 4;
    public int maxStamina { get; set; } = 100;
    public int maxFood { get; set; } = 100;
    public int swordLvl { get; set; } = 1;
    public int axeLvl { get; set; } = 0;
    public int pickaxeLvl { get; set; } = 0;
    public int hammerLvl { get; set; } = 0;
    public List<PlayerInventoryListModel> inventoryList { get; set; } = new List<PlayerInventoryListModel>();

    #region Inhouse Items
    public int craftTableLevel { get; set; } = 0;
    public int forgeLevel { get; set; } = 0;
    #endregion


    #region Resources
    public int berry { get; set; } = 0;
    public int wood { get; set; } = 0;
    public int stone { get; set; } = 0;
    public int fiber { get; set; } = 0;
    public int vine { get; set; } = 0;
    public int rope { get; set; } = 0;
    public int log { get; set; } = 0;
    public int leather { get; set; } = 0;
    public int cookedmeat { get; set; } = 0;
    public int meat { get; set; } = 0;
    public int flint { get; set; } = 0;
    public int metal { get; set; } = 0;
    public int metalIngot { get; set; } = 0;
    public int charcoal { get; set; } = 0;
    #endregion
}
