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
    }

    private void OnDisable()
    {
        _spawner.CubeSpawned -= OnCubeSpawned;
    }

    private void OnCubeSpawned(List<Rigidbody> needCubes)
    {
        foreach (Rigidbody explodableObject in needCubes)
            explodableObject.AddExplosionForce(_explosionForce, explodableObject.position, _explosionRadius);
    }
}