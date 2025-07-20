using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public float timeRemaining = 60f;
    public TextMeshProUGUI timerText;
    public GameObject loseCanvas; // Canvas hiện lên khi thua

    private bool isTimerRunning = true;

    void Update()
    {
        if (isTimerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                timeRemaining = 0;
                isTimerRunning = false;
                TimeUp();
            }
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimeUp()
    {
        Debug.Log("Hết giờ!");
        if (loseCanvas != null)
            loseCanvas.SetActive(true); // Hiện canvas thua
    }

    public void RestartTimer(float newTime)
    {
        timeRemaining = newTime;
        isTimerRunning = false;
    }
    public void StopTimer()
    {
        isTimerRunning = false;
    }

}
