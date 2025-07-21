using UnityEngine;
using TMPro;

public class FlowerDisplayUI : MonoBehaviour
{
    public static FlowerDisplayUI Instance;

    [Header("UI")]
    public TextMeshProUGUI flowerText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateFlowerUI(QuestManager.GetFlowers());
        QuestManager.OnFlowerChanged += UpdateFlowerUI;
    }

    private void OnDestroy()
    {
        QuestManager.OnFlowerChanged -= UpdateFlowerUI;
    }

    private void UpdateFlowerUI(int amount)
    {
        if (flowerText != null)
        {
            flowerText.text = $"Flowers: {amount}";
        }
    }
}