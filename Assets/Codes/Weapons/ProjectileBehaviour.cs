using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    protected Vector3 direction;
    public float destroyBulletTime;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(gameObject, destroyBulletTime);
    }

    // Update is called once per frame
    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;
    }
}
