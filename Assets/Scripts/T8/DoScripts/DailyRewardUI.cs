using UnityEngine;
using UnityEngine.UI;

public class DailyRewardUI : MonoBehaviour
{
    public Button claimButton;
    public Button debugButton;

    private void Start()
    {
        if (claimButton != null)
            claimButton.onClick.AddListener(OnClaimClicked);

        if (debugButton != null)
            debugButton.onClick.AddListener(OnDebugClicked);

        UpdateButtonState();
    }

    private void OnClaimClicked()
    {
        DailyRewardManager.Claim();
        UpdateButtonState();
    }

    private void OnDebugClicked()
    {
        DailyRewardManager.DebugForceClaim();
        UpdateButtonState();
    }

    private void UpdateButtonState()
    {
        if (claimButton != null)
            claimButton.interactable = DailyRewardManager.CanClaim();
    }
}