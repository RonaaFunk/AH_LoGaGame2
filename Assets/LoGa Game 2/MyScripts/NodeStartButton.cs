using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[EventHandlerInfo("Custom",
                  "ButtonStart",
                  "Executes Node when button clicked")]
[AddComponentMenu("")]
public class NodeStartButton : EventHandler
{
    [Tooltip("Wait for a number of frames before executing the node")]

    [SerializeField] Button _button;

    void Start()
    {
        _button.onClick.AddListener(OnClicked);
    }

    void OnClicked(){
        ExecuteNode();
    }

    public override string GetSummary()
    {
        return "This node will execute when buttonX is pressed";
    }
}