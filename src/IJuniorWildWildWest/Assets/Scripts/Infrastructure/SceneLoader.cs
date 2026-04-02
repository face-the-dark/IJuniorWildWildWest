using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader
    {
        public async UniTask Load(string sceneName)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
                return;

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(sceneName);

            await waitNextScene.ToUniTask();
        }
    }
}