using UnityEditor;
using UnityEngine;

namespace BogGames.Tools.Achievements
{
    /// <summary>
    /// Custom inspector for the BogAchievementList scriptable object.
    /// Essentially draws the list and shows a button to reset the list to default values.
    /// </summary>
    [CustomEditor(typeof(BogAchievementList), true)]
    public class BogAchievementListEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            BogAchievementList achievementList = (BogAchievementList)target;
            if (GUILayout.Button("Reset Achievements"))
            {
                achievementList.ResetAchievements();
            }
            EditorUtility.SetDirty(achievementList);
        }
    }
}