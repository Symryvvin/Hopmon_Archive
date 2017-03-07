using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public LiveState liveState;

    void Start() {
        liveState = LiveState.ALIVE;
    }

    void ThouchEnemy() {
        liveState = LiveState.DEAD;
    }

}
