using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            var name = path;
            var index = name.LastIndexOf('/');
            if (index >= 0)
                name = name.Substring(index + 1);

            var go = Managers.Pool.GetOriginal(name);
            if (go != null)
                return go as T;
        }

        return Resources.Load<T>(path);
    }

    public Sprite LoadSprite(string name)
    {
        var path = $"Sprites/{name}";

        var original = Resources.Load<Sprite>(path);
        if (original == null)
        {
            Debug.Log($"Faild to sprite : {path}");
            original = Resources.Load<Sprite>("Sprites/NullErrorImg/Error");
        }

        return original;
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        var original = Load<GameObject>($"Prefabs/{path}");
        if (original == null)
        {
            Debug.Log($"Faild to load prefab : {path}");
            return null;
        }

        if (original.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(original, parent).gameObject;


        var go = Object.Instantiate(original, parent);
        go.name = original.name;

        return go;
    }

    public GameObject Instantiate(string path, Vector3 position, Transform parent = null)
    {
        var original = Load<GameObject>($"Prefabs/{path}");
        if (original == null)
        {
            Debug.Log($"Faild to load prefab : {path}");
            return null;
        }

        GameObject go = null;
        if (original.GetComponent<Poolable>() != null)
        {
            go = Managers.Pool.Pop(original, parent).gameObject;
            go.transform.position = position;
            return go;
        }


        go = Object.Instantiate(original, position, Quaternion.identity, parent);
        go.name = original.name;

        return go;
    }

    public GameObject Instantiate(GameObject original, Vector3 position, Transform parent = null)
    {
        GameObject go = null;
        if (original.GetComponent<Poolable>() != null)
        {
            go = Managers.Pool.Pop(original, parent).gameObject;
            go.transform.position = position;
            return go;
        }


        go = Object.Instantiate(original, position, Quaternion.identity, parent);
        go.name = original.name;

        return go;
    }


    public void Destroy(GameObject obj, float time = 0)
    {
        if (obj == null) return;


        var poolable = obj.GetComponent<Poolable>();
        if (poolable != null)
        {
            Managers.Pool.Push(poolable, time);
            return;
        }

        Object.Destroy(obj, time);
    }
}