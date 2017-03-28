public interface IPoolable {
    void OnEnable();

    void Destroy();

    void OnDisable();
}