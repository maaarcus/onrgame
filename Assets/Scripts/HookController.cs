using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour
{
    public float speed = 10f;
    public float range = 2f;

    public Vector3 targetPos;
    private Vector3 hookPos;
    private bool isPosSet = false;
    private bool isHookAtRange = false;

    Transform ownedPlayerTransform;

    public void setTargetPos(Vector3 hookPos, Vector3 unitPos, Transform ownedPlayerTransform)
    {
        this.hookPos = hookPos;
        this.ownedPlayerTransform = ownedPlayerTransform;
        targetPos = unitPos * range + hookPos;
        isPosSet = true;
    }
 
    public void shootHook(Vector3 targetPos)
    {
        if (transform.position != targetPos){
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        else
        {
            isHookAtRange = true;
        }
    }

    private void hookback()
    {
        if (transform.position != ownedPlayerTransform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, ownedPlayerTransform.position, speed * Time.deltaTime);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (isPosSet)
        {
            if(!isHookAtRange)
            {
                shootHook(targetPos);
            }
            else
            {
                hookback();
            }
        }
    }

}
