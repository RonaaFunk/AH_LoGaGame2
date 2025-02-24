using BogGames.Tools.Achievements;
using UnityEngine;

[OrderInfo("LUTEAchievements",
              "Show Achievement List",
              "Shows the achievement list (toggles on if off and vice versa).")]
[AddComponentMenu("")]
public class ShowAchievementList : Order
{
    [Tooltip("The set achievement list to toggle - this will be found or created in the scene if not provided")]
    [SerializeField] protected BogAchievementListCanvasDisplayer setList;

    public override void OnEnter()
    {
        if (setList != null)
        {
            BogAchievementListCanvasDisplayer.ActiveList = setList;
        }

        var list = BogAchievementListCanvasDisplayer.GetList();
        if (list == null)
        {
            Continue();
            return;
        }

        list.FadeAchievementMenu();

        Continue();
    }

    public override string GetSummary()
    {
        return "Shows the achievement list (toggles on if off and vice versa).";
    }
}