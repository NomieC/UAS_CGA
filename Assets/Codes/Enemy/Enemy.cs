using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyScriptable enemyStatus;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<HeroMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemyStatus.MoveSpeed * Time.deltaTime);
    }
}
