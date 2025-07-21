using UnityEngine;

public class Win : MonoBehaviour
{
    public GameObject winCanvas; // Giao diện Win sẽ bật khi thắng

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            QuestManager.RegisterWin(); //when player wins, register the win in QuestManager

            if (QuestManager.IsQuestComplete(QuestManager.QuestType.WinOnce))
            {
                Debug.Log("Player has completed the quest!");
            }

            if (winCanvas != null)
            {
                winCanvas.SetActive(true);
            }
            // Dừng thời gian khi thắng
            TimerManager timer = FindObjectOfType<TimerManager>();
            if (timer != null)
            {
                timer.StopTimer();
            }
        }
    }
}