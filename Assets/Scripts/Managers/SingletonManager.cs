using UnityEngine;

public abstract class SingletonManager<T> : MonoBehaviour where T : MonoBehaviour {
    protected ManagerStatus managerStatus = ManagerStatus.SHUTDOWN;
    protected static T manager;

    public static T instance {
        get {
            if (manager == null) {
                manager = FindObjectOfType(typeof(T)) as T;
                if (manager == null) {
                    Debug.LogError("There is no " + typeof(T) + " on Scene");
                }
            }
            return manager;
        }
    }

    public void StartUp() {
        managerStatus = ManagerStatus.INITIALIZING;
        Init();
        managerStatus = ManagerStatus.STARTED;
    }

    protected abstract void Init();

}