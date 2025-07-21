using UnityEngine;
using System;

public static class QuestManager
{
    public enum QuestType
    { WinOnce, MoveBlock }

    private static bool winQuestComplete = false;
    private static bool moveBlockQuestComplete = false;

    private static int flowers = 0;
    private static int exp = 0;
    private static int level = 1;
    private static int expToNext = 100;

    public static Action<int> OnFlowerChanged;
    public static Action<int, int, int> OnExpChanged;
    public static Action OnQuestUpdated;

    static QuestManager()
    {
        LoadProgress();
    }

    public static void RegisterWin()
    {
        if (!winQuestComplete)
        {
            winQuestComplete = true;
            AwardFlowers(100);
            GainExp(120);
            PersistentQuestUI.NotifyQuestComplete("Quest complete, you won a game.");
            OnQuestUpdated?.Invoke();
        }
    }

    public static void RegisterBlockMove()
    {
        if (!moveBlockQuestComplete)
        {
            moveBlockQuestComplete = true;
            AwardFlowers(100);
            GainExp(80);
            PersistentQuestUI.NotifyQuestComplete("Quest complete, you moved a block!");
            OnQuestUpdated?.Invoke();
        }
    }

    public static bool IsQuestComplete(QuestType type) => type switch
    {
        QuestType.WinOnce => winQuestComplete,
        QuestType.MoveBlock => moveBlockQuestComplete,
        _ => false
    };

    public static bool HasWonOnce() => winQuestComplete;

    public static bool HasMovedOnce() => moveBlockQuestComplete;

    public static void RegisterDebugFlowers(int amount)
    {
        flowers += amount;
        SaveProgress();
        OnFlowerChanged?.Invoke(flowers);
    }

    private static void AwardFlowers(int amount)
    {
        flowers += amount;
        SaveProgress();
        OnFlowerChanged?.Invoke(flowers);
    }

    public static void GainExp(int amount)
    {
        exp += amount;
        CheckLevelUp();
        SaveProgress();
        OnExpChanged?.Invoke(exp, level, expToNext);
    }

    public static bool SpendFlowers(int amount)
    {
        if (flowers >= amount)
        {
            flowers -= amount;
            SaveProgress();
            OnFlowerChanged?.Invoke(flowers);
            return true;
        }
        return false;
    }

    private static void CheckLevelUp()
    {
        while (exp >= expToNext)
        {
            exp -= expToNext;
            level++;
            expToNext = Mathf.RoundToInt(expToNext * 1.2f);
            PersistentQuestUI.NotifyQuestComplete($"Level Up! You are now level {level}!");
            AwardFlowers(50);
        }
    }

    public static int GetFlowers() => flowers;

    public static int GetLevel() => level;

    public static int GetExp() => exp;

    public static int GetExpToNext() => expToNext;

    private static void SaveProgress()
    {
        PlayerPrefs.SetInt("Flowers", flowers);
        PlayerPrefs.SetInt("Exp", exp);
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetInt("ExpToNext", expToNext);
        PlayerPrefs.Save();
    }

    private static void LoadProgress()
    {
        flowers = PlayerPrefs.GetInt("Flowers", 0);
        exp = PlayerPrefs.GetInt("Exp", 0);
        level = PlayerPrefs.GetInt("Level", 1);
        expToNext = PlayerPrefs.GetInt("ExpToNext", 100);
    }
}