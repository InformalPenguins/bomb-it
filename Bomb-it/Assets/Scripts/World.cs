using System;
using System.Collections.Generic;

[Serializable]
public class World {
    public List<int> blocks;
    public int rowSize;
    public List<int> spawnPoints;
}