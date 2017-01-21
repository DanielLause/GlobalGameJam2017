using UnityEngine;

public class UnitySingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (instance == null)
                    Debug.LogError(string.Format("There needs to be one active {0} component on a GameObject in your scene", typeof(T).Name));
            }
            return instance;
        }
    }

    public static bool HasInstance
    {
        get { return instance != null; }
    }
}