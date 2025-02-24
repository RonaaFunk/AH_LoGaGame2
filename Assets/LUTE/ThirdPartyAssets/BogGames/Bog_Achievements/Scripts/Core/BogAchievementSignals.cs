namespace BogGames.Tools.Achievements
{
    /// <summary>
    /// Class used to send signals when achievements are unlocked or progress has been made.
    /// </summary>
    public class BogAchievementSignals
    {
        /// <summary>
        /// Achievement Unlocked signal; sent when an achievement is unlocked.
        /// </summary>
        public static event AchievementUnlockedHandler OnAchievementUnlocked;
        public delegate void AchievementUnlockedHandler(BogAchievement achievement);
        public static void DoAchievementUnlocked(BogAchievement achievement) { OnAchievementUnlocked?.Invoke(achievement); }

        /// <summary>
        /// Achievement progress made; sent when an achievement has made progress.
        /// </summary>
        public static event AchievementProgressHandler OnAchievementProgress;
        public delegate void AchievementProgressHandler(BogAchievement achievement);
        public static void DoAchievementProgress(BogAchievement achievement) { OnAchievementProgress?.Invoke(achievement); }

        /// <summary>
        /// Achievements Reset signal; sent when all achievements are reset.
        /// </summary>
        public static event AchievementsResetHandler OnAchievementsReset;
        public delegate void AchievementsResetHandler();
        public static void DoAchievementsReset() { OnAchievementsReset?.Invoke(); }
    }
}
