using BogGames.Tools;
using BogGames.Tools.Achievements;
using LoGaCulture.LUTE;
using UnityEngine;


[OrderInfo("LUTEAchievements",
              "Progress Achievement",
              "Add to or set a new progress amount for a given achievement. Requires achievement list in scene.")]
[AddComponentMenu("")]
public class ProgressAchievement : Order
{
    [GenericListProperty("<Value>", typeof(BogAchievement))]
    [Tooltip("The achievement or quest in question.")]
    [SerializeField] protected BogAchievement achievement;
    [Tooltip("Whether to set the progress or add it by the specified value")]
    [SerializeField] protected bool setProgress;
    [Tooltip("The value to add to the progress")]
    [SerializeField] protected int progressValue;

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

        if (setProgress)
            achievementManager.SetProgress(achievement.AchievementID, progressValue);
        else
            achievementManager.AddProgress(achievement.AchievementID, progressValue);

        Continue();
    }

    public override string GetSummary()
    {
        if (achievement == null)
            return "Error: No achievement set!";

        return $"   Progressing {achievement.AchievementID} by {progressValue} (Set Progress: {setProgress})";
    }

    public override Color GetButtonColour()
    {
        return new Color32(235, 191, 217, 255);
    }
}