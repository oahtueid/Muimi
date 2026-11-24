using UnityEngine;

public class MouseController : SceneManager<MouseController>
{
    [SerializeField]
    Texture2D defaultCursorTexture;

    [SerializeField]
    Texture2D arrowCursorTexture;

    [SerializeField]
    Texture2D handCursorTexture;

    [SerializeField]
    Texture2D textCursorTexture;

    void OnEnable()
    {
        Cursor.visible = true;
    }

    void OnDisable()
    {
        // Cursor.visible = false; cmt for testing purpose
    }

    void Start()
    {
        Cursor.SetCursor(defaultCursorTexture, Vector2.zero, CursorMode.Auto);
    }

    public void SetToMode(MouseMode mode)
    {
        switch (mode)
        {
            case MouseMode.Arrow:
                Cursor.SetCursor(arrowCursorTexture, Vector2.zero, CursorMode.Auto);
                break;
            case MouseMode.Hand:
                Cursor.SetCursor(handCursorTexture, Vector2.zero, CursorMode.Auto);
                break;
            case MouseMode.Text:
                Cursor.SetCursor(textCursorTexture, Vector2.zero, CursorMode.Auto);
                break;
            case MouseMode.Default:
                Cursor.SetCursor(defaultCursorTexture, Vector2.zero, CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(defaultCursorTexture, Vector2.zero, CursorMode.Auto);
                break;
        }
    }
}

public enum MouseMode
{
    Default,
    Arrow,
    Hand,
    Text,
}
