using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HouseDefence.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private AudioSO audioSettings;
        [SerializeField] private Slider backgroundVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;

        private AudioSource backgroundMusicSource;

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);

            backgroundMusicSource = gameObject.AddComponent<AudioSource>();
            backgroundMusicSource.clip = audioSettings.backgroundMusicClip;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.volume = audioSettings.backgroundMusicVolume;
            backgroundMusicSource.Play();

            if (backgroundVolumeSlider != null)
            {
                backgroundVolumeSlider.value = audioSettings.backgroundMusicVolume;
                backgroundVolumeSlider.onValueChanged.AddListener(UpdateBackgroundVolume);
            }

            if (sfxVolumeSlider != null)
            {
                sfxVolumeSlider.value = audioSettings.sfxVolume;
                sfxVolumeSlider.onValueChanged.AddListener(UpdateSFXVolume);
            }
        }

        private void UpdateBackgroundVolume(float volume)
        {
            audioSettings.backgroundMusicVolume = volume;
            backgroundMusicSource.volume = volume;
            Debug.Log($"Background Music Volume set to: {volume}");
        }

        private void UpdateSFXVolume(float volume)
        {
            audioSettings.sfxVolume = volume;
            Debug.Log($"SFX Volume set to: {volume}");
        }

        internal void PlaySFX(SFXType sfxType)
        {
            AudioClip clip = audioSettings.GetSFXClip(sfxType);
            if (clip != null)
            {
                AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, audioSettings.sfxVolume);
            }
        }
    }
}
