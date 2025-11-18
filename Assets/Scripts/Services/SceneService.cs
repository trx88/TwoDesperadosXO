using System;
using UnityEngine.SceneManagement;

namespace Services
{
    public class SceneService
    {
        private bool _isLoading;

        public event Action<string> OnSceneChanging;
        public event Action<string> OnSceneLoaded;

        public void LoadScene(string sceneName)
        {
            if (_isLoading) return;

            _isLoading = true;

            OnSceneChanging?.Invoke(sceneName);

            // Use Unity's LoadSceneAsync
            var operation = SceneManager.LoadSceneAsync(sceneName);

            if (operation != null)
            {
                operation.completed += op =>
                {
                    _isLoading = false;
                    OnSceneLoaded?.Invoke(sceneName);
                };   
            }
        }

        public void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }

    public enum SceneIndices
    {
        PlayScene = 0,
        GameScene = 1
    }
}
