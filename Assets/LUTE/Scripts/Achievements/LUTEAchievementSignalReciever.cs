using BogGames.Tools.Achievements;
using UnityEngine;

namespace LoGaCulture.LUTE
{
    /// <summary>
    /// Listens out for Bog's achievement signals and processes them.
    /// For the most part, Bog's achievement system handles the achievements but this class is used to save the achievements to the save manager.
    /// </summary>
    public class LUTEAchievementSignalReciever : MonoBehaviour
    {
        public string SavePointKey
        {
            get
            {
                return "LUTEAchievementsList";
            }
        }

        public string SavePointDescription
        {
            get
            {
                return "LUTEAchievementsList" + " - " + LogaManager.Instance.BogAchievementsManager.CurrentAchievements.Count + System.DateTime.UtcNow.ToString("HH:mm dd MMMM, yyyy");
            }
        }

        protected virtual void Awake()
        {
            BogAchievementSignals.OnAchievementsReset += OnAchievementsReset;
            BogAchievementSignals.OnAchievementUnlocked += OnAchievementUnlocked;
            BogAchievementSignals.OnAchievementProgress += OnAchievementProgressed;
        }

        protected virtual void OnDisable()
        {
            BogAchievementSignals.OnAchievementsReset -= OnAchievementsReset;
            BogAchievementSignals.OnAchievementUnlocked -= OnAchievementUnlocked;
            BogAchievementSignals.OnAchievementProgress -= OnAchievementProgressed;
        }

        protected virtual void OnAchievementsReset()
        {
            LogaManager.Instance.SaveManager.AddSavePoint(SavePointKey, SavePointDescription, false, SaveManager.SaveProfile.BogAchievementData);
        }

        protected virtual void OnAchievementUnlocked(BogAchievement achievement)
        {
            var achievementManager = LogaManager.Instance.BogAchievementsManager;
            LogaManager.Instance.SaveManager.AddSavePoint(SavePointKey, SavePointDescription, false, SaveManager.SaveProfile.BogAchievementData);
        }

        protected virtual void OnAchievementProgressed(BogAchievement achievement)
        {
            // As we may have updated the achievement progress we should save this here unless our progress is still 0
            var achievementManager = LogaManager.Instance.BogAchievementsManager;
            LogaManager.Instance.SaveManager.AddSavePoint(SavePointKey, SavePointDescription, false, SaveManager.SaveProfile.BogAchievementData);
        }
    }
}
