using System.Collections;
using UnityEngine;

namespace BogGames.Tools.Achievements
{
    /// <summary>
    /// A class used to display the achievements on screen.
    /// Typically the BogAchievementFadeDisplayItem will parent to this so the suggestion is to use some form of layout group to avoid overcrowding.
    /// One can override this class to add additional animations or sounds.
    /// </summary>
    public class BogAchievementFadeDisplayer : MonoBehaviour
    {
        [Header("Display Settings")]
        [Tooltip("The prefab to use to display achievements.")]
        [SerializeField] protected BogAchievementFadeDisplayItem achievementDisplayPrefab;
        [Tooltip("The duration the achievement will remain on screen for when unlocked.")]
        [SerializeField] protected float achievementDisplayDuration = 5f;
        [Tooltip("The fade in/out speed.")]
        [SerializeField] protected float achievementFadeDuration = 0.2f;

        protected WaitForSeconds achievementFadeWFS;

        /// <summary>
        /// Instantiates an achievement display prefab and shows it for the specified duration
        /// One may wish to additional animations here such as floating or scaling.
        /// </summary>
        /// <returns>The achievement.</returns>
        /// <param name="achievement">Achievement.</param>
        public virtual IEnumerator DisplayAchievementIEnum(BogAchievement achievement)
        {
            if ((this.transform == null) || (achievementDisplayPrefab == null))
            {
                yield break;
            }

            // Instantiate the achievement display prefab, and add it to the group that will automatically handle its position
            BogAchievementFadeDisplayItem instance = (BogAchievementFadeDisplayItem)Instantiate(achievementDisplayPrefab);
            instance.transform.SetParent(this.transform, false);

            // Fill the achievement item with relevant information
            instance.Title.text = achievement.Title;
            instance.Description.text = achievement.Description;
            if (achievement.AchievementType == AchievementType.Progressive)
            {
                // Set the progress bar fill amount here based on achievement progress
                instance.ProgressBarDisplay.value = (float)achievement.ProgressCurrent / (float)achievement.ProgressTarget;
                instance.ProgressBarDisplay.gameObject.SetActive(true);
            }
            else
            {
                instance.ProgressBarDisplay.gameObject.SetActive(false);
            }

            if (achievement.UnlockedStatus == true)
            {
                instance.Icon.sprite = achievement.UnlockedImage;
                PlaySound(achievement, false);

            }
            else
            {
                PlaySound(achievement, true);
            }

            instance.FadeCanvasGroup(null, achievementFadeDuration, 1.0f);
            yield return achievementFadeWFS;
            instance.FadeCanvasGroup(null, achievementFadeDuration, 0.0f);
        }

        protected virtual void PlaySound(BogAchievement achievement, bool progress)
        {
            // As there are various ways to play sounds in projects this method is left to be overridden.
        }

        protected virtual void OnEnable()
        {
            // Listen out for progress or unlock signals
            // One could opt to have individual methods for progress and unlock if desired.
            BogAchievementSignals.OnAchievementUnlocked += DisplayAchievement;
            BogAchievementSignals.OnAchievementProgress += DisplayAchievement;
            achievementFadeWFS = new WaitForSeconds(achievementFadeDuration + achievementDisplayDuration);
        }

        protected virtual void OnDisable()
        {
            // Stop listening for events
            BogAchievementSignals.OnAchievementUnlocked -= DisplayAchievement;
            BogAchievementSignals.OnAchievementProgress -= DisplayAchievement;
        }

        protected virtual void DisplayAchievement(BogAchievement achievement)
        {
            StartCoroutine(DisplayAchievementIEnum(achievement));
        }
    }
}
