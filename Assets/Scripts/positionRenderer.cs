using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class positionRenderer : MonoBehaviour
{
    //[SerializeField]
    private int sortingOrderBase = 100;
    private int offset = 100;
    private Renderer myRenderer;
    Grid grid;
    string[] snapGridBlackListArray = { "Player", "dropBusket", "enemy", "PlayerIndicator", "PlayerAttacker", "TallGrass" };
    string[] onTopObjectArray = { "FloatingText" };
    List<string> snapGridBlackList = new List<string>();
    private void Start()
    {
        snapGridBlackList = snapGridBlackListArray.ToList();
        grid = Grid.FindObjectOfType<Grid>();
        myRenderer = gameObject.GetComponent<Renderer>();
    }
    private void FixedUpdate()
    {
        if (onTopObjectArray.Contains(gameObject.tag))
        {
            myRenderer.sortingLayerName = "OnTop";
            return;
        }
        myRenderer.sortingOrder = (int)Mathf.Abs(((transform.position.y - sortingOrderBase) * offset));

        if (!snapGridBlackList.Contains(gameObject.tag))
        {
            Vector3Int gridPos = grid.LocalToCell(gameObject.transform.position);
            transform.localPosition = grid.GetCellCenterLocal(gridPos);
        }

    }
}
