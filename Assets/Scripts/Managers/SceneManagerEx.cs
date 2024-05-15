using System;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class SceneManagerEx
{
    public BaseScene CurrentScene => Object.FindObjectOfType<BaseScene>();

    public void LoadScene(Define.SceneType type)
    {
        CurrentScene.Clear();
        SceneManager.LoadScene(GetSceneName(type));
    }

    private string GetSceneName(Define.SceneType type)
    {
        var name = Enum.GetName(typeof(Define.SceneType), type);
        return name;
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}