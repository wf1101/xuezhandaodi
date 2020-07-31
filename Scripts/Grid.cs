using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Grid
{
    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    public TextMesh[,] gridArray;

    // Problem
    string[,] Puzzle = new string[8, 8]{
        {"10","89","15","19","16","11","4","28"},
        {"33","3","16","14","78","58","7","55"},
        {"15","13","51","84","51","17","2","57"},
        {"21","65","38","47","29","42","47","15"},
        {"19","91","13","33","89","47","37","53"},
        {"23","130","89","2","89","114","74","51"},
        {"69","95","15","8","47","106","11","10"},
        {"26","63","66","14","28","32","13","9"},
    }; 

    public Grid(int w, int h, float cellSize, Vector3 originPosition)
    {
        this.width = w;
        this.height = h;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TextMesh[w, h];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = Utils.CreateWorldText(Puzzle[x,y], Color.white, null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 20, TextAnchor.MiddleCenter);         
                
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, h), GetWorldPosition(w, h), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(w, 0), GetWorldPosition(w, h), Color.white, 100f);
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            if (value == 1){
                gridArray[x, y].text= "";
            } else{
                gridArray[x, y].text = value.ToString();
            }
        }
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public int GetValue(int x, int y){        
        if (x >= 0 && y >= 0 && x < width && y < height){
            // if value == 1, set to empty string 
            if(gridArray[x, y].text == ""){
                return 1;
            }
            return Int32.Parse(gridArray[x, y].text);
        } else
        {
            return 0;
        }
    }

    public int GetValue(Vector3 worldPosition){
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }

    public void SetColor(Vector3 position, Color color){
        int x, y;
        GetXY(position, out x, out y);
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y].color = color;
        }
    }
}


