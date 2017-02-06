using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldRenderer : MonoBehaviour {
    [SerializeField] private GameObject _playerLocal;

    [SerializeField] private GameObject _playerRemote;

    [SerializeField] private GameObject _floor;

    [SerializeField] private GameObject _wall; // non destroyable

    [SerializeField] private GameObject _block; // destroyable

    private World _myWorld;

    private List<GameObject> _gameGameBlocks;

    public List<GameObject> GameBlocks {
        get { return _gameGameBlocks; }
    }

    public World MyWorld {
        get { return _myWorld; }
    }

    // Use this for initialization
    private void Start() {
        _myWorld = JsonReader.Read();

        int rows = _myWorld.blocks.Count / _myWorld.rowSize;

        _gameGameBlocks = new List<GameObject>(new GameObject[rows * _myWorld.rowSize]);

        int idx = 0;
        _myWorld.blocks.ForEach(block => {
            GameObject baseBlock;

            if (_myWorld.blocks[idx] == 1) {
                baseBlock = _wall;
            } else if (_myWorld.blocks[idx] == 2) {
                baseBlock = _block;
            } else {
                baseBlock = _floor;
            }

            Vector3 newBlockPosition = CalculatePosition(idx);
            newBlockPosition.y = baseBlock.transform.position.y;

            GameObject newBlock = Instantiate(baseBlock, newBlockPosition, Quaternion.identity);

            _gameGameBlocks.Insert(idx, newBlock);

            idx++;
        });

        spawnPlayer(false);
    }

    private Vector3 CalculatePosition(float linealPosition) {
        float x = linealPosition % _myWorld.rowSize;
        // ReSharper disable once PossibleLossOfFraction
        float z = Mathf.Floor(linealPosition / _myWorld.rowSize);

        return new Vector3(x, 0f, z);
    }

    private void spawnPlayer(bool remote) {
        int spawnPoint = MyWorld.spawnPoints[(int) Random.Range(0.0f, MyWorld.spawnPoints.Count)];
        Vector3 spawnPosition = CalculatePosition(spawnPoint);
        spawnPosition.y = 2f;

        Instantiate(remote ? _playerRemote : _playerLocal, spawnPosition, Quaternion.identity);
    }

    public void Destroy(int position) {
        Vector3 newBlockPosition = CalculatePosition(position);
        newBlockPosition.y = _floor.transform.position.y;
        GameObject newFloorBlock = Instantiate(_floor, newBlockPosition, Quaternion.identity);

        Destroy(_gameGameBlocks[position]);

        _gameGameBlocks[position] = newFloorBlock;
        _myWorld.blocks[position] = 0;
    }
}