using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infractructure
{
    public class SceneLoader : ISceneLoader
    {
        private ICoroutineRunner _coroutineRunner;
        
        public void Init(ICoroutineRunner coroutineRunner) =>
            _coroutineRunner = coroutineRunner;

        public void LoadScene(string name, Action onLoaded = null) =>
            _coroutineRunner.StartCoroutine(Load(name, onLoaded));
        
        private IEnumerator Load(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}