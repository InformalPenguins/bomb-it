using UnityEngine;

public class Bomb : MonoBehaviour {
    [SerializeField] private float _duration;

    [SerializeField] private int _power;

    [SerializeField] private GameObject _fire;

    private WorldRender WR;

    private void Awake() { Invoke("Blow", _duration); }

    private void Start() { WR = GameObject.FindGameObjectWithTag("WorldGenerator").GetComponent<WorldRender>(); }

    private void Update() { }

    private int Position() {
        int x = (int) transform.position.x;
        int z = (int) transform.position.z;

        return z * 7 + x;
    }

    private void Blow() {
        // center
        Instantiate(_fire, new Vector3(
                transform.position.x,
                transform.position.y,
                transform.position.z),
            Quaternion.identity);

        // Right

        int step = 1;
        while (step <= _power) {
            int blockFound = WR.MyWorld.blocks[Position() + step];

            if (blockFound == 1) { // Wall
                break;
            }

            Instantiate(_fire, new Vector3(
                    transform.position.x + step,
                    transform.position.y,
                    transform.position.z),
                Quaternion.identity);

            if (blockFound == 2) { // Block
                WR.Destroy(Position() + step);
                break;
            }

            step++;
        }

        // Left

        step = 1;
        while (step <= _power) {
            int blockFound = WR.MyWorld.blocks[Position() - step];

            if (blockFound == 1) { // Wall
                break;
            }

            Instantiate(_fire, new Vector3(
                    transform.position.x - step,
                    transform.position.y,
                    transform.position.z),
                Quaternion.identity);

            if (blockFound == 2) { // Block
                WR.Destroy(Position() - step);
                break;
            }

            step++;
        }

        // Up

        step = 1;
        while (step <= _power) {
            int blockFound = WR.MyWorld.blocks[Position() + step * WR.MyWorld.rowSize];

            if (blockFound == 1) { // Wall
                break;
            }

            Instantiate(_fire, new Vector3(
                    transform.position.x,
                    transform.position.y,
                    transform.position.z + step),
                Quaternion.identity);

            if (blockFound == 2) { // Block
                WR.Destroy(Position() + step * WR.MyWorld.rowSize);
                break;
            }

            step++;
        }

        // Down

        step = 1;
        while (step <= _power) {
            int blockFound = WR.MyWorld.blocks[Position() - step * WR.MyWorld.rowSize];

            if (blockFound == 1) { // Wall
                break;
            }

            Instantiate(_fire, new Vector3(
                    transform.position.x,
                    transform.position.y,
                    transform.position.z - step),
                Quaternion.identity);

            if (blockFound == 2) { // Block
                WR.Destroy(Position() - step * WR.MyWorld.rowSize);
                break;
            }

            step++;
        }

        Destroy(gameObject);
    }
}