using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour, ISaveable
{
    public bool IsLoaded { get; set; }

    private void Awake()
    {
        IsLoaded = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CanLoad() && other.gameObject == GameManager.Instance.Player.gameObject)
        {
            SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            IsLoaded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (CanUnload() && other.gameObject == GameManager.Instance.Player.gameObject)
        {
            SceneManager.UnloadSceneAsync(gameObject.name);
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

    public object CaptureState()
    {
        return new SceneSaveData(this);
    }

    public void RestoreState(object state)
    {
        var data = (SceneSaveData)state;

        IsLoaded = data.IsLoaded;
    }

}
