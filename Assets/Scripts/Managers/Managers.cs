using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers _instance;

    public static Managers Instance
    {
        get
        {
            Init();
            return _instance;
        }
    }

    #region core

    private readonly ResourceManager _resource = new();
    private readonly SceneManagerEx _scene = new();
    private readonly UIManager _ui = new();
    private readonly PoolManager _pool = new();
    private readonly GameManager _game = new();


    public static ResourceManager Resource => Instance._resource;
    public static SceneManagerEx Scene => Instance._scene;
    public static UIManager UI => Instance._ui;
    public static PoolManager Pool => Instance._pool;
    public static GameManager Game => Instance._game;

    #endregion
    
    private void Awake()
    {
        Init();
    }

    private static void Init()
    {
        if (_instance == null)
        {
            var go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            _instance = go.GetComponent<Managers>();
        }

        _instance._pool.Init();
    }


    public static void Clear()
    {
    }


  
}