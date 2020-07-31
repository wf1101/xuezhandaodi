using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class Testing : MonoBehaviour
{
    private Grid grid;
    private List<Vector3> pair;
    private PathFind pathFind;
    private bool[,] boolGrid;
    private int winner;

    // Start is called before the first frame update
    private void Start()
    {
        grid = new Grid(8, 8, 5f, new Vector3(-20, -20));
        pair = new List<Vector3>();
        pathFind = new PathFind();
        boolGrid = pathFind.boolGrid;
        winner = 64;
    }  

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {   
            var position = Utils.GetMouseWorldPosition();

            if (grid.GetValue(position) != 1){
                HightlightCell(position, Color.yellow);
                if (!pair.Contains(position)){
                    pair.Add(position);
                }
                
                if (pair.Count == 2){

                    if (CanConnect())
                    {
                        // Debug.Log("===========connected==================");
                        
                        int x, y;
                        ModTwoNumber(out x, out y);
                        UpdateNumber(x, y);

                        // update boolGrid 
                        UpdateBoolGrid(x, y);
                    }
                
                    // unhighlight cells after calculation
                    HightlightCell(pair[0], Color.white);
                    HightlightCell(pair[1], Color.white);

                    pair.Clear();
                }
            }
        }
    }

    void ModTwoNumber(out int x, out int y){
        int num1 = grid.GetValue(pair[0]);
        int num2 = grid.GetValue(pair[1]);
        int gcd = GetGCD(num1, num2);
        x = num1 / gcd;
        y = num2 / gcd;
    }

    void UpdateNumber(int x, int y){      
        grid.SetValue(pair[0], x);
        grid.SetValue(pair[1], y);
    }

    void HightlightCell(Vector3 position, Color color){
        grid.SetColor(position, color);
    }

    // get greatest common divisor 
    public int GetGCD(int a, int b) {
        return b > 0 ? GetGCD(b, a % b) : a;
    }

    // check if two number can connect within 3 line path
    bool CanConnect(){
        int x1, y1;
        grid.GetXY(pair[0], out x1, out y1);
        int x2, y2;
        grid.GetXY(pair[1], out x2, out y2);

        // Debug.Log("position 1: " + x1 + " " + y1);
        // Debug.Log("position 2: " + x2 + " " + y2);
        return pathFind.CanConnect(x1, y1, x2, y2);
    }

    void UpdateBoolGrid(int x, int y){
        if (x == 1){
            int x1, y1;
            grid.GetXY(pair[0], out x1, out y1);    
            winner--;      
            pathFind.MarkEmpty(x1, y1);
        }
        if (y == 1) {
            int x2, y2;
            grid.GetXY(pair[1], out x2, out y2);
            pathFind.MarkEmpty(x2, y2);
            winner--;
        }
        if (winner == 0){
            // TODO: do somthing
        }
    }
}
