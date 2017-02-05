using System;
using System.IO;
using UnityEngine;

public static class JsonReader {
    public static Worldx Read() {
        String worldFile = File.ReadAllText("Assets/map01.json");

        Worldx myWorld = JsonUtility.FromJson<Worldx>(worldFile);

        return myWorld;
    }
}