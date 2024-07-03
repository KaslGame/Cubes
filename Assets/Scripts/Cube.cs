using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    private int _chanceDivision = 100;
    private int _generationAmount = 1;

    public void AddSpawner(Spawner spawner)
    {
        _spawner = spawner;
    }

    public bool RandomNumberCheck()
    {
        int minChance = 0;
        int maxChance = 100;
        int randomNumber = Random.Range(minChance, maxChance);

        if (randomNumber <= _chanceDivision / _generationAmount)
            return true;

        return false;
    }

    public void NextGeneration(int generationAmount)
    {
        _generationAmount = generationAmount;
        gameObject.transform.localScale /= _generationAmount;
        GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    private void OnMouseDown()
    {
        _spawner.CreateCube(this);
    }
}
