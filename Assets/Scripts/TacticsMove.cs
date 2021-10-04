using System.Collections;
using System.Collections.Generic;
using PathFind;
using UnityEngine;

public class TacticsMove : MonoBehaviour
{
    public Stack<Cell> path = new Stack<Cell>();
    public bool moving = false;


    public void MoveToTile(Cell cell)
    {
        path.Clear();
        cell.target = true;
        moving = true;

        Cell next = cell;
        while (next != null)
        {
            path.Push(next);
            next = next.parent;
            Debug.Log("MoveToTile" + next);
        }
    }
}