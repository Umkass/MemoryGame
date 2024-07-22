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

        public void PlayMatchSFX() =>
            PlaySFX(_cardMatchSFX);

        public void PlayNoMatchSFX() =>
            PlaySFX(_cardNoMatchSFX);

        private void PlaySFX(AudioClip clip)
        {
            _sfxAudioSource.clip = clip;
            _sfxAudioSource.Play();
        }
    }
}