using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level {
    public int number;
    public int cristals;
    public String name;
    public World world;
    public int lenght;
    public int width;
    public Vector3 start;


    public Level(JsonLevel level) {
        start = level.start;
        number = level.number;
    }
}
