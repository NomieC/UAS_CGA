using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
    [System.Serializable]
    public class Drops
    {
        public string name;
        public GameObject itemPrefab;
        public float dropRate;
    }

    public List<Drops> drops;

    private void OnDestroy() {
        float random = UnityEngine.Random.Range(0f, 100f);
        List<Drops> possibleDrop = new List<Drops>();

        foreach (Drops rate in drops)
        {
            if(random <= rate.dropRate){
                possibleDrop.Add(rate);
            }
        }

        if(possibleDrop.Count > 0){
            Drops drops = possibleDrop[UnityEngine.Random.Range(0, possibleDrop.Count)];
            Instantiate(drops.itemPrefab, transform.position, Quaternion.identity);
        }
    }

}
