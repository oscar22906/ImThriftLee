using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Manages sprite renderer on specific body part, and contains methods for basic actions. Controlled by OutfitManager.

public class BodyPartRenderer : MonoBehaviour
{
    public BodyPart bodyPart;
    Clothing currentClothing;

    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer.GetComponent<SpriteRenderer>();
    }


    public void UpdateClothing(int currentFrame)
    {
        if (currentFrame > currentClothing.sprites.Length)
        {
            spriteRenderer.sprite = currentClothing.sprites[currentFrame];
        }
        Debug.Log("Frame out of clothing sprite bound.");
    }

    public void ResetClothing()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = null;
            currentClothing = null; // possible null exception
        }
    }

    public void SetClothing(Clothing cl) 
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = cl.sprites[0];
            currentClothing = cl;
        }
    }
    public void ApplySet(OutfitSet set)
    {

    }
    public void ApplySet(int setNumber) // int overload method variation
    {

    }
}
