namespace Applications
{
    internal static class SceneLoader
    {
        public static void LoadGameScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene($"Game");
        }
    }
}