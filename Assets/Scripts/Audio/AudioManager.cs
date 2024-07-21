using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private AudioSource _sfxAudioSource;
        [SerializeField] private AudioClip _cardMatchSFX;
        [SerializeField] private AudioClip _cardNoMatchSFX;
        
        public void Initialize(float musicVolume, float soundVolume)
        {
            UpdateVolumeMusic(musicVolume);
            UpdateVolumeSound(soundVolume);
        }

        public void UpdateVolumeMusic(float volume) => 
            _musicAudioSource.volume = volume;

        public void UpdateVolumeSound(float volume) => 
            _sfxAudioSource.volume = volume;
        
        public void PlayMatchSFX()
        {
            _sfxAudioSource.clip = _cardMatchSFX;
            _sfxAudioSource.Play();
        }

        public void PlayNoMatchSFX()
        {
            _sfxAudioSource.clip = _cardNoMatchSFX;
            _sfxAudioSource.Play();
        }
    }
}