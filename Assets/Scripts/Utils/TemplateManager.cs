using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentManager<T> : MonoBehaviour
    where T : PersistentManager<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<T>();
                if (instance == null)
                {
                    Debug.LogError(
                        $"No instance of {typeof(T)} found in scene {SceneManager.GetActiveScene()}!"
                    );
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = (T)this;
        DontDestroyOnLoad(gameObject);
    }
}

public class SceneManager<T> : MonoBehaviour
    where T : SceneManager<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<T>();
                if (instance == null)
                {
                    Debug.LogError(
                        $"No instance of {typeof(T)} found in scene {SceneManager.GetActiveScene()}!"
                    );
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = (T)this;
    }
    protected virtual void OnDestroy()
    {
        if (Instance == this)
        {
            instance = null;
        }
    }
}
