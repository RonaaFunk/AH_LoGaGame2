using BogGames.Tools.Achievements;

namespace LoGaCulture.LUTE
{
    /// <summary>
    /// Overrides the achievement fade displayer to play LUTE sounds.
    /// </summary>
    public class LUTEAchievementFadeDisplayer : BogAchievementFadeDisplayer
    {
        protected override void PlaySound(BogAchievement achievement, bool progress)
        {
            if (!progress)
            {
                if (achievement.UnlockedSound != null)
                {
                    // Play the sound if set
                    var soundManager = LogaManager.Instance.SoundManager;
                    if (soundManager)
                    {
                        soundManager.PlaySound(achievement.UnlockedSound, 1.0f);
                    }
                }
            }
            else if (achievement.ProgressSound != null)
            {
                // Play the sound if set
                var soundManager = LogaManager.Instance.SoundManager;
                if (soundManager)
                {
                    soundManager.PlaySound(achievement.ProgressSound, 1.0f);
                }
            }
        }
    }
}