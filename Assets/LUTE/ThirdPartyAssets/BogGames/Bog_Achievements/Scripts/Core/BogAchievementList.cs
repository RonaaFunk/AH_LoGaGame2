using System.Collections.Generic;
using UnityEngine;

namespace BogGames.Tools.Achievements
{
    /// <summary>
    /// A scriptable object containing a list of achievements. You need to create one and store it in a Resources folder for the achievement system to work.
    /// For LUTE users, you should be able to set this list up via the FlowEngine object.
    /// </summary>
    [CreateAssetMenu(fileName = "AchievementList", menuName = "Bog/Achievement List")]
    public class BogAchievementList : ScriptableObject
    {
        [SerializeField] protected string achievementsListID = "BogAchievementsList";

        [SerializeField] protected List<BogAchievement> achievements;

        public string AchievementsListID { get { return achievementsListID; } }
        public List<BogAchievement> Achievements { get { return achievements; } }

        /// <summary>
        /// Resets of all the achievements in this list (all achievements will be locked again, and their progress lost).
        /// </summary>
        public virtual void ResetAchievements()
        {
        }
    }
}
