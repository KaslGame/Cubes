using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    public int GenerationAmount => _generationAmount;

    private MeshRenderer _meshRenderer;

    private int _chanceDivision = 100;
    private int _generationAmount = 1;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnMouseDown()
    {
        _spawner.CreateCube(this);
    }

    public bool RandomNumberCheck()
    {
        int minChance = 0;
        int maxChance = 100;
        int randomNumber = Random.Range(minChance, maxChance);

        if (randomNumber <= _chanceDivision / _generationAmount) return true; return false;
    }

    public void NextGeneration(int generationAmount)
    {
        _generationAmount = generationAmount;
        gameObject.transform.localScale /= _generationAmount;
        _meshRenderer.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
