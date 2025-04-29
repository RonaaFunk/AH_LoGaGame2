using LoGaCulture.LUTE;
using UnityEngine;

[OrderInfo("Custom",
              "ClearLocations",
              "Clear locations from the directions API")]
[AddComponentMenu("")]

    public class ClearLocations : Order
    {
        [Tooltip("LUTE Directions")]
        [SerializeField] LUTEDirectionsFactory directionsScript;
    public override void OnEnter()
    {
        directionsScript.ClearDirections();
    }
}

