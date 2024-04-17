using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour, ISaveable
{
    public SceneNames SceneName;
    public bool IsLoaded { get; set; }

    private void Awake()
    {
        IsLoaded = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CanLoad() && ColliderIsPlayer(other))
        {
            SceneManager.LoadSceneAsync((int)SceneName, LoadSceneMode.Additive);
            IsLoaded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (CanUnload() && ColliderIsPlayer(other))
        {
            SceneManager.UnloadSceneAsync((int)SceneName);
            IsLoaded = false;
        }
    }

    private bool CanLoad()
    {
        return !GameManager.Instance.IsLoadingData && !GameManager.Instance.Player.IsDead && !IsLoaded;
    }

    private bool CanUnload()
    {
        return !GameManager.Instance.IsLoadingData && !GameManager.Instance.Player.IsDead;
    }

    private bool ColliderIsPlayer(Collider2D other)
    {
        return other.gameObject == GameManager.Instance.Player.gameObject;
    }

    public object CaptureState()
    {
        return new SceneSaveData(this);
    }

    public void RestoreState(ref object state)
    {
        var data = (SceneSaveData)state;

        IsLoaded = data.IsLoaded;
    }

}
