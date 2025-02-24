using BogGames.Tools;
using BogGames.Tools.Achievements;
using LoGaCulture.LUTE;
using UnityEngine;

[OrderInfo("LUTEAchievements",
              "Unlock Achievement",
              "Unlocks an achievement based upon the list found in the scene.")]
[AddComponentMenu("")]
public class UnlockAchievement : Order
{
    [GenericListProperty("<Value>", typeof(BogAchievement))]
    [Tooltip("The achievement or quest in question.")]
    [SerializeField] protected BogAchievement achievement;

    public override void OnEnter()
    {
        if (achievement == null)
        {
            Debug.LogError("Achievement or achievement rules is missing!");
            return;
        }

        var achievementManager = LogaManager.Instance.BogAchievementsManager;
        if (achievementManager == null)
            return;

        achievementManager.UnlockAchievement(achievement.AchievementID);

        Continue();
    }

    public override string GetSummary()
    {
        if (achievement == null)
            return "Error: No achievement set!";

        return $"   Unlocking {achievement.AchievementID}";
    }

    public override Color GetButtonColour()
    {
        return new Color32(235, 191, 217, 255);
    }
}