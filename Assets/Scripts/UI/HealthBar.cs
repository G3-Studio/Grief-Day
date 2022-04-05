using System;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel.Dispatcher;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Player player ;
    [SerializeField] int MaxHealth , Health ;
    Slider slider; 
    [SerializeField] Gradient gradient ;
    [SerializeField] Image fill;
    // Start is called before the first frame update

    public void SetMaxHealth(int maxhealth){
            slider.maxValue =maxhealth ;
            
    }
    public void SetHealth (int health){
            slider.value =health ; 
    }
    void Start()
    {
        slider = GetComponent<Slider>() ;
        slider.minValue = 0 ;
    }
            
    // Update is called once per frame
    void Update()
    {
        MaxHealth = player.GetMaxHealth() ;
        Health = player.GetCurrentHealth() ; 
        fill.color = gradient.Evaluate(slider.normalizedValue);
        Debug.Log(slider.normalizedValue);
        SetMaxHealth(MaxHealth);
        SetHealth(Health);
    }
}
