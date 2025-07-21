using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestLogUI : MonoBehaviour
{
    public TextMeshProUGUI winQuestText;
    public TextMeshProUGUI moveBlockQuestText;

    private void Start()
    {
        RefreshQuests();
        QuestManager.OnQuestUpdated += RefreshQuests;
    }

    private void OnDestroy()
    {
        QuestManager.OnQuestUpdated -= RefreshQuests;
    }

    private void RefreshQuests()
    {
        winQuestText.text = QuestManager.IsQuestComplete(QuestManager.QuestType.WinOnce)
            ? "Completed: Win 1 game"
            : "Unfinished: Win 1 game";

        moveBlockQuestText.text = QuestManager.IsQuestComplete(QuestManager.QuestType.MoveBlock)
            ? "Completed: Move 1 block"
            : "Unfinished: Move 1 block";
    }
}