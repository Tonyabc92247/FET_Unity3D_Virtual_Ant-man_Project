using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acontrol : MonoBehaviour
{
    public Animator anim;
    public int state = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1)){
            state = 1;
         }
        else {
                state = 0;
         }
        anim.SetInteger("state", state);
    }
}
