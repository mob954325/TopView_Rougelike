using UnityEngine;

public class Test_Jem_Rotate : MonoBehaviour {

    public float xForce = 0, yForce = 0, zForce = 0, speedMultiplier = 1;

    void Update () {
        this.transform.Rotate (xForce * speedMultiplier, yForce * speedMultiplier, zForce * speedMultiplier);
    }

}