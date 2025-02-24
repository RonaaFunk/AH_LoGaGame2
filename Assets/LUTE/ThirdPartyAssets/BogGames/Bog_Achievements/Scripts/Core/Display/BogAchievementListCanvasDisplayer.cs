using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BogGames.Tools.Achievements
{
    /// <summary>
    /// Helper class that will fill the achievement list - ensure you attach this to a relevant canvas object.
    /// Typically the best way to do this is to attach the achievment items to a canvas object that has some form of layout group.
    /// Therefore the suggestion is to attach this script directly to an object that has the canvas layout component.
    /// </summary>
    public class BogAchievementListCanvasDisplayer : MonoBehaviour
    {
        [Tooltip("The achievement item prefab to use for displaying achievements.")]
        [SerializeField] protected BogAchievementFadeDisplayItem achievementItem;
        [Tooltip("The sound to play when this menu opens.")]
        [SerializeField] protected AudioClip menuOpenSound;
        [Tooltip("The sound to play when this menu closes.")]
        [SerializeField] protected AudioClip menuCloseSound;
        [Tooltip("How long to fade the menu in and out for.")]
        [SerializeField] protected float fadeDuration = 0.5f;

        protected List<BogAchievement> currentAchievements = new List<BogAchievement>();

        private CanvasGroup canvasGroup;
        private bool menuOpen = false;
        private List<BogAchievementFadeDisplayItem> achievements = new List<BogAchievementFadeDisplayItem>();

        private static List<BogAchievementListCanvasDisplayer> activeLists = new List<BogAchievementListCanvasDisplayer>();

        public static BogAchievementListCanvasDisplayer ActiveList;

        protected virtual void Awake()
        {
            canvasGroup = GetComponentInParent<CanvasGroup>();
            if (!activeLists.Contains(this))
                activeLists.Add(this);

            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            BogAchievementSignals.OnAchievementUnlocked += UnlockAchievement;
            BogAchievementSignals.OnAchievementProgress += UpdateAchievementProgress;
        }
        protected virtual void OnDestroy()
        {
            activeLists.Remove(this);

            BogAchievementSignals.OnAchievementUnlocked -= UnlockAchievement;
            BogAchievementSignals.OnAchievementProgress -= UpdateAchievementProgress;
        }

        protected virtual void Start()
        {
            // Populate the list here so we can display the achievements correctly.
            // No need to populate elsewhere as we need to call this method before calling any other related method.
            // Just ensure you set the achievements on the achievement manager somewhere.
            AddAchievements();
        }

        protected virtual void AddAchievements()
        {
            if (currentAchievements == null || currentAchievements.Count <= 0)
            {
                return;
            }

            foreach (var achievement in currentAchievements)
            {
                AddAchievement(achievement);
            }
        }

        /// <summary>
        /// Helper method to determine if the achievement item already exists in the list.
        /// </summary>
        /// <param name="achievementID"></param>
        /// <returns></returns>
        protected BogAchievementFadeDisplayItem AchievementItemExists(string achievementID)
        {
            // Check to see if an achievement already exists on this menu object
            foreach (var achievement in achievements)
            {
                if (achievement.AchievementID == achievementID)
                {
                    return achievement;
                }
            }
            return null;
        }

        private IEnumerator FadeCanvasGroupIEnum(CanvasGroup target, float duration, float targetAlpha, bool unscaled = true)
        {
            if (target == null)
            {
                target = canvasGroup;
            }

            if (target == null)
            {
                yield break;
            }

            float currentAlpha = target.alpha;

            float t = 0f;
            while (t < 1.0f)
            {
                if (target == null)
                    yield break;

                float newAlpha = Mathf.SmoothStep(currentAlpha, targetAlpha, t);
                target.alpha = newAlpha;

                if (unscaled)
                {
                    t += Time.unscaledDeltaTime / duration;
                }
                else
                {
                    t += Time.deltaTime / duration;
                }

                yield return null;
            }

            target.alpha = targetAlpha;
        }

        public virtual void FadeAchievementMenu()
        {

            if (menuOpen)
            {
                StartCoroutine(FadeCanvasGroupIEnum(canvasGroup, fadeDuration, 0f));
                PlayMenuSound(false);
                menuOpen = false;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }
            else
            {
                StartCoroutine(FadeCanvasGroupIEnum(canvasGroup, fadeDuration, 1f));
                PlayMenuSound(true);
                menuOpen = true;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }
        }

        protected virtual void PlayMenuSound(bool open)
        {
            // Typically you override this method to play a sound when the menu opens or closes.
            // This is often the case as there are too many methods to play sounds so we allow the user to override this method.
        }

        /// <summary>
        /// Adds an achievement to the list if one does not exist already.
        /// If no achievement item has been provided we look for this in the resources/prefabs folder.
        /// </summary>
        /// <param name="achievement"></param>
        public virtual BogAchievementFadeDisplayItem AddAchievement(BogAchievement achievement)
        {
            if (achievement == null)
                return null;

            if (AchievementItemExists(achievement.AchievementID) != null)
                return null;

            if (achievementItem == null)
                achievementItem = Resources.Load<BogAchievementFadeDisplayItem>("Prefabs/BogAchievementDisplayItem");
            if (achievementItem == null)
            {
                Debug.LogError("No achievement item prefab found in resources/prefabs folder.");
                return null;
            }

            var newAchievementItem = Instantiate(achievementItem);
            newAchievementItem.transform.SetParent(this.transform, false);
            newAchievementItem.SetAchievementInMenu(achievement); // ensure you set the ID here too
            achievements.Add(newAchievementItem);

            return newAchievementItem;
        }

        /// <summary>
        /// Updates an exisiting achievement in the list to the unlocked status.
        /// Often called using signals.
        /// If no achievement item is found then we create one before updating it.
        /// </summary>
        /// <param name="achievement"></param>
        public virtual void UnlockAchievement(BogAchievement achievement)
        {
            if (achievement == null)
                return;

            var achievementItem = AchievementItemExists(achievement.AchievementID);

            if (achievementItem == null)
            {
                achievementItem = AddAchievement(achievement);
            }
            if (achievementItem == null)
            {
                return;
            }

            achievementItem.UnlockAchievementInMenu(achievement);
        }

        /// <summary>
        /// Updates an exisiting achievmement's progress in the list.
        /// If no achievement exists then we create one before updating it.
        /// </summary>
        /// <param name="achievement"></param>
        public virtual void UpdateAchievementProgress(BogAchievement achievement)
        {
            if (achievement == null)
                return;

            var achievementItem = AchievementItemExists(achievement.AchievementID);

            if (achievementItem == null)
            {
                achievementItem = AddAchievement(achievement);
            }
            if (achievementItem == null)
            {
                return;
            }

            achievementItem.UpdateAchievementProgressInMenu(achievement);
        }

        /// <summary>
        /// Helper method that finds the active list in the scene or creates a new one if none exists.
        /// </summary>
        public static BogAchievementListCanvasDisplayer GetList()
        {
            if (ActiveList == null)
            {
                BogAchievementListCanvasDisplayer list = null;
                if (activeLists.Count > 0)
                {
                    list = activeLists[0];
                }
                if (list != null)
                    ActiveList = list;
                else
                {
                    GameObject listPrefab = Resources.Load<GameObject>("Prefabs/BogAchievementListCanvas");
                    if (listPrefab != null)
                    {
                        GameObject listObject = Instantiate(listPrefab) as GameObject;
                        listObject.name = "BogAchievementListCanvas";
                        list = listObject.GetComponent<BogAchievementListCanvasDisplayer>();
                        ActiveList = list;
                    }
                }
            }

            return ActiveList;
        }
    }
}
