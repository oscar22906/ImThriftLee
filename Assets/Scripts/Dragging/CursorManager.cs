using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    // Singleton instance
    public static CursorManager Instance { get; private set; }

    public Image cursorImage;
    public Vector2 cursorOffset = Vector2.zero;
    private bool isCustomCursorActive = true;

    private Animator animator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (isCustomCursorActive)
        {
            Cursor.visible = false;
        }
        animator = cursorImage?.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (isCustomCursorActive)
        {
            FollowMousePosition();
        }

        if (Input.GetMouseButtonDown(0))
        {
            ButtonDown();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ButtonUp();
        }
    }

    private void FollowMousePosition()
    {
        Vector2 mousePos = Input.mousePosition;
        cursorImage.transform.position = mousePos + cursorOffset;
    }

    public void ShowCursor()
    {
        isCustomCursorActive = true;
        cursorImage.enabled = true;
        Cursor.visible = false;
    }

    public void HideCursor()
    {
        isCustomCursorActive = false;
        cursorImage.enabled = false;
        Cursor.visible = true;
    }

    public void ChangeCursorSprite(Sprite newSprite)
    {
        cursorImage.sprite = newSprite;
    }

    public void HoverEnter()
    {
        animator?.SetBool("Hover", true);
    }
    public void HoverExit()
    {
        animator?.SetBool("Hover", false);
    }
    public void ButtonDown()
    {
        animator?.SetBool("Hover", false);
        animator?.SetBool("ButtonDown", true);
    }
    public void ButtonUp()
    {
        animator?.SetBool("ButtonDown", false);
    }
}
