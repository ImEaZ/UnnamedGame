using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DragAndDropScript : MonoBehaviour
{
    bool moveAllowed;
    public bool PCPlatform;
    public bool snapToGrid;
    Grid grid;
    Collider2D col;
    Touch touch;
    Vector2 touchPos;
    private void Start()
    {
        //PCPlatform = (Application.platform == RuntimePlatform.WindowsPlayer);
        col = GetComponent<Collider2D>();
        grid = Grid.FindObjectOfType<Grid>();
    }
    void Update()
    {
        if (PCPlatform)
        {
            #region PC
            if (moveAllowed && PCPlatform)
            {
                Dragging();
            }
            #endregion
        }
        else
        {
            #region Mobile dragNdrop
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                if (touch.phase == TouchPhase.Began)
                {
                    DragStart();
                }
                if (touch.phase == TouchPhase.Moved)
                {
                    Dragging();
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    DragRelease();
                }
            }
            #endregion
        }




    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D touchCollider = Physics2D.OverlapPoint(Input.mousePosition);
            moveAllowed = true;
        }
    }
    private void OnMouseUp()
    {
        moveAllowed = false;
    }
    void DragStart()
    {
        Collider2D touchCollider = Physics2D.OverlapPoint(touchPos);
        if (col == touchCollider)
        {
            moveAllowed = true;
        }
    }
    void Dragging()
    {
        if (moveAllowed)
        {
            if (PCPlatform)
            {
                   touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            if (snapToGrid)
            {
                Vector3Int gridPos = grid.LocalToCell(touchPos);
                transform.localPosition = grid.GetCellCenterLocal(gridPos);
            }
            else
            {
                transform.position = new Vector2(touchPos.x, touchPos.y);
            }
            

        }
    }
    void DragRelease()
    {
        moveAllowed = false;
    }
}
