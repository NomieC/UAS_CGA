using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
   [SerializeField] private Slider slider;
   [SerializeField] private Transform target;
   private Camera mainCamera;

   void Awake()
   {
       mainCamera = Camera.main;  // Cari main camera
   }

   public void UpdateHealthBar(float currentValue, float maxValue)
   {
       slider.value = currentValue / maxValue;
   }

   void Update()
{
    if (mainCamera != null)
    {
        // Pastikan health bar selalu menghadap kamera penuh
        transform.LookAt(mainCamera.transform);
        transform.Rotate(0, 180, 0);  // Putar 180 derajat agar tidak terbalik
    }
}

}
