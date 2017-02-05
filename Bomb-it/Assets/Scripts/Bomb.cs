using UnityEngine;

public class Bomb : MonoBehaviour {
    [SerializeField] private float _duration;

    [SerializeField] private int _power;

    [SerializeField] private GameObject _fire;

    private void Awake() { Invoke("Blow", _duration); }

    private void Start() { }

    private void Update() { }

    private void Blow() {
        int currentPosition;

        // center
        Instantiate(_fire, transform.position, Quaternion.identity);

        // to Right
        currentPosition = 0;
        while (++currentPosition <= _power) {
            Instantiate(_fire, new Vector3(
                    transform.position.x + currentPosition,
                    transform.position.y,
                    transform.position.z),
                Quaternion.identity);
        }

        // to Left
        currentPosition = 0;
        while (++currentPosition <= _power) {
            Instantiate(_fire, new Vector3(
                    transform.position.x - currentPosition,
                    transform.position.y,
                    transform.position.z),
                Quaternion.identity);
        }

        // to Up
        currentPosition = 0;
        while (++currentPosition <= _power) {
            Instantiate(_fire, new Vector3(
                    transform.position.x,
                    transform.position.y,
                    transform.position.z - currentPosition),
                Quaternion.identity);
        }

        // to Down
        currentPosition = 0;
        while (++currentPosition <= _power) {
            Instantiate(_fire, new Vector3(
                    transform.position.x,
                    transform.position.y,
                    transform.position.z + currentPosition),
                Quaternion.identity);
        }

        Destroy(gameObject);
    }
}