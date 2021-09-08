using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class playerInventoryManager : MonoBehaviour
{
    public int maxStack = 0;
    public void setMaxStackValue(int maxStackValue)
    {
        maxStack = maxStackValue;
    }
    public void swapIndexItemInList(List<PlayerInventoryListModel> playerIVList, int sourceInd, int targetInd)
    {
        var checkTargetBox = playerIVList.Any(s => s.index == targetInd);
        if (checkTargetBox)
        {
            var sItem = playerIVList.Where(s => s.index == sourceInd).FirstOrDefault();
            var tItem = playerIVList.Where(s => s.index == targetInd).FirstOrDefault();
            int sInd = playerIVList.IndexOf(sItem);
            int tInd = playerIVList.IndexOf(tItem);
            playerIVList[sInd].index = targetInd;
            playerIVList[tInd].index = sourceInd;
        }
        else
        {
            var sItem = playerIVList.Where(s => s.index == sourceInd).FirstOrDefault();
            int sInd = playerIVList.IndexOf(sItem);
            playerIVList[sInd].index = targetInd;
        }
        FindObjectOfType<GameManager>().player.inventoryList = playerIVList;
    }

    public List<PlayerInventoryListModel> moveToBin(List<PlayerInventoryListModel> playerIVList, List<PlayerInventoryListModel> binList, int itemIndex, bool half)
    {
        var removeItem = createNewPlayerInventoryModelItem(playerIVList[itemIndex]);
        if (half)
        {
            var checkOdd = removeItem.qty % 2 == 0;
            removeItem.qty = (checkOdd ? removeItem.qty / 2 : (removeItem.qty - 1) / 2);
            var theRest = (checkOdd ? removeItem.qty : (removeItem.qty + 1));
            binList = addItemIntoBinList(binList, removeItem);
            playerIVList[itemIndex].qty = theRest;
        }
        else
        {
            binList = addItemIntoBinList(binList, removeItem);
            playerIVList.Remove(playerIVList[itemIndex]);
        }
        FindObjectOfType<GameManager>().binList = binList;
        return playerIVList;
    }

    public List<PlayerInventoryListModel> addItemIntoBinList(List<PlayerInventoryListModel> binList, PlayerInventoryListModel removeItem)
    {
        var checkItem = binList.Any(s => s.itemName == removeItem.itemName && s.qty < maxStack);
        if (checkItem)
        {
            var temp = binList.Where(s => s.itemName == removeItem.itemName && s.qty < maxStack).LastOrDefault();
            var itemIndex = binList.IndexOf(temp);
            binList[itemIndex].qty += removeItem.qty;
            var theRest = binList[itemIndex].qty - maxStack;
            if (theRest > 0)
            {
                binList[itemIndex].qty = maxStack;
                var newRemoveItem = new PlayerInventoryListModel()
                {
                    index = binList.Count,
                    itemName = removeItem.itemName,
                    qty = theRest
                };
                binList = addItemIntoBinList(binList, newRemoveItem);
            }
        }
        else
        {
            if (removeItem.qty > maxStack)
            {

                var theRest = removeItem.qty - maxStack;
                removeItem.index = binList.Count;
                removeItem.qty = maxStack;
                binList.Add(removeItem);
                if (theRest > 0)
                {
                    var newRemoveItem = new PlayerInventoryListModel()
                    {
                        index = binList.Count,
                        itemName = removeItem.itemName,
                        qty = theRest
                    };
                    binList = addItemIntoBinList(binList, newRemoveItem);
                }
            }
            else
            {
                removeItem.index = binList.Count;
                binList.Add(removeItem);
            }
        }
        return binList;

    }

    public PlayerInventoryListModel createNewPlayerInventoryModelItem(PlayerInventoryListModel item)
    {

        var newItem = new PlayerInventoryListModel()
        {
            itemName = item.itemName,
            qty = item.qty,
            index = item.index
        };
        return newItem;
    }
}
