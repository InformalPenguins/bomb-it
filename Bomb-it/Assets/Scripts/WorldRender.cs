using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRender : MonoBehaviour {
	public GameObject blockPrefab, terrain, playerLocal, playerRemote;
	private List<GameObject> blocks;
	private float blockSize = 0.1f;
	private World world;

	// Use this for initialization
	void Start () {
		this.world = JsonReader.Read ();

		int rows = this.world.blocks.Count / this.world.rowSize;

		terrain.transform.localScale += new Vector3(rows * blockSize, 0.0f, this.world.rowSize * blockSize);

		blocks = new List<GameObject> (new GameObject[rows * this.world.rowSize]);

		int i;
		for (i = 0; i < this.world.blocks.Count; i++) {
			if (this.world.blocks[i] > 0) {
				float x = getXPosition(i);
				float z = getZPosition(i);
				GameObject newBlock = Instantiate (blockPrefab, new Vector3 (x, 0.5f, z), Quaternion.identity);
				newBlock.GetComponent<Block> ().isDestructible = this.world.blocks[i] != 1;
				blocks.Insert (i, newBlock);
			}
		}

		spawnPlayer (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	float getXPosition(float index) {
		return index % this.world.rowSize - this.world.blocks.Count / this.world.rowSize / 2;
	}

	float getZPosition(float index) {
		return Mathf.Floor (index / this.world.rowSize) - this.world.rowSize / 2;
	}

	GameObject spawnPlayer(bool remote) {
		int spawnPoint = world.spawnPoints[(int)Random.Range (0.0f, world.spawnPoints.Count)];
		return Instantiate (remote ? playerRemote : playerLocal, new Vector3 (getXPosition(spawnPoint), 2.0f, getZPosition(spawnPoint)), Quaternion.identity);
	}
}
