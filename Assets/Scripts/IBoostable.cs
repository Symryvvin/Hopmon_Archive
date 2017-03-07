using UnityEngine;

public interface IBoostable {

    Vector3 GetNextPoint(Booster booster);

    void Boost(Vector3 to);

    void OnTriggerEnter(Collider collider);
}
