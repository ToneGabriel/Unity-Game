using System;

[Serializable]
public struct SceneSaveData
{
    public bool IsLoaded;

    public SceneSaveData(SceneLoader loader)
    {
        IsLoaded = loader.IsLoaded;
    }
}
