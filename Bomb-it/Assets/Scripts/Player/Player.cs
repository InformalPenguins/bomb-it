using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private GameObject myBomb;

    [SerializeField] private float movementSpeed;

    private Rigidbody myRigidbody;

    private bool canAct;

    private void Awake() { myRigidbody = GetComponent<Rigidbody>(); }

    private void Start() { canAct = true; }

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

        Instantiate(myBomb, transform.position, Quaternion.identity);

        Invoke("EnableAction", 1f);
    }

    private void EnableAction() { canAct = true; }
}