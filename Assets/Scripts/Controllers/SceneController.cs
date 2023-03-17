using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    [SerializeField] private Camera _mainCamera;
    private AsyncOperation _nextScene;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(_mainCamera);
    }

    private void Start()
    {
        LoadNextSceneAsync();
    }

    public void LoadNextSceneAsync()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 4)
        {
            return;
        }

        _nextScene = SceneManager.LoadSceneAsync(currentSceneIndex + 1, LoadSceneMode.Single);
        _nextScene.allowSceneActivation = false;
    }

    public IEnumerator ActivateNextScene()
    {
        if (_nextScene.progress >= 0.9f)
        {
            yield return new WaitForSeconds(0.25f);
            _nextScene.allowSceneActivation = true;
        }
    }










}//class
