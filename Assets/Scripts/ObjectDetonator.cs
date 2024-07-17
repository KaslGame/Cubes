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
        _spawner.CubeSpawned += OnCubeSpawned;
        _spawner.CubeDestroed += OnCubeDestroed;
    }

    private void OnDisable()
    {
        _spawner.CubeSpawned -= OnCubeSpawned;
        _spawner.CubeDestroed -= OnCubeDestroed;
    }

    private void OnCubeSpawned(List<Rigidbody> needCubes, Cube cube)
    {
        Explode(needCubes, cube.transform.position);
    }

    private void OnCubeDestroed(Cube cube)
    {
        Explode(GetExployedableCubes(cube.transform.position, cube.GenerationAmount), cube.transform.position, cube.GenerationAmount);
    }

    private void Explode(List<Rigidbody> cubes, Vector3 postion, int multiplier = 1)
    {
        foreach (Rigidbody explodableObject in cubes)
            explodableObject.AddExplosionForce(_explosionForce * multiplier, postion , _explosionRadius * multiplier);
    }

    private List<Rigidbody> GetExployedableCubes(Vector3 position, int multiplier)
    {
        Collider[] hits = Physics.OverlapSphere(position, _explosionRadius * multiplier);

        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);

        return cubes;
    }
}