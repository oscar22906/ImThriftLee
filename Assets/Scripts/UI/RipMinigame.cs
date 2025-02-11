using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System;

public class RipMinigame : MonoBehaviour
{
    [SerializeField] private SpriteRenderer iconDisplay;
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private GameObject[] tearPrefabs;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private AudioSource ripSound;
    [SerializeField] private AudioClip[] ripAudioClips;
    [SerializeField] private List<Sprite> backgroundStates;
    [SerializeField] private float failTime = 5f;

    private int targetsRemaining = 3;
    private int currentAudioIndex = 0;
    private Clothing currentItem;
    private List<GameObject> spawnedTargets = new List<GameObject>();
    private Coroutine failTimer;

    public static event Action OnMinigameEnd;

    private void OnEnable()
    {
        ripSound = GameObject.FindGameObjectWithTag("ThriftAudio")?.GetComponent<AudioSource>();
        StartCoroutine(StartMinigame());
    }

    public void SetupMinigame(Clothing item)
    {
        MapManager.Instance.DisableAllCollider();
        currentItem = item;
        targetsRemaining = 3;
        currentAudioIndex = 0;
        iconDisplay.sprite = item.shootIcons[0]; // Set initial sprite
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
        float randomX = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        float randomY = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

        GameObject newTarget = Instantiate(targetPrefab, spawnPosition, Quaternion.identity, transform);
        spawnedTargets.Add(newTarget);
        newTarget.GetComponent<RipTarget>().Initialize(this);

        ResetFailTimer();
    }

    public void OnTargetClicked(Vector3 position)
    {
        targetsRemaining--;

        if (currentItem.shootIcons.Length > targetsRemaining)
        {
            int spriteIndex = 3 - targetsRemaining;
            if (spriteIndex < currentItem.shootIcons.Length)
            {
                iconDisplay.sprite = currentItem.shootIcons[spriteIndex];
            }
        }

        if (ripSound != null && ripAudioClips.Length > 0)
        {
            ripSound.PlayOneShot(ripAudioClips[currentAudioIndex]);
            currentAudioIndex = Mathf.Min(currentAudioIndex + 1, ripAudioClips.Length - 1); // Ensure last sound plays
        }

        if (backgroundStates.Count > 0)
        {
            int index = Mathf.Clamp(3 - targetsRemaining, 0, backgroundStates.Count - 1);
            background.sprite = backgroundStates[index];
        }

        if (tearPrefabs.Length > 0)
        {
            GameObject tear = Instantiate(tearPrefabs[UnityEngine.Random.Range(0, tearPrefabs.Length)], position, Quaternion.identity, transform);
            tear.transform.DOScale(Vector3.one * 1.2f, 0.2f).SetEase(Ease.OutBounce);
        }

        SpawnNextTarget();
    }

    private void ResetFailTimer()
    {
        if (failTimer != null)
            StopCoroutine(failTimer);
        failTimer = StartCoroutine(FailCountdown());
    }

    private IEnumerator FailCountdown()
    {
        yield return new WaitForSeconds(failTime);
        FailMinigame();
    }

    private void FailMinigame()
    {
        Debug.Log("Minigame failed due to timeout!");
        EndMinigame();
    }

    private void EndMinigame()
    {
        OnMinigameEnd?.Invoke();
        ItemDisplay.Instance.OnMinigameSuccess(currentItem);
        MapManager.Instance.currentShop.EnableButtonColliders();
        foreach (GameObject target in spawnedTargets)
        {
            if (target) Destroy(target);
        }

        StopAllCoroutines();
        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
