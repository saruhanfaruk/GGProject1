using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesManager : MonoBehaviour
{
    int cubeCount = 4;
    float spaceSize = .1f;
    List < Transform> cubes = new List<Transform>();
    float totalSpaceSize;
    float cubeSize;

    void Start()
    {
        DestroyAllCubes();
        CreateCube();
        EditingPositionAndScale();
    }

    public void DestroyAllCubes()
    {
        while (cubes.Count != 0)
        {
            Transform selectedCube = cubes[0];
            cubes.RemoveAt(0);
            DestroyImmediate(selectedCube.gameObject);
        }
    }
    public void CreateCube()
    {
        for (int i = 0; i < cubeCount*cubeCount; i++)
        {
            Transform newCube = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
            CubeController cubeController = newCube.gameObject.AddComponent<CubeController>();
            cubeController.Index = i;
            cubes.Add(newCube);
        }
        //for (int i = 0; i < cubeCount; i++)
        //{
        //    for (int j = 0; j < cubeCount; j++)
        //    {
        //        Transform newCube = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        //        newCube.name = i + "-" + j;
        //        CubeController cubeController = newCube.gameObject.AddComponent<CubeController>();
        //        cubeController.SetLineAndColumn(i, j);
        //        cubes.Add(newCube);
        //    }
        //}
    }
    public void EditingPositionAndScale()
    {
        CameraManager cameraManager = CameraManager.Instance;
        UpdateValues();
        Vector3 currentCubePos = new Vector3(cameraManager.Left(), cameraManager.Top(), 0);
        for (int i = 0; i < cubeCount; i++)
        {
            currentCubePos.y -= spaceSize + cubeSize * .5f;
            for (int j = 0; j < cubeCount; j++)
            {
                Transform newCube = cubes[i * cubeCount + j];
                newCube.SetParent(transform);
                newCube.localScale = Vector3.one * cubeSize;
                currentCubePos.x += spaceSize + cubeSize * .5f;
                newCube.position = currentCubePos;
                currentCubePos.x += cubeSize * .5f;
            }
            currentCubePos.x = cameraManager.Left();
            currentCubePos.y -= cubeSize * .5f;
        }
    }
    public void UpdateValues()
    {
        totalSpaceSize = spaceSize * (cubeCount + 1);
        cubeSize = (CameraManager.Instance.CameraSize() - totalSpaceSize) / cubeCount;
    }

    public void Check(int selectedIndex)
    {

    }
    public void CheckRightSide()
    {

    }

}

