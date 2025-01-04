using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemyBig : MonoBehaviour
{
   [SerializeField] private Slider slider;
   [SerializeField] private Transform target;


   public void UpdateHealthBar(float currentValue, float maxValue)
   {
       slider.value = currentValue / maxValue;
   }

}