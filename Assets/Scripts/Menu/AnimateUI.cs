using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimateUI : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI[] text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableDebug()
    {
        image.color = new Color(0.5f, 0.5f, 0.5f, 0);
        foreach (TextMeshProUGUI t in text)
        {
            t.color = new Color(1, 1, 1, 0);
        }
    }
    public void EnableDebug()
    {
        image.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        foreach (TextMeshProUGUI t in text)
        {
            t.color = new Color(1, 1, 1, 1);
        }
    }
}
