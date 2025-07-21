using UnityEngine;

public class SkinBootstrap : MonoBehaviour
{
    private static bool alreadySpawned = false;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Init()
    {
        if (alreadySpawned) return;

        GameObject bootstrap = new GameObject("SkinBootstrap");
        bootstrap.AddComponent<SkinBootstrap>();
        DontDestroyOnLoad(bootstrap);

        alreadySpawned = true;
    }

    private void Start()
    {
        ShopManager.ApplyEquippedSkin();
    }
}