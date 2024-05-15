using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public abstract Define.SceneType _sceneType { get; }

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        var obj = FindObjectOfType(typeof(EventSystem));

        if (obj == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
    }

    public abstract void Clear();
}