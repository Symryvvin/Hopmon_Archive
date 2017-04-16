public interface IManager {
    ManagerStatus status { get; }
    void StartUp();
}