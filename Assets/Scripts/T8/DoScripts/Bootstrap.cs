using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPersistentBootstrap : MonoBehaviour
{
    public GameObject uiPrefab;

    private static GameObject spawnedUI;

    private void Awake()
    {
        if (spawnedUI == null && uiPrefab != null)
        {
            spawnedUI = Instantiate(uiPrefab);
            DontDestroyOnLoad(spawnedUI);
        }

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (spawnedUI != null)
        {
            Canvas canvas = spawnedUI.GetComponentInChildren<Canvas>(true);
            if (canvas != null)
                canvas.gameObject.SetActive(false); // hide it initially
            canvas.gameObject.SetActive(true); // ensure it's visible again

            // Optional: reassign text/UI if it got unlinked in scene
            var flowerUI = spawnedUI.GetComponentInChildren<FlowerDisplayUI>();
            if (flowerUI != null)
                flowerUI.SendMessage("Start", SendMessageOptions.DontRequireReceiver);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}