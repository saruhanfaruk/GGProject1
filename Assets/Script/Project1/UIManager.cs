using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        CreateMap();
    }
    public void CreateMap()
    {
        try
        {
            int count = int.Parse(inputField.text);
            if(count>=3)
                CubesManager.Instance.CreateMap(count);
            else
                Debug.Log("3 veya daha büyük bir sayý giriniz.");
        }
        catch (System.Exception)
        {
            Debug.Log("Lütfen sayý giriniz.");
        }

    }
}
