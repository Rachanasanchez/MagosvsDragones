using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumenManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider slider;

    private const string PARAM = "MasterVolumen";
    private const string PREFS_KEY = "VolumenGuardado";

    void Start()
    {
        // Cargar volumen guardado
        float savedValue = PlayerPrefs.GetFloat(PREFS_KEY, 1f);
        Debug.Log(savedValue + "VALOR GUARDADO");
        // Aplicarlo al slider SIN disparar evento
        slider.SetValueWithoutNotify(savedValue);

        // Aplicarlo al mixer
        SetVolume(savedValue);
    }

    public void OnSliderValueChanged(float value)
    {
        SetVolume(value);

        // Guardar valor
        PlayerPrefs.SetFloat(PREFS_KEY, value);
        PlayerPrefs.Save();
    }

    private void SetVolume(float value)
    {
        // evitar error log(0)
        if (value <= 0.0001f)
        {
            audioMixer.SetFloat(PARAM, -80f); // silencio
        }
        else
        {
            audioMixer.SetFloat(PARAM, Mathf.Log10(value) * 20);
        }
    }
}