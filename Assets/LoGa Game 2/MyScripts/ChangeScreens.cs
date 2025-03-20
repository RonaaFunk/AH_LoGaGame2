using UnityEngine;

[OrderInfo("Custom",
              "ChangeScreen",
              "Changes The active Screen by Hiding all the unwanted ones and showing the desired one")]
[AddComponentMenu("")]


public class ChangeScreens : Order
{
     [Tooltip("The objects to hide")]
    [SerializeField] GameObject[] screensToHide;

     [Tooltip("The objects to show")]
    [SerializeField] GameObject screenToShow;

    [Tooltip("Time to wait until the object is hidden")]
    [SerializeField] protected float delay = 0f;
    public override void OnEnter()
    {
        if (screensToHide == null)
        {
            Continue();
            return;
        }

        Invoke("ChangeActiveScreen", delay);
        Continue();
    }

    private void ChangeActiveScreen()
    {
        foreach (var screen in screensToHide){
            screen.SetActive(false);
        }

        screenToShow.SetActive(true);
    }

/**
    public override string GetSummary()
    {
        if (screensToHide == null)
        {
            return "Error: Object to hide is not provided";
        }
        else
        {
            foreach (var screen in screensToHide){
                return "Hide: " + screen.name + " in " + delay + " seconds";
            }
        }
    }
**/
}