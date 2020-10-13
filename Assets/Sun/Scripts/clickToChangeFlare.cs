using UnityEngine;

public class clickToChangeFlare : MonoBehaviour {

    Flare flare1;
    Flare flare2;
    UnityEngine.Light lig;
void Update(){
    if (Input.GetKeyDown(KeyCode.Mouse0)) {
        if (lig.flare == flare1) {
            lig.flare = flare2;
        } else {
            lig.flare = flare1;
        }


    }
}
}