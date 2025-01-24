using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{

    public static AnimationManager Instance { get; private set; }

    public event Action<int> OnFrameChanged;

    private int currentFrame;

    private void Awake() // singleton
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    public void SetFrame(int frame) // detect frame change and update frame
    {
        if (currentFrame != frame)
        {
            currentFrame = frame;
        }
        OnFrameChanged?.Invoke(currentFrame); // notifies listeners
    }

    public int GetCurrentFrame()
    {
        return currentFrame;
    }
}
