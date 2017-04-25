using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {
    private List<IManager> managers;

    void Awake() {
        DontDestroyOnLoad(gameObject);
        managers = new List<IManager> {
            EventManager.instance,
            GameManager.instance,
            AudioManager.instance,
            UIManager.instance,
            LevelManager.instance
        };
/*        if (SceneManager.GetSceneByName("Game") == SceneManager.GetSceneAt(SceneManager.sceneCount)) {
             print("game csene");
        }*/
        StartCoroutine(StartUpManagers());
    }

    private IEnumerator StartUpManagers() {
        foreach (var manager in managers) {
            manager.StartUp();
        }
        yield return null;
        int managersCount = managers.Count;
        int managersReady = 0;

        while (managersReady < managersCount) {
            int lastReady = managersReady;
            managersReady = 0;

            foreach (var manager in managers) {
                if (manager.status == ManagerStatus.STARTED) {
                    managersReady++;
                }
            }

            if (managersReady > lastReady) {
                print("Progress: " + managersReady + "/" + managersCount);
                yield return null;
            }
        }

        print("All managers startet up.");
    }
}