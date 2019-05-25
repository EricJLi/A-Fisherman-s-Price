using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject hook;
    [SerializeField] private GameObject anchor;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        List<Fish> fishies = hook.GetComponent<Hook>().getHooked();
        foreach (Fish f in fishies) {
            f.gameObject.transform.position = anchor.transform.position;
        }
    }


}
