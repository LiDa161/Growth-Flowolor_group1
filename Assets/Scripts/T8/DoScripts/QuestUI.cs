using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PersistentQuestUI : MonoBehaviour
{
    public static PersistentQuestUI Instance;

    [Header("Quest UI")]
    public GameObject questCompletePanel;

    public TextMeshProUGUI questText;

    [Header("Effects")]
    public AudioSource audioSource;

    public AudioClip questSound;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        canvasGroup = questCompletePanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = questCompletePanel.AddComponent<CanvasGroup>();
        }

        questCompletePanel.SetActive(false);
    }

    public static void NotifyQuestComplete(string message)
    {
        if (Instance != null)
        {
            Instance.ShowQuestMessage(message);
        }
    }

    private void ShowQuestMessage(string message)
    {
        questText.text = message;
        questCompletePanel.SetActive(true);
        canvasGroup.alpha = 1f;

        if (audioSource && questSound)
            audioSource.PlayOneShot(questSound);

        // Fade out after 3 seconds
        DOVirtual.DelayedCall(3f, () =>
        {
            canvasGroup.DOFade(0f, 1f).OnComplete(() =>
            {
                questCompletePanel.SetActive(false);
            });
        });
    }
}