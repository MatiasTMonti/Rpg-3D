using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D mainCursor, attackCursor, talkCursor, itemCursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    private void OnGUI()
    {
        Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit2;

        if (Physics.Raycast(ray2, out hit2, 10000))
        {
            if (hit2.transform.tag == "enemy")
            {
                Cursor.SetCursor(attackCursor, hotSpot, cursorMode);
            }
            else if(hit2.transform.tag == "talkNpc")
            {
                Cursor.SetCursor(talkCursor, hotSpot, cursorMode);
            }
            else if (hit2.transform.tag == "item")
            {
                Cursor.SetCursor(itemCursor, hotSpot, cursorMode);
            }
            else
            {
                Cursor.SetCursor(mainCursor, hotSpot, cursorMode);
            }

        }
    }
}
