using System;

namespace Infractructure
{
    public interface ISceneLoader
    {
        void LoadScene(string name, Action onLoaded = null);
        void Init(ICoroutineRunner coroutineRunner);
    }
}