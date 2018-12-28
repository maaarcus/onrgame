using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
    public CharacterController2D controller;
    public Transform goTransform;
    public GameObject hook;
    public GameObject currentHook;

    public Vector3 mousePos ;

    public float horizontalMove = 0f;
    public float runSpeed = 30f;
    public bool jump = false;
    public bool shootHook = false;

    // Start is called before the first frame update
    void Start()
    {

    }
    Vector3 unitPos;
    Vector3 hookPos;
    // Update is called once per frame
    void Update()
    {
        if (!hasAuthority)
        {
            return;
        }
        horizontalMove = Input.GetAxisRaw("Horizontal")*runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }



        if (Input.GetMouseButtonDown(0)) {
            CmdSpawnHook();
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            hookPos = currentHook.transform.position;
            unitPos = (mousePos - hookPos).normalized;
            currentHook.GetComponent<HookController>().setTargetPos(hookPos, unitPos, goTransform);
            Debug.DrawLine(hookPos, unitPos * 3f + hookPos, Color.red);
            //currentHook = Instantiate(hook, transform.position, transform.rotation);

        }

        if (shootHook)
        {
            //currentHook.GetComponent<HookController>().shootHook(unitPos + hookPos);
        }

    }

    void FixedUpdate()
    {
        if (!hasAuthority)
        {
            return;
        }
        controller.Move(horizontalMove * Time.fixedDeltaTime,false, jump);
        jump = false;
    }

    [Command]
    void CmdSpawnHook() {
        Debug.Log("asking server to spawn hook");

        currentHook = Instantiate(hook, transform.position, transform.rotation);
        NetworkServer.Spawn(currentHook);
        
        //return currentHook;
    }
}
