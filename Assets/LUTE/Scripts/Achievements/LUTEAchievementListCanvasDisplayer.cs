using BogGames.Tools.Achievements;

namespace LoGaCulture.LUTE
{
    /// <summary>
    /// Inherited class from BogAchievementListCanvasDisplayer that will fill the achievement list correctly and play menu sounds.
    /// </summary>
    public class LUTEAchievementListCanvasDisplayer : BogAchievementListCanvasDisplayer
    {
        protected override void Start()
        {
            currentAchievements = LogaManager.Instance.BogAchievementsManager.CurrentAchievements;
            base.Start();
        }

        protected override void PlayMenuSound(bool open)
        {
            var soundManager = LogaManager.Instance.SoundManager;

            if (!open)
            {
                if (soundManager != null && menuCloseSound != null)
                {
                    soundManager.PlaySound(menuCloseSound, 1.0f);
                }
            }
            else
            {
                if (soundManager != null && menuCloseSound != null)
                {
                    soundManager.PlaySound(menuOpenSound, 1.0f);
                }
            }
        }
    }
}
