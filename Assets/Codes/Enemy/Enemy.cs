using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyScriptable enemyStatus;
    Transform player;
    Animator anim;
    // Start is called before the first frame update
    private void Start()
    {
        player = FindObjectOfType<HeroMovement>().transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemyStatus.MoveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(direction);
    }
}