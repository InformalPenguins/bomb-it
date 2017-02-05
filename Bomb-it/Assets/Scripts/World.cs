using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

	public int rows, rowSize;
	public GameObject blockPrefab;
	private List<GameObject> blocks;
	private float blockSize = 0.1f;

	// Use this for initialization
	void Start () {
		this.transform.localScale += new Vector3(rows * blockSize, 0.0f, rowSize * blockSize);

		blocks = new List<GameObject> (new GameObject[rows * rowSize]);

		int i, j;
		
		for (i = 1; i < rows; i+=2) {
			for (j = 1; j < rowSize; j+=2) {
				GameObject newBlock = Instantiate (blockPrefab, new Vector3 (i - rows / 2, 0.5f, j - rowSize / 2), Quaternion.identity);
				blocks.Insert (i * rowSize + j, newBlock);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
