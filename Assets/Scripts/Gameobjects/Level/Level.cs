using UnityEngine;

public class Level  {
    public int number;
    public string name;
    public string world;
    public Vector3 start;
    public Tile[] parts;
    public Size size;

    public int GetCristallCount() {
        int count = 0;
        foreach (var part in parts) {
            if (part.name.Equals("Cristal"))
                count++;
        }
        return count;
    }
}
