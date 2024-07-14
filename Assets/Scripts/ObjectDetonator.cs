using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Spawner))]
public class ObjectDetonator : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private Spawner _spawner;

    private void Awake()
    {
        _spawner = GetComponent<Spawner>();   
    }

    private void OnEnable()
    {
        _spawner.CubeSpawned += OnCubeSpawned;
        _spawner.CubeDestroyed += OnCubeDestroyed;
    }

    private void OnDisable()
    {
        _spawner.CubeSpawned -= OnCubeSpawned;
        _spawner.CubeDestroyed -= OnCubeDestroyed;
    }

    private void OnCubeSpawned(List<Rigidbody> needCubes)
    {
        foreach (Rigidbody explodableObject in needCubes)
            explodableObject.AddExplosionForce(_explosionForce, explodableObject.position, _explosionRadius);
    }

    private void OnCubeDestroyed(Cube cube)
    {
        foreach (Rigidbody explodableObject in GetExployedableCubes(cube.transform, cube.GenerationAmount))
            explodableObject.AddExplosionForce(_explosionForce * cube.GenerationAmount, cube.transform.position, _explosionRadius * cube.GenerationAmount);

        Debug.Log(transform.transform.position);
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