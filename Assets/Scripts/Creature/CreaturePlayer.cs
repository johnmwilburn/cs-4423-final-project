using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class CreaturePlayer : Creature
{
    [Header("Functional References")]
    public MissionSO missionSO;
    public PlayerSO playerSO;
    public MissionManager missionManager;
    
    [Header("Vignette (MOVE THIS)")]
    public VolumeProfile postProcessingVolume;
    public float maxIntensity = 0.5f;
    public float fadeTime = 0.5f;

    private Vignette vignette;

    protected override void Awake()
    {
        base.Awake();
        projectileType = Projectile.Type.Player;
    }

    public void Update()
    {
        playerSO.health = health;
        playerSO.maxHealth = maxHealth;
        playerSO.ammo = ammo;
        playerSO.maxAmmo = maxAmmo;
    }

    public override void Die()
    {
        FlashVignette(maxIntensity * 1.25f, 10f);
        missionManager.FailMission();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        FlashVignette(maxIntensity, fadeTime);
    }

    public void FlashVignette(float maxIntensity, float fadeTime)
    {
        if (!vignette)
        {

            for (int i = 0; i < postProcessingVolume.components.Count; i++)
            {
                if (postProcessingVolume.components[i].name == "Vignette")
                {
                    vignette = (Vignette)postProcessingVolume.components[i];
                }
            }
        }
        ClampedFloatParameter intensityRef = vignette.intensity;

        StartCoroutine(FadeToClearRoutine());
        IEnumerator FadeToClearRoutine()
        {
            float timer = 0;
            while (timer < fadeTime)
            {
                yield return null;
                timer += Time.deltaTime;
                intensityRef.value = maxIntensity - (timer / fadeTime);
            }
            intensityRef.value = 0f;
        }
    }
}
