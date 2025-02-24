using UnityEngine;

namespace BogGames.Tools.Achievements
{
    /// <summary>
    /// Base achievement rules to be extended for specific game implementations.
    /// Having one of these in a game scene with the corresponding achievement list should ensure the achievement system operates correctly.
    /// </summary>
    public abstract class BogAchievementRules : MonoBehaviour
    {
        [SerializeField] protected BogAchievementList achievementList;
        [BogInspectorButton("PrintCurrentStatus")]
        [SerializeField] protected bool printCurrentStatusBtn;

        public BogAchievementList AchievementList { get { return achievementList; } }

        public virtual void PrintCurrentStatus()
        {
            // Find your achievement manager however you need to and print the status to your desire here.
        }

        /// <summary>
        /// On Awake, we load the list so that the serialisation system can populate the current list.
        /// </summary>
        protected virtual void Awake()
        {
            if (achievementList == null)
                return;

            // Ideally load your achievement list here. 
            // This can be done from inherited class.
        }
    }
}
