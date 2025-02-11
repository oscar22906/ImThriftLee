using UnityEngine;

public class SprinkleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject specialPrefab;
    [SerializeField] private float maxChance;

    public void MakeSprinkles()
    {
        if (Random.Range(0f, maxChance) == 1)
        {
            Instantiate(specialPrefab);
        }
        else
        {
            Instantiate(prefab);
        }
    }
}
