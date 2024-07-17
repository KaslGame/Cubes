using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CubeSpawner : MonoBehaviour
{
    private int _maxCubesSpawn = 6;
    private int _minCubesSpawn = 2;

    public event UnityAction<List<Rigidbody>, Cube> CubeSpawned;
    public event UnityAction<Cube> CubeDestroed;
 
    public void TryCreateCube(Cube parent)
    {
        if (parent.IsDivision())
        {
            int randomCountSpawn = Random.Range(_minCubesSpawn, _maxCubesSpawn);

            List<Rigidbody> cubeList = new();

            for (int i = 0; i < randomCountSpawn; i++)
            {
                Cube newCube = Instantiate(parent, parent.transform.position, Quaternion.identity);
                newCube.MakeNextGeneration(parent.GenerationAmount);
                cubeList.Add(newCube.GetComponent<Rigidbody>());
            }

            CubeSpawned?.Invoke(cubeList, parent);
        }
        else
        {
            CubeDestroed?.Invoke(parent);
        }

        Destroy(parent.gameObject);
    }
}