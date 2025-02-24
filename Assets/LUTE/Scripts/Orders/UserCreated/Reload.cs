using UnityEngine;

[OrderInfo("Scenes",
              "Reload",
              "Reload the current scene")]
[AddComponentMenu("")]
public class Reload : Order
{
    [Tooltip("Image to display while loading the scene")]
    [SerializeField]
    protected Texture2D loadingImage;

    public override void OnEnter()
    {
        LevelSelector.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, loadingImage);

        Continue();
    }

    public override string GetSummary()
    {
        return "";
    }

    public override Color GetButtonColour()
    {
        return new Color32(184, 210, 235, 255);
    }
}