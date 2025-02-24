using BogGames.Tools.Achievements;
using UnityEngine;

namespace LoGaCulture.LUTE
{
    /// <summary>
    /// An example of how LUTE or other libraries can use the BogAchievementRules to define their own rules and ensure the class is founded within the game and therefore works during runtime.
    /// </summary>
    public class LUTEAchievementRules : BogAchievementRules
    {
        /// <summary>
        /// Either unlocks an achievement with a given name or adds progress to it.
        /// </summary>
        /// <param name="achievmentName"></param>
        /// <param name="progress"></param>
        /// <param name="amount"></param>
        public void GenericAchievementCall(string achievmentName, bool progress, int amount = 0)
        {
            var achievementManager = LogaManager.Instance.BogAchievementsManager;
            if (achievementManager == null)
                return;

            if (progress)
                achievementManager.AddProgress(achievmentName, amount);
            else
                achievementManager.UnlockAchievement(achievmentName);
        }

        /// <summary>
        /// Generic event to unlock achievements via the manager.
        /// Often used with events such as buttons or other UI elements.
        /// </summary>
        /// <param name="achievementID"></param>
        public void GenericUnlock(string achievementID)
        {
            var achievementManager = LogaManager.Instance.BogAchievementsManager;
            if (achievementManager == null)
                return;

            achievementManager.UnlockAchievement(achievementID);
        }

        public override void PrintCurrentStatus()
        {
            if (Application.isPlaying)
            {
                var achievementManager = LogaManager.Instance.BogAchievementsManager;
                foreach (BogAchievement achievement in achievementManager.CurrentAchievements)
                {
                    string status = achievement.UnlockedStatus ? "unlocked" : "locked";
                    Debug.Log("[" + achievement.AchievementID + "] " + achievement.Title + ", status : " + status + ", progress : " + achievement.ProgressCurrent + "/" + achievement.ProgressTarget);
                }
            }
            else
            {
                Debug.LogWarning("BogAchievementRules Warning: Must be playing the game to get the achievement status");
            }
        }

        protected override void Awake()
        {
            base.Awake();
            var achievementManager = LogaManager.Instance.BogAchievementsManager;
            achievementManager.LoadAchievementList(achievementList);
        }
    }
}