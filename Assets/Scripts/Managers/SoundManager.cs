using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioMixerGroup mixerGroup;
    private Dictionary<AudioClip, int> sonidosActivos = new Dictionary<AudioClip, int>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StopAllCoroutines();
        sonidosActivos.Clear();
    }

    public void PlayLimited(AudioClip clip, int maxSimultaneos, Vector3 posicion, float volumen = 1f)
    {
        if (clip == null) return;

        if (!sonidosActivos.ContainsKey(clip))
            sonidosActivos[clip] = 0;

        if (sonidosActivos[clip] >= maxSimultaneos)
            return;

        sonidosActivos[clip]++;

        StartCoroutine(ReproducirSonido(clip, posicion, volumen));
    }

    private System.Collections.IEnumerator ReproducirSonido(AudioClip clip, Vector3 posicion, float volumen)
    {
        AudioSource temp = new GameObject("TempAudio").AddComponent<AudioSource>();
        temp.transform.position = posicion;
        temp.clip = clip;
        temp.outputAudioMixerGroup = mixerGroup;
        //aplicamos volumen global
        temp.volume = volumen;

        temp.spatialBlend = 0f; // 2D
        temp.Play();

        yield return new WaitForSeconds(clip.length);

        sonidosActivos[clip]--;
        Destroy(temp.gameObject);
    }
}