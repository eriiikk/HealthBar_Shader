using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public HealthBar_Shader HBS_health;
    public HealthBar_Shader HBS_stamina;

    public float maxHealth = 200;
    public float currentHealth = 200;
    public float maxStamina = 150;
    public float currentStamina = 150;

    // Start is called before the first frame update
    void Start()
    {
        if(HBS_health != null) {
            HBS_health.SetHealthValue(currentHealth / maxHealth);
        }
        if(HBS_stamina != null) {
            HBS_stamina.SetHealthValue(currentStamina / maxStamina);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            //Health
            if(currentHealth<(maxHealth-25))
            currentHealth+=25;
            else {
                currentHealth = maxHealth;
            }
            //Stamina
            if(currentStamina<(maxStamina-25))
            currentStamina+=25;
            else {
                currentStamina = maxStamina;
            }
        }
        else if (Input.GetKeyDown("s"))
        {
            //Health
            if(currentHealth>5)
            currentHealth-=5;
            else currentHealth = 0;

            //Steamina
            if(currentStamina>5)
            currentStamina-=5;
            else currentStamina = 0;
        }

        //Update shader
        if(HBS_health != null) {
            HBS_health.SetHealthValue(currentHealth / maxHealth);
        }
        if(HBS_stamina != null) {
            HBS_stamina.SetHealthValue(currentStamina / maxStamina);
        }
        
    }
}
