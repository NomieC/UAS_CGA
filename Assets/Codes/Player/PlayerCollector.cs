using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.TryGetComponent(out ICollectible collectible)){
            collectible.Collect();
        }
    }
}
