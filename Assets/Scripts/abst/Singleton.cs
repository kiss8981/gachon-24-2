using UnityEngine;

public class Singleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance != null)
                return _instance;

            _instance = FindObjectOfType<T>();

            if (_instance == null)
            {
                GameObject singletonObj = new GameObject(typeof(T).Name);
                _instance = singletonObj.AddComponent<T>();
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 파괴되지 않음
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스 제거
        }
    }
}
