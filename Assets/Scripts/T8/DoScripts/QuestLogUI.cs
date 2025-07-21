using UnityEngine;
using TMPro;

public class QuestLogUI : MonoBehaviour
{
    public TextMeshProUGUI quest1Text;
    public TextMeshProUGUI quest2Text;

    private void Start()
    {
        UpdateQuestTexts();
        QuestManager.OnQuestUpdated += UpdateQuestTexts;
    }

    private void OnDestroy()
    {
        QuestManager.OnQuestUpdated -= UpdateQuestTexts;
    }

    private void UpdateQuestTexts()
    {
        if (quest1Text != null)
        {
            bool moved = QuestManager.HasMovedOnce();
            quest1Text.text = $"Move Once - {(moved ? "Completed" : "In Progress")}";
        }

        if (quest2Text != null)
        {
            bool won = QuestManager.HasWonOnce();
            quest2Text.text = $"Win Once - {(won ? "Completed" : "In Progress")}";
        }
    }
}