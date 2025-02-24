using BogGames.Tools.Achievements;
using UnityEngine;

[OrderInfo("LUTEAchievements",
              "Create Achievements List Button",
              "Creates a button that will show the achievement menu when pressed.")]
[AddComponentMenu("")]
public class CreateAchievementListButton : GenericButton
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

        var popupIcon = SetupButton();

        UnityEngine.Events.UnityAction action = () =>
        {
            list.FadeAchievementMenu();
        };

        SetAction(popupIcon, action);

        Continue();
    }
    public override string GetSummary()
    {
        return "Creates a button that will show the achievement menu when pressed.";
    }

    public override Color GetButtonColour()
    {
        return new Color32(235, 191, 217, 255);
    }
}