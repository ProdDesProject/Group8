using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePowerUp : MonoBehaviour
{
    //How much fire rate is added when picking the power-up
    public float addedRate;

    //How long the power-up lasts when picked up
    public float powerupTime;

    private PowerUpController powerUpController;

    private bool picked = false;

    void Start()
    {
        powerUpController = FindObjectOfType<PowerUpController>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player" && !picked)
        {
            Destroy(gameObject);

            picked = true;

            powerUpController.ActivatePowerUp("fireRate", addedRate, powerupTime);
        }
    }
}
