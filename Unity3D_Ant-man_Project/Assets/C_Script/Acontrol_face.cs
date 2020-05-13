using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acontrol_face : MonoBehaviour
{
    public Animator anim;
    public int Game_dle;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            anim.SetTrigger("Game_Idle");
        }
    }
}
