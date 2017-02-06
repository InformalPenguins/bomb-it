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
        int currentPosition;

        // center
        Instantiate(_fire, transform.position, Quaternion.identity);

        int x = (int) transform.position.x;
        int z = (int) transform.position.z;

        // Right

        int step = 1;
        while (step < _power) {
            if (WR.World1.blocks[Position() + step] != 1) {
                WR.World1.blocks[Position() + step] = 0;
                Destroy(WR.Blocks[Position() + step]);
            }

            Instantiate(_fire, new Vector3(
                    transform.position.x + step,
                    transform.position.y,
                    transform.position.z),
                Quaternion.identity);
            step++;
        }

        // Left
        step = 0;
        while (step < _power) {
            if (WR.World1.blocks[Position() - step] != 1) {
                WR.World1.blocks[Position() - step] = 0;
                Destroy(WR.Blocks[Position() - step]);
            }

            Instantiate(_fire, new Vector3(
                    transform.position.x - step,
                    transform.position.y,
                    transform.position.z),
                Quaternion.identity);
            step++;
        }

//        // to Right
//        currentPosition = 0;
//        while (++currentPosition <= _power) {
//            Instantiate(_fire, new Vector3(
//                    transform.position.x + currentPosition,
//                    transform.position.y,
//                    transform.position.z),
//                Quaternion.identity);
//        }
//
//        // to Left
//        currentPosition = 0;
//        while (++currentPosition <= _power) {
//            Instantiate(_fire, new Vector3(
//                    transform.position.x - currentPosition,
//                    transform.position.y,
//                    transform.position.z),
//                Quaternion.identity);
//        }
//
//        // to Up
//        currentPosition = 0;
//        while (++currentPosition <= _power) {
//            Instantiate(_fire, new Vector3(
//                    transform.position.x,
//                    transform.position.y,
//                    transform.position.z - currentPosition),
//                Quaternion.identity);
//        }
//
//        // to Down
//        currentPosition = 0;
//        while (++currentPosition <= _power) {
//            Instantiate(_fire, new Vector3(
//                    transform.position.x,
//                    transform.position.y,
//                    transform.position.z + currentPosition),
//                Quaternion.identity);
//        }

        Destroy(gameObject);
    }
}