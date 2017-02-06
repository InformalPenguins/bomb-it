using UnityEngine;

public class Bomb : MonoBehaviour {
    [SerializeField] private float _duration;

    [SerializeField] private int _power;

    [SerializeField] private GameObject _fire;

    private WorldRenderer _wr;

    private void Awake() { Invoke("Blow", _duration); }

    private void Start() { _wr = GameObject.FindGameObjectWithTag("WorldGenerator").GetComponent<WorldRenderer>(); }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 12) { // fire
            CancelInvoke("Blow");
            Blow();
        }
    }

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
            int blockFound = _wr.MyWorld.blocks[Position() + step];

            if (blockFound == 1) { // Wall
                break;
            }

            Instantiate(_fire, new Vector3(
                    transform.position.x + step,
                    transform.position.y,
                    transform.position.z),
                Quaternion.identity);

            if (blockFound == 2) { // Block
                _wr.Destroy(Position() + step);
                break;
            }

            step++;
        }

        // Left

        step = 1;
        while (step <= _power) {
            int blockFound = _wr.MyWorld.blocks[Position() - step];

            if (blockFound == 1) { // Wall
                break;
            }

            Instantiate(_fire, new Vector3(
                    transform.position.x - step,
                    transform.position.y,
                    transform.position.z),
                Quaternion.identity);

            if (blockFound == 2) { // Block
                _wr.Destroy(Position() - step);
                break;
            }

            step++;
        }

        // Up

        step = 1;
        while (step <= _power) {
            int blockFound = _wr.MyWorld.blocks[Position() + step * _wr.MyWorld.rowSize];

            if (blockFound == 1) { // Wall
                break;
            }

            Instantiate(_fire, new Vector3(
                    transform.position.x,
                    transform.position.y,
                    transform.position.z + step),
                Quaternion.identity);

            if (blockFound == 2) { // Block
                _wr.Destroy(Position() + step * _wr.MyWorld.rowSize);
                break;
            }

            step++;
        }

        // Down

        step = 1;
        while (step <= _power) {
            int blockFound = _wr.MyWorld.blocks[Position() - step * _wr.MyWorld.rowSize];

            if (blockFound == 1) { // Wall
                break;
            }

            Instantiate(_fire, new Vector3(
                    transform.position.x,
                    transform.position.y,
                    transform.position.z - step),
                Quaternion.identity);

            if (blockFound == 2) { // Block
                _wr.Destroy(Position() - step * _wr.MyWorld.rowSize);
                break;
            }

            step++;
        }

        Destroy(gameObject);
    }
}