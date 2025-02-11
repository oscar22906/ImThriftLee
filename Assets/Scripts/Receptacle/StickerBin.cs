using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System;

public class StickerBin : MonoBehaviour, IReceptacle
{
    [SerializeField] private GameObject[] stickers;

    [SerializeField] private GameObject stickerSpawnPoint;
    public static StickerBin Instance { get; private set; }

    private Animator animator;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        animator = GetComponent<Animator>();
    }

    public void DragBegin()
    {
        animator.SetBool("Dragging", true);
    }
    public void DragEnd()
    {
        animator.SetBool("Dragging", false);
    }

    public bool CanAccept(IDraggable draggable)
    {
        if (draggable != null && draggable.Type == IDraggable.DragType.Sticker) // only accept stickers
        {
            return true;
        }
        else
        {
            print("NOT A STICKER !!!");
            return false;
        }
    }

    public List<GameObject> GetList()
    {
        return null;
    }

    public void OnItemRecieve(IDraggable draggable)
    {
        if (draggable is MonoBehaviour draggableObject)
        {
            Destroy(draggableObject.gameObject);
        }
    }

    public void SpitOutSticker()
    {
        if (stickers.Length > 0)
        {
            animator.SetBool("Dragging", true);
            StartCoroutine(AnimateSpit());
            Instantiate(stickers[UnityEngine.Random.Range(0, stickers.Length)], stickerSpawnPoint.transform.position, stickerSpawnPoint.transform.rotation);
        }
    }

    private IEnumerator AnimateSpit()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Dragging", false);
    }
}
