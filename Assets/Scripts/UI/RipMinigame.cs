using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class RipMinigame : MonoBehaviour
{
    [SerializeField] private SpriteRenderer iconDisplay;
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private GameObject[] tearPrefabs;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private AudioSource ripSound;
    [SerializeField] private AudioClip[] ripAudioClips;
    [SerializeField] private List<Sprite> backgroundStates;

    private int targetsRemaining = 3;
    private int currentAudioIndex = 0;
    private Clothing currentItem;
    private List<GameObject> spawnedTargets = new List<GameObject>();

    private void OnEnable()
    {
        ripSound = GameObject.FindGameObjectWithTag("OneShot")?.GetComponent<AudioSource>();
        StartCoroutine(StartMinigame());
    }

    public void SetupMinigame(Clothing item)
    {
        currentItem = item;
        iconDisplay.sprite = item.itemIcon;
        targetsRemaining = 3;
        currentAudioIndex = 0;
    }

    private IEnumerator StartMinigame()
    {
        yield return new WaitForSeconds(0.5f);
        SpawnNextTarget();
    }

    private void SpawnNextTarget()
    {
        if (targetsRemaining <= 0)
        {
            EndMinigame();
            return;
        }

        Bounds bounds = iconDisplay.bounds;
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        Vector3 spawnPosition = new Vector3(randomX, randomY, 0);
        GameObject newTarget = Instantiate(targetPrefab, spawnPosition, Quaternion.identity, transform);
        spawnedTargets.Add(newTarget);

        newTarget.GetComponent<RipTarget>().Initialize(this);
    }

    public void OnTargetClicked(Vector3 position)
    {
        targetsRemaining--;

        if (ripSound != null && ripAudioClips.Length > 0)
        {
            ripSound.PlayOneShot(ripAudioClips[Mathf.Clamp(currentAudioIndex, 0, ripAudioClips.Length - 1)]);
            currentAudioIndex++;
        }

        if (backgroundStates.Count > 0)
        {
            int index = Mathf.Clamp(3 - targetsRemaining, 0, backgroundStates.Count - 1);
            background.sprite = backgroundStates[index];
        }

        if (tearPrefabs.Length > 0)
        {
            GameObject tear = Instantiate(tearPrefabs[Random.Range(0, tearPrefabs.Length)], position, Quaternion.identity, transform);
            tear.transform.DOScale(Vector3.one * 1.2f, 0.2f).SetEase(Ease.OutBounce);
        }

        SpawnNextTarget();
    }

    private void EndMinigame()
    {
        foreach (GameObject target in spawnedTargets)
        {
            if (target) Destroy(target);
        }

        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
