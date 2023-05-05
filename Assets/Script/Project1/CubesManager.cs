using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesManager : Singleton<CubesManager>
{
    int cubeCount = 4;
    float spaceSize = .1f;
    List < Transform> cubes = new List<Transform>();
    List<CubeController> cubeControllers = new List<CubeController>();
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
        for (int i = 0; i < cubeCount; i++)
        {
            for (int j = 0; j < cubeCount; j++)
            {
                Transform newCube = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
                newCube.name = i + "-" + j;
                CubeController cubeController = newCube.gameObject.AddComponent<CubeController>();
                cubeController.SetLineAndColumn(i, j);
                cubeControllers.Add(cubeController);
                cubes.Add(newCube);
            }
        }
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
    List<CubeController> correctChecklist = new List<CubeController>();
    public void StartControl(int line, int column)
    {
        correctChecklist.Clear();
        Check(line, column);
        Debug.Log(correctChecklist.Count);
        if(correctChecklist.Count>=3)
            foreach (var item in correctChecklist)
                item.DefaultValues();
        EndControl();
    }
    public void EndControl()
    {
        foreach (var item in cubeControllers)
        {
            item.HasItBeenChecked = false;
        }
    }

    public void Check(int line, int column)
    {
        List<int> indexesToCheck = Checklist(line, column);
        foreach (var item in indexesToCheck)
        {
            if (cubeControllers[item].Clicked && !cubeControllers[item].HasItBeenChecked)
            {
                correctChecklist.Add(cubeControllers[item]);
                cubeControllers[item].HasItBeenChecked = true;
                Check(cubeControllers[item].GetLine(), cubeControllers[item].GetColumn());
            }
        }
    }
    
    public List<int> Checklist(int line, int column)
    {
        int maxCount = cubeCount * cubeCount;
        List<int> list = new List<int>();
        list.Add((line + 1) * cubeCount + column);
        list.Add((line - 1) * cubeCount + column);
        list.Add(line * cubeCount + (column + 1));
        list.Add(line * cubeCount + (column - 1));
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i] < 0 || list[i] >= maxCount)
                list.RemoveAt(i);
        }
        return list;
    }

}

