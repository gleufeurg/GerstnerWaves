using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    #region Singleton

    public static WaveManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
    }

    #endregion

    public float amplitude = 1f;
    public float length = 2f;
    public float speed = 1f;
    public float offset = 0f;

    private void Update()
    {
        offset = Time.deltaTime * speed;
    }

    public float GetWaveHeight(float x)
    {
        //Calcul the Amplitude of the wave (its height based on its length)
        //The amplitude (height) is sin(x) (more realistic version is x = cos(theta))
        return amplitude * Mathf.Sin(x / length + offset);
    }
}
