﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    private bool jumpBoostEnabled;
    private float normalJumpForce;
    private float jumpBoostForce;
    private float jumpBoostTime;

    private bool damageImmunityCheck;
    private float immunityTime;

    private bool fireRateBoostEnabled;
    private float fireRateBoost;
    private float fireRateBoostTime;
    private float normalFireRate;

    private CharacterController2D characterController;
    private RestartOnPlayerDeath playerDamageController;
    private WeaponScript weaponController;

    public AudioClip audioClip;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        characterController = FindObjectOfType<CharacterController2D>();
        playerDamageController = FindObjectOfType<RestartOnPlayerDeath>();
        weaponController = FindObjectOfType<WeaponScript>();
        normalJumpForce = characterController.m_JumpForce;
        normalFireRate = weaponController.fireRate;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(jumpBoostEnabled)
        {
            jumpBoostTime -= Time.deltaTime;

            if(jumpBoostTime <= 0)
            {
                characterController.m_JumpForce = normalJumpForce;
                jumpBoostEnabled = false;
            }
        }

        if(damageImmunityCheck)
        {
            playerDamageController.EnableDamageImmunity(immunityTime, damageImmunityCheck);
            damageImmunityCheck = false;
        }

        if(fireRateBoostEnabled)
        {
            fireRateBoostTime -= Time.deltaTime;

            if(fireRateBoostTime <= 0)
            {
                weaponController.fireRate = normalFireRate;
                fireRateBoostEnabled = false;
            }
        }
    }

    public void ActivatePowerUp(string name, float force, float time)
    {
        if(name == "jump")
        {
            if(jumpBoostEnabled)
            {
                audioSource.PlayOneShot(audioClip);
                characterController.m_JumpForce = normalJumpForce;

                jumpBoostEnabled = true;
                jumpBoostForce = force;
                jumpBoostTime = time;
                
                characterController.m_JumpForce += jumpBoostForce;
            }
            else
            {
                audioSource.PlayOneShot(audioClip);
                jumpBoostEnabled = true;
                jumpBoostForce = force;
                jumpBoostTime = time;
                
                characterController.m_JumpForce += jumpBoostForce;
            }
        }

        if(name == "immunity")
        {
            audioSource.PlayOneShot(audioClip);
            damageImmunityCheck = true;
            immunityTime = time;
        }

        if(name == "fireRate")
        {
            fireRateBoostEnabled = true;
            fireRateBoost = force;
            fireRateBoostTime = time;

            if(fireRateBoostEnabled)
            {
                audioSource.PlayOneShot(audioClip);
                weaponController.fireRate = normalFireRate;

                fireRateBoostEnabled = true;
                fireRateBoost = force;
                fireRateBoostTime = time;
                
                weaponController.fireRate += fireRateBoost;
            }
            else
            {
                audioSource.PlayOneShot(audioClip);
                fireRateBoostEnabled = true;
                fireRateBoost = force;
                fireRateBoostTime = time;
                
                weaponController.fireRate += fireRateBoost;
            }
        }
    }
}
