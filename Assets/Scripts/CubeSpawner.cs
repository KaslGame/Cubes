using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CubeSpawner : MonoBehaviour
{
    private int _maxCubesSpawn = 6;
    private int _minCubesSpawn = 2;

    public event UnityAction<List<Rigidbody>, Cube> StateCubeChanged;
 
    public void TryCreateCube(Cube parent)
    {
        if (parent.IsDivision())
        {
            int generationAmount = 2;
            int multiplier = 2;
            int randomCountSpawn = Random.Range(_minCubesSpawn, _maxCubesSpawn);
            List<Rigidbody> cubeList = new();

            for (int i = 0; i < randomCountSpawn; i++)
            {
                Cube newCube = Instantiate(parent, parent.transform.position, Quaternion.identity);
                newCube.MakeNextGeneration(generationAmount);
                generationAmount *= multiplier;
                cubeList.Add(newCube.GetComponent<Rigidbody>());
            }

            StateCubeChanged?.Invoke(cubeList, parent);
        }
        else
        {
            List<Rigidbody> cubeList = new();
            StateCubeChanged?.Invoke(cubeList, parent);
        }

        Destroy(parent.gameObject);
    }
}