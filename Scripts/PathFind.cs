using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFind
{
    private int[] deltaX = new int[]{-1, 1, 0, 0};
    private int[] deltaY = new int[]{0, 0, -1, 1};
    // public TextMesh[,] textGrid;
    public bool[,] boolGrid;
    
    public PathFind(){
        // this.textGrid = textGrid;
        boolGrid = new bool[10,10];
        
        for (int k = 0; k < 10; k++)
        {
            boolGrid[9, k] = true;
            boolGrid[k, 0] = true;
            boolGrid[0, k] = true;
            boolGrid[k, 9] = true;
        }
    }

    public void MarkEmpty(int x, int y){
        boolGrid[8 - y, x + 1] = true;
    }

    // check if two cells can be connected in maximum three lines
    public bool CanConnect(int x1, int y1, int x2, int y2)
    {
        if (PathHelper(8 - y1, x1 + 1, 8 - y2, x2 + 1, 0, 3, -1) != -1){
            return true;
        }
        return false;
    }

    private int PathHelper(int x1, int y1, int x2, int y2, int currSteps, int target, int preDir){
        if (currSteps > target)
        {
            return -1;       
        }
        if (x1 == x2 && y1 == y2)
        {
            return currSteps;
        }

        for (int i = 0; i < 4; i++)
        {
            int newX = x1 + deltaX[i];
            int newY = y1 + deltaY[i];
            // Debug.Log("NEWX NEWY " + newX + " " + newY);

            if (newX == x2 && newY == y2)
            {   
                return preDir == i ? currSteps : currSteps + 1;
            }

            if (newX >= 0 && newX < 10 && newY >= 0 && newY < 10 && boolGrid[newX, newY])
            {
                int steps = -1;
                if (i != preDir){
                    steps = PathHelper(newX, newY, x2, y2, currSteps + 1, target, i);
                } else
                {
                    steps = PathHelper(newX, newY, x2, y2, currSteps, target, i);
                }
                if(steps != -1){
                    return steps;
                }
            }
        }
        return -1;      
    }
}

