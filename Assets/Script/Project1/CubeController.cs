using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    int index;
    public int Index
    {
        get { return index; }
        set { index = value; }
    }
    bool clicked;
    Renderer renderer;

    private void OnEnable()
    {
        renderer = GetComponent<Renderer>();
        renderer.material = RefManager.Instance.cubeNotClickedMat;
    }

    public void Click()
    {
        if (clicked)
            return;
        clicked = true;
        renderer.material = RefManager.Instance.cubeClickedMat;

    }
    private void OnMouseDown()
    {
        Click();
    }
    
}
