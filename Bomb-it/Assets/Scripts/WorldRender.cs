using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRender : MonoBehaviour {
	public GameObject blockPrefab, destructibleBlockPrefab, terrain, playerLocal, playerRemote, floor;
	private List<GameObject> blocks;
	private float blockSize = 0.1f;
	private World world;

    public List<GameObject> Blocks {
        get { return blocks; }
    }

    public World World1 {
        get { return world; }
    }

    // Use this for initialization
	void Start () {
		this.world = JsonReader.Read ();

		int rows = this.World1.blocks.Count / this.World1.rowSize;

		blocks = new List<GameObject> (new GameObject[rows * this.World1.rowSize]);

		int i;
	    for (i = 0; i < this.World1.blocks.Count; i++) {
	        GameObject baseBlock;

	        if (this.World1.blocks[i] == 1) {
	            baseBlock = blockPrefab;
	        } else if (this.World1.blocks[i] == 2) {
	            baseBlock = destructibleBlockPrefab;
	        } else {
	            baseBlock = floor;
	        }

	        float x = getXPosition(i);
	        float z = getZPosition(i);
	        GameObject newBlock = Instantiate(baseBlock, new Vector3(x, baseBlock.transform.position.y, z), Quaternion.identity);
	        Blocks.Insert(i, newBlock);
	    }

		spawnPlayer (false);
	}

	// Update is called once per frame
	void Update () {

	}

	float getXPosition(float index) {
	    return index % this.World1.rowSize; //- this.world.blocks.Count / this.world.rowSize / 2;
	}

	float getZPosition(float index) {
	    return Mathf.Floor(index / this.World1.rowSize); //- this.world.rowSize / 2;
	}

	GameObject spawnPlayer(bool remote) {
		int spawnPoint = World1.spawnPoints[(int)Random.Range (0.0f, World1.spawnPoints.Count)];
		return Instantiate (remote ? playerRemote : playerLocal, new Vector3 (getXPosition(spawnPoint), 2.0f, getZPosition(spawnPoint)), Quaternion.identity);
	}
}
