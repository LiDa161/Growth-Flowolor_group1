using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadShopButton : MonoBehaviour
{
    public string shopSceneName = "Shop"; // Change this to your actual shop scene name

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(LoadShop);
    }

    private void LoadShop()
    {
        SceneManager.LoadScene(shopSceneName);
    }
}