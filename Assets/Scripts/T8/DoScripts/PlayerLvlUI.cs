using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerLevelUI : MonoBehaviour
{
    public static PlayerLevelUI Instance;

    [Header("UI")]
    public TextMeshProUGUI levelText;

    public Slider expSlider;

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
        QuestManager.OnExpChanged += UpdateUI;
        UpdateUI(QuestManager.GetExp(), QuestManager.GetLevel(), QuestManager.GetExpToNext());
    }

    private void OnDestroy()
    {
        QuestManager.OnExpChanged -= UpdateUI;
    }

    private void UpdateUI(int exp, int level, int expToNext)
    {
        if (levelText != null)
            levelText.text = $"Level {level}";

        if (expSlider != null)
        {
            expSlider.maxValue = expToNext;
            expSlider.value = exp;
        }
    }
}