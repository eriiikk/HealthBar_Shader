using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Shader : MonoBehaviour
{
    public Material material;
    public float health = 1.0f;
    private float healthCheck = 1.0f;
    [SerializeField] private float quickLerpSpeed = 10.0f;
    [SerializeField] private float slowLerpSpeed = 2.0f;
    private float currentValue;
    private float targetValue;
    private float currentValue2;
    private float targetValue2;
    private bool isChanging;
    private int changeCount;

    //Grab attached material
    private void Awake()
    {
        if(material == null) {
            Renderer renderer = GetComponent<Renderer>();

            if (renderer == null) {
                Image img = GetComponent<Image>();
                if(img!=null) {
                    material = img.material;
                }
            }
            else
            {
                material = renderer.material;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentValue = health;
        currentValue2 = health;
        material.SetFloat("_Value", currentValue);
        material.SetFloat("_Value2", currentValue2);
    }

    // Update is called once per frame
    void Update()
    {
        if (isChanging)
        {
            // Quick lerp towards target value
            float quickLerpAmount = quickLerpSpeed * Time.deltaTime;
            float slowLerpAmount = slowLerpSpeed * Time.deltaTime;

            // +++
            if (targetValue < currentValue) {
                currentValue2 = Mathf.MoveTowards(currentValue2, targetValue2, quickLerpAmount);
                currentValue = Mathf.MoveTowards(currentValue, targetValue, slowLerpAmount);
                //currentValue = Mathf.Lerp(currentValue, targetValue, quickLerpAmount);
            }
            /// ---
            else {
                currentValue = Mathf.MoveTowards(currentValue, targetValue, quickLerpAmount);
                currentValue2 = Mathf.MoveTowards(currentValue2, targetValue2, slowLerpAmount);
                //currentValue2 = Mathf.Lerp(currentValue2, targetValue2, quickLerpAmount);
            }

            // Check if the current and target values are approximately equal
            if (Mathf.Approximately(currentValue, targetValue) && Mathf.Approximately(currentValue2, targetValue2))
            {
                isChanging = false;
            }
            material.SetFloat("_Value", currentValue);
            material.SetFloat("_Value2", currentValue2);
        }

        if (healthCheck != health) {
            healthCheck = health;
            SetHealthValue(health);
        }
    }

    public void SetHealthValue(float newHealthValue)
    {
        if (newHealthValue > 1.0f) {
            newHealthValue = 1;
        }
        health = newHealthValue;
        if (!Mathf.Approximately(newHealthValue, targetValue))
        {
                // Subsequent change, update target values
                targetValue = newHealthValue;
                
                if (targetValue > currentValue) {
                    targetValue2 = currentValue + (targetValue - currentValue);
                }
                else {
                    targetValue2 = currentValue - (currentValue - targetValue);
                }
                
                isChanging = true;
        }
    }

}