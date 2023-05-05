using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BoxCollider colli = GetComponent<BoxCollider>();
        //Debug.Log(colli.bounds.size.x);

        float screenHeightInUnits = Camera.main.orthographicSize * 2;
        float screenWidthInUnits = screenHeightInUnits * Screen.width / Screen.height; // basically height * screen aspect ratio

        //Debug.Log(screenHeightInUnits + "       " + screenWidthInUnits);
    
    }
}
