using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesManager : Singleton<CubesManager>
{
    int cubeCount = 3; 
    float spaceSize = .1f; // Küpler arasýndaki býrakýlacak boþluk mesafesi
    List<CubeController> cubeControllers = new List<CubeController>();// Oluþturulan küplerin listesi
    List<CubeController> correctChecklist = new List<CubeController>();// Kontrol sýrasýnda seçilen küplerin listesi
    float totalSpaceSize;//Toplam boþluk mesafesi
    float cubeSize;//Küplerin boyutu
    
    public void CreateMap(int newCount)
    {
        cubeCount = newCount;
        DestroyAllCubes();
        CreateCube();
        EditingPositionAndScale();
    }
    private void Update()
    {
        EditingPositionAndScale();
    }
    #region CubeSetting

    public void DestroyAllCubes()
    {
        while (cubeControllers.Count != 0)
        {
            Transform selectedCube = cubeControllers[0].transform;
            cubeControllers.RemoveAt(0);
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
                CubeController cubeController = newCube.gameObject.AddComponent<CubeController>();
                cubeController.SetLineAndColumn(i, j);
                cubeControllers.Add(cubeController);
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
                Transform newCube = cubeControllers[i * cubeCount + j].transform;
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
    #endregion
    #region Control
    public void StartControl(int line, int column)
    {
        correctChecklist.Clear();
        Check(line, column);
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
        list.Add((line + 1) * cubeCount + column);//Alt nokta
        list.Add((line - 1) * cubeCount + column);//Üst nokta
        if(column!= cubeCount - 1)
            list.Add(line * cubeCount + (column + 1));//Sað nokta
        if(column!=0)
            list.Add(line * cubeCount + (column - 1));//Sol nokta

        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i] < 0 || list[i] >= maxCount)
                list.RemoveAt(i);
        }
        return list;
    }
    #endregion

}

