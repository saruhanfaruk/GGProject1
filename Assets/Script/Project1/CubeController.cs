using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    int line, column;//Olu�tuklar�ndaki noktalar�
    bool clicked, hasItBeenChecked;//T�kland�m� ve kontrol edildimi de�erleri
    Renderer render;

    public bool Clicked
    {
        get { return clicked; }
        set { clicked = value; }
    }
    public bool HasItBeenChecked 
    {
        get { return hasItBeenChecked; }  
        set { hasItBeenChecked = value; } 
    }

    private void OnEnable()
    {
        render = GetComponent<Renderer>();
        DefaultValues();
    }

    public void Click()//T�kland���nda yap�lacaklar
    {
        if (Clicked)
            return;
        Clicked = true;
        render.material = RefManager.Instance.cubeClickedMat; // �arp� i�aretini material ile veriyoruz.
        CubesManager.Instance.StartControl(line, column);

    }
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
    private void OnMouseDown()
    {
        Click();
    }
    public void DefaultValues()
    {
        HasItBeenChecked = false;
        Clicked = false;
        render.material = RefManager.Instance.cubeNotClickedMat;
    }

}
