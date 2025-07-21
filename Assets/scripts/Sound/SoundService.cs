using System;
using UnityEngine;

namespace Puzzle.Sound
{
    public class SoundService
    {
        private SoundScriptableObject soundScriptableObject;
        private AudioSource audioEffects;
        private AudioSource backgroundMusic;

        public SoundService(SoundScriptableObject soundScriptableObject, AudioSource audioEffectSource, AudioSource bgMusicSource)
        {
            this.soundScriptableObject = soundScriptableObject;
            audioEffects = audioEffectSource;
            backgroundMusic = bgMusicSource;
            PlaybackgroundMusic(SoundType.BGM, true);
        }

        public void PlaySoundEffects(SoundType soundType, bool loopSound = false)
        {
            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                audioEffects.loop = loopSound;
                audioEffects.clip = clip;
                audioEffects.PlayOneShot(clip);
            }
            else
                Debug.LogError("No Audio Clip selected.");
        }

        private void PlaybackgroundMusic(SoundType soundType, bool loopSound = false)
        {
            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                backgroundMusic.loop = loopSound;
                backgroundMusic.clip = clip;
                backgroundMusic.Play();
            }
            else
                Debug.LogError("No Audio Clip selected.");
        }

        private AudioClip GetSoundClip(SoundType soundType)
        {
            Sounds sound = Array.Find(soundScriptableObject.soundsList, item => item.soundType == soundType);
            if (sound.audioClip != null)
                return sound.audioClip;
            return null;
        }
    }
}