using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSevices : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] chunkParts;
    [SerializeField] float activationDistance;
    [SerializeField] float chekInterval = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckDistance());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CheckDistance()
    {
        while (true)
        {
            foreach (GameObject chunk in chunkParts)
            {
                float distance = Vector3.Distance(player.transform.position, chunk.transform.position);
                if (distance <= activationDistance)
                {
                    chunk.SetActive(true);
                }
                else
                {
                    chunk.SetActive(false);
                }
            }
            yield return new WaitForSeconds(chekInterval);
        }
    }
}
