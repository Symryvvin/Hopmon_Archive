using UnityEngine;
using UnityEngine.UI;

public class GameInterface : Menu {
    private LevelStats stats;
    [SerializeField] private Text currentStats;
    [SerializeField] private Image[] chargeSet;

    public void InitGUI() {
        EventManager.StartListener("charging", IncreaseCharge);
        EventManager.StartListener("disCharging", DecreaseCharge);
    }

    public void UpdateLevelStats() {
        currentStats.text = "Level " + stats.LevelNumber + " Cristals " + stats.CristalCount;
    }

    private void IncreaseCharge() {
        foreach (var charge in chargeSet) {
            if (charge.enabled == false) {
                charge.enabled = true;
                break;
            }
        }
    }

    private void DecreaseCharge() {
        foreach (var charge in chargeSet) {
            charge.enabled = false;
        }
    }

    public void SetLevelStats(LevelStats s) {
        stats = s;
    }
}