using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    int line, column;
    public void SetLineAndColumn(int newLine, int newColumn)
    {
        line = newLine;
        column = newColumn;
    }
    public int GetLine()
    {
        return line;
    }
    public int GetColumn()
    {
        return column;
    }
}
