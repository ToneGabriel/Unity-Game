using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenState : GameManagerState
{
    private LoadingScreenData _loadingScreenData;
    private GameManagerState _nextState;
    private int _loadingBarLength;                                  // The number of processes included in loading bar
    private int _loadingProcessCounter;                             // The counter of these processes

    public LoadingScreenState(GameManager gameManager, GameManagerFiniteStateMachine stateMachine, LoadingScreenData loadingScreenData)
        :base(gameManager, stateMachine)
    {
        _loadingScreenData = loadingScreenData;
        _loadingBarLength = _loadingScreenData.SceneLoaders.Length * 2;    // Loading bar includes loading/unloading of scenes, therefore double the number of scenes
    }

    public override void Enter()
    {
        base.Enter();

        ObjectPoolManager.Instance.ClearScene();

        _gameManager.IsLoadingData = true;
        _loadingScreenData.LoaderCanvas.SetActive(true);
        _loadingScreenData.ProgressBar.fillAmount = 0f;
        _loadingProcessCounter = 0;
        _gameManager.StartCoroutine(LoadingScreen());
    }

    public override void LogicUpdate() => base.LogicUpdate();

    public override void Exit()
    {
        base.Exit();

        _loadingScreenData.LoaderCanvas.SetActive(false);
        _gameManager.IsLoadingData = false;
    }

    public void SetLoadData(Action loadData)
    {
        _loadData = loadData;
    }

    public void SetNextState(GameManagerState nextState)
    {
        _nextState = nextState;
    }

    #region Load Coroutines
    private IEnumerator LoadingScreen()
    {
        yield return _gameManager.StartCoroutine(LoadScenes(false));        // Unload ALL scenes
        _loadData?.Invoke();
        yield return _gameManager.StartCoroutine(LoadScenes(true));         // Load needed scenes
        _stateMachine.ChangeState(_nextState);
    }

    private IEnumerator LoadScenes(bool activateScenes)
    {
        AsyncOperation sceneToProcess;

        foreach (var sceneLoader in _loadingScreenData.SceneLoaders)
        {
            if (sceneLoader.IsLoaded)
            {
                if (activateScenes)
                {
                    sceneToProcess = SceneManager.LoadSceneAsync(sceneLoader.gameObject.name, LoadSceneMode.Additive);
                    yield return _gameManager.StartCoroutine(LoadingProcess(sceneToProcess));
                }
                else
                {
                    sceneToProcess = SceneManager.UnloadSceneAsync(sceneLoader.gameObject.name);
                    yield return _gameManager.StartCoroutine(LoadingProcess(sceneToProcess));
                    sceneLoader.IsLoaded = false;
                }
            }
            else
                yield return _gameManager.StartCoroutine(LoadingProcess(null));
        }
    }

    private IEnumerator LoadingProcess(AsyncOperation operation)
    {
        float progressCheckpoint = _loadingScreenData.ProgressBar.fillAmount;

        if (operation != null)
            do
            {
                yield return null;
                _loadingScreenData.ProgressBar.fillAmount = progressCheckpoint + operation.progress / _loadingBarLength;

            } while (!operation.isDone);
        else
            _loadingScreenData.ProgressBar.fillAmount = (_loadingProcessCounter + 1f) / _loadingBarLength;

        _loadingProcessCounter++;
        yield return new WaitForSeconds(0.1f);
    }
    #endregion
}
