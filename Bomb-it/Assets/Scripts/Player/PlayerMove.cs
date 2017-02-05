using UnityEngine;

public class PlayerMove : MonoBehaviour {
    private Player myPlayer;

    private void Start() {
        myPlayer = GetComponent<Player>();
    }

    private void Update() {
        float axisHorizontal = Input.GetAxis("Horizontal");
        float axisVertical = Input.GetAxis("Vertical");
        bool isAction = Input.GetButton("Action");

        myPlayer.Move(axisHorizontal, axisVertical);

        if (isAction) {
            myPlayer.Action();
        }
    }
}