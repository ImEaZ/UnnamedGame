using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MaxExpLevelRange
{
    public static int localLevel { get; set; } = 1;
    public static int lv_1_5 = 200;
    public static int lv_6_10 = 300;
    public static int lv_11_20 = 400;
    public static int lv_21_30 = 500;
    public static int lv_Rest = 500;
    public static int GetMaxStackLevel()
    {
        if (localLevel <= 5)
        {
            return lv_1_5;
        }
        else if (localLevel <= 10)
        {
            return lv_6_10;
        }
        else if (localLevel <= 20)
        {
            return lv_11_20;
        }
        else if (localLevel <= 30)
        {
            return lv_21_30;
        }
        else
        {
            return lv_Rest;
        }
    }
}
