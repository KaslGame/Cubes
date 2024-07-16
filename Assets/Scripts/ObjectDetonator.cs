using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CubeSpawner))]
public class ObjectDetonator : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private CubeSpawner _spawner;

    private void Awake()
    {
        _spawner = GetComponent<CubeSpawner>();   
    }

    private void OnEnable()
    {
        _spawner.StateCubeChanged += OnStateCubeChanged;
    }

    private void OnDisable()
    {
        _spawner.StateCubeChanged -= OnStateCubeChanged;
    }

    private void OnStateCubeChanged(List<Rigidbody> needCubes, Cube cube)
    {
        int minCount = 0;

        if (needCubes.Count > minCount)
        {
            Explode(needCubes, (needObject) => needObject.transform.position);
        }
        else
        {
            List<Rigidbody> cubes = GetExployedableCubes(cube.transform, cube.GenerationAmount);

            Explode(cubes, (_) => cube.transform.position, cube.GenerationAmount);
        }
    }

    private void Explode(List<Rigidbody> cubes, Func<Rigidbody, Vector3> func,int multiplier = 1)
    {
        foreach (Rigidbody explodableObject in cubes)
            explodableObject.AddExplosionForce(_explosionForce * multiplier, func.Invoke(explodableObject), _explosionRadius * multiplier);
    }

    private List<Rigidbody> GetExployedableCubes(Transform transform, int multiplier)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius * multiplier);

        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);

        return cubes;
    }
}