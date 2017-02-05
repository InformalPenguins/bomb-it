using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRender : MonoBehaviour {
	public GameObject blockPrefab, terrain;
	private List<GameObject> blocks;
	private float blockSize = 0.1f;

	// Use this for initialization
	void Start () {
		World world = JsonReader.Read ();

		int rows = world.blocks.Count / world.rowSize;

		terrain.transform.localScale += new Vector3(rows * blockSize, 0.0f, world.rowSize * blockSize);

		blocks = new List<GameObject> (new GameObject[rows * world.rowSize]);

		int i;
		for (i = 0; i < world.blocks.Count; i++) {
			if (world.blocks[i] > 0) {
				float x = i % world.rowSize;
				float z = Mathf.Floor (i / world.rowSize);
				GameObject newBlock = Instantiate (blockPrefab, new Vector3 (x - rows / 2, 0.5f, z - world.rowSize / 2), Quaternion.identity);
				newBlock.GetComponent<Block> ().isDestructible = world.blocks[i] != 1;
				blocks.Insert (i, newBlock);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
