using Assets.Scripts.Gameobjects.Game;
using UnityEngine;
using UnityEngine.UI;

public class HUD : Menu {
    private LevelStats stats;
    [SerializeField] private Text currentStats;
    [SerializeField] private Image[] chargeSet;

    public void InitHUD(LevelStats s) {
        stats = s;
    }

    public void StartListeners() {
        EventManager.StartListener(GameEvents.CHARGE, Charge);
        EventManager.StartListener(GameEvents.DISCHARGE, Discharge);
        EventManager.StartListener(GameEvents.UPDATE_HUD, UpdateHUD);
    }

    public void UpdateHUD() {
        currentStats.text = "Level " + stats.number + " Cristals " + stats.cristals;
    }

    private void Charge() {
        foreach (var charge in chargeSet) {
            if (charge.enabled == false) {
                charge.enabled = true;
                break;
            }
        }
    }

    private void Discharge() {
        foreach (var charge in chargeSet) {
            charge.enabled = false;
        }
    }


}
