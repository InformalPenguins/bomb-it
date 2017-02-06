using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private GameObject myBomb;

    [SerializeField] private float movementSpeed;

    public int position;

    private Rigidbody myRigidbody;

    private bool canAct;

    private void Awake() { myRigidbody = GetComponent<Rigidbody>(); }

    private void Start() { canAct = true; }

    private void LateUpdate() {
        int x = (int) transform.position.x;
        int z = (int) transform.position.z;

        position = z * 7 + x;
    }

    private void EnableAction() { canAct = true; }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 12) { // fire
            Die();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.transform.gameObject.layer == 10) { // Bomb
            other.transform.GetComponent<Collider>().isTrigger = false;
        }
    }

    public void Die() {
        Destroy(gameObject);
    }

    public void Move(float axisHorizontal, float axisVertical) {
        myRigidbody.velocity = new Vector3(
            axisHorizontal * Time.deltaTime * movementSpeed,
            myRigidbody.velocity.y,
            axisVertical * Time.deltaTime * movementSpeed
        );
    }

    public void Action() {
        if (!canAct) {
            return;
        }

        canAct = false;

        Vector3 newPosition = new Vector3(
            Mathf.Round(transform.position.x),
            0.2f,
            Mathf.Round(transform.position.z)
        );

        Instantiate(myBomb, newPosition, Quaternion.identity);

        Invoke("EnableAction", 1f);
    }
}