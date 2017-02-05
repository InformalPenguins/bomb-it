using System;
using System.IO;
using UnityEngine;

public static class JsonReader {
    public static World Read() {
        String worldFile = File.ReadAllText("Assets/map01.json");

        World myWorld = JsonUtility.FromJson<World>(worldFile);

        return myWorld;
    }
}