using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Boost : MonoBehaviour
{
    [SerializeField] float defaultAsteroidsSpeed = -10;
    [SerializeField] float defaultObstaclesSpeed = -9;

    [SerializeField] float speedModifier = 1.5f;

    [SerializeField, Range(0, 1)] float chromaticIntensity = .8f;
    [SerializeField, Range(0, 1)] float vignetteIntensity = .8f;
    [SerializeField] float partsSpeed = 600f;
    float startchromaticIntensity;
    float startvignetteIntensity;
    float startpartsSpeed;

    [Space]
    [SerializeField] Spawner spawner;
    [SerializeField] Ship ship;
    [SerializeField] PostProcessVolume postProcessVolume; 
    [SerializeField] ParticleSystem _particleSystem; 

    private ChromaticAberration chromaticAberration;
    private Vignette vignette;
    List<Asteroids> backgroundAsteroids = new List<Asteroids>();
    List<Asteroids> obstaclesAsteroids = new List<Asteroids>();

    bool isBoosted = false;

    private void Start()
    {
        spawner.speed = defaultObstaclesSpeed;

        postProcessVolume.profile.TryGetSettings(out chromaticAberration);
        startchromaticIntensity = chromaticAberration.intensity.value;

        postProcessVolume.profile.TryGetSettings(out vignette);
        startvignetteIntensity = vignette.intensity.value;

        var mainModule = _particleSystem.main;
        startpartsSpeed = mainModule.startSpeed.constant;

        backgroundAsteroids.AddRange(GameObject.FindObjectsOfType<Asteroids>());
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(1) && !isBoosted)
        {
            foreach (Asteroids asteroid in backgroundAsteroids)
            {
                asteroid.speed = defaultAsteroidsSpeed * speedModifier;
            }

            spawner.speed = defaultObstaclesSpeed * speedModifier;

            obstaclesAsteroids.AddRange(GameObject.FindObjectsOfType<Asteroids>());
            foreach (Asteroids asteroid in backgroundAsteroids)
            {
                if(!asteroid.isDecor)
                    asteroid.speed = defaultAsteroidsSpeed * speedModifier;
            }

            chromaticAberration.intensity.value = chromaticIntensity;
            vignette.intensity.value = vignetteIntensity;

            var mainModule = _particleSystem.main;
            mainModule.startSpeed = partsSpeed;

            ship.isBoost = true;
            isBoosted = true;
        }
        else if (!Input.GetMouseButton(1) && isBoosted)
        {
            foreach (Asteroids asteroid in backgroundAsteroids)
            {
                asteroid.speed = defaultAsteroidsSpeed;
            }

            spawner.speed = defaultObstaclesSpeed;
            obstaclesAsteroids.AddRange(GameObject.FindObjectsOfType<Asteroids>());
            foreach (Asteroids asteroid in backgroundAsteroids)
            {
                if (!asteroid.isDecor)
                    asteroid.speed = defaultAsteroidsSpeed;
            }

            chromaticAberration.intensity.value = startchromaticIntensity;
            vignette.intensity.value = startvignetteIntensity;

            var mainModule = _particleSystem.main;
            mainModule.startSpeed = startpartsSpeed;
            ship.isBoost = false;
            isBoosted = false; 
        }
    }
}