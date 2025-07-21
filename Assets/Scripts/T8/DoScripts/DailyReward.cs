using UnityEngine;
using System;

public static class DailyRewardManager
{
    private static DateTime? lastClaimedTime = null;
    private const string LAST_CLAIM_KEY = "LastDailyClaim";

    public static readonly int rewardFlowers = 90;
    public static readonly int rewardExp = 50;

    static DailyRewardManager()
    {
        // Load from PlayerPrefs when the game starts
        if (PlayerPrefs.HasKey(LAST_CLAIM_KEY))
        {
            string savedTime = PlayerPrefs.GetString(LAST_CLAIM_KEY);
            if (DateTime.TryParse(savedTime, out DateTime parsedTime))
            {
                lastClaimedTime = parsedTime;
            }
        }
    }

    public static bool CanClaim()
    {
        if (lastClaimedTime == null) return true;

        TimeSpan timeSinceLast = DateTime.Now - lastClaimedTime.Value;
        return timeSinceLast.TotalHours >= 24;
    }

    public static void Claim()
    {
        if (!CanClaim())
        {
            Debug.Log("[DailyReward] Already claimed today.");
            return;
        }

        QuestManager.GainExp(rewardExp);
        QuestManager.RegisterDebugFlowers(rewardFlowers);

        lastClaimedTime = DateTime.Now;
        PlayerPrefs.SetString(LAST_CLAIM_KEY, lastClaimedTime.Value.ToString());
        PlayerPrefs.Save();

        PersistentQuestUI.NotifyQuestComplete("Daily Reward Claimed!");
    }

    // For debug/testing purposes
    public static void DebugForceClaim()
    {
        lastClaimedTime = null;
        PlayerPrefs.DeleteKey(LAST_CLAIM_KEY);
        Claim();
    }
}