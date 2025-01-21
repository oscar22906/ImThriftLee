using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericInteractble : MonoBehaviour, IInteractable
{
    public bool isInteractable;

    public AudioSource audioSource;
    public AudioClip interact;
    public AudioClip hovering;
    public AudioClip nothovering;

    public void Interact()
    {
        if (IsInteractable())
        {
            Debug.Log("Interacted");
            PlaySound(interact);
        }
    }

    public bool IsInteractable()
    {
        return isInteractable;
    }

    public void OnHoverEnter()
    {
        Debug.Log("Started Hovering");
        GetComponent<SpriteRenderer>().color = Color.black;
        PlaySound(hovering);
    }

    public void OnHoverExit()
    {
        Debug.Log("Stopped Hovering");
        GetComponent<SpriteRenderer>().color = Color.white;
        PlaySound(nothovering);
    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource != null)
        {
            if (clip != null)
            {
                audioSource.PlayOneShot(clip);
            } else { Debug.Log("Clip is null"); }
        }
        else { Debug.Log("No Audio Source"); }
    }
}
