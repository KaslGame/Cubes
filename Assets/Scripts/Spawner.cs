using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    public event UnityAction<List<Rigidbody>> CubeSpawned;

    private int _maxCubesSpawn = 6;
    private int _minCubesSpawn = 2;

    public void CreateCube(Cube parent)
    {
        if (parent.RandomNumberCheck())
        {
            int generationAmount = 2;
            int multiplier = 2;
            int randomCountSpawn = Random.Range(_minCubesSpawn, _maxCubesSpawn);
            List<Rigidbody> cubeList = new List<Rigidbody>();

            Destroy(parent.gameObject);

            for (int i = 0; i < randomCountSpawn; i++)
            {
                Cube newCube = Instantiate(parent, parent.transform.position, Quaternion.identity);
                newCube.NextGeneration(generationAmount);
                generationAmount *= multiplier;
                cubeList.Add(newCube.GetComponent<Rigidbody>());
            }

            CubeSpawned?.Invoke(cubeList);
        }
        else
        {
            Destroy(parent.gameObject);
        }
    }
}