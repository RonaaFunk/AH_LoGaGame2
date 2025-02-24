using UnityEngine;

[OrderInfo("Scenes",
              "Load Scene",
              "Loads a scene based on the exact scene name")]
[AddComponentMenu("")]
public class LoadScene : Order
{
    [Tooltip("the exact name of the target level. Ensure this added to build index.")]
    [SerializeField] protected StringData sceneName;
    [Tooltip("Image to display while loading the scene")]
    [SerializeField] protected Texture2D loadingImage;

    public override void OnEnter()
    {
        LevelSelector.LoadScene(sceneName, loadingImage);
    }

    public override string GetSummary()
    {
        if (sceneName.Value.Length == 0)
        {
            return "Error: No scene name selected";
        }

        return sceneName.Value;
    }

    public override Color GetButtonColour()
    {
        return new Color32(184, 210, 235, 255);
    }

    public override bool HasReference(Variable variable)
    {
        return sceneName.stringRef == variable ||
            base.HasReference(variable);
    }
}