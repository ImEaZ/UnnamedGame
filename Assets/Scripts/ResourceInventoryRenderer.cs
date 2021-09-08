using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceInventoryRenderer : MonoBehaviour
{
    public GameObject inventoryPanel;
    public PlayerTemp playerTemp;
    public Text textAmt;

    private void FixedUpdate()
    {
        if (inventoryPanel.activeSelf)
        {
            switch (gameObject.name)
            {
                case "stone":
                    textAmt.text = playerTemp.stone.ToString("#,##0");
                    break;
                case "wood":
                    textAmt.text = playerTemp.wood.ToString("#,##0");
                    break;
                case "fiber":
                    textAmt.text = playerTemp.fiber.ToString("#,##0");
                    break;
                case "berry":
                    textAmt.text = playerTemp.berry.ToString("#,##0");
                    break;
                case "vine":
                    textAmt.text = playerTemp.vine.ToString("#,##0");
                    break;
                case "rope":
                    textAmt.text = playerTemp.rope.ToString("#,##0");
                    break;
                case "log":
                    textAmt.text = playerTemp.log.ToString("#,##0");
                    break;
                case "meat":
                    textAmt.text = playerTemp.meat.ToString("#,##0");
                    break;
                case "cookedmeat":
                    textAmt.text = playerTemp.cookedmeat.ToString("#,##0");
                    break;
                case "flint":
                    textAmt.text = playerTemp.flint.ToString("#,##0");
                    break;
                case "metal":
                    textAmt.text = playerTemp.metal.ToString("#,##0");
                    break;
                case "metalingot":
                    textAmt.text = playerTemp.metalIngot.ToString("#,##0");
                    break;
            }
        }
    }
}
