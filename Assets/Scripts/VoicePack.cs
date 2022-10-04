using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu]
public class VoicePack : ScriptableObject
{
    public AudioClip[] roundStartSounds;
    public AudioClip[] takeDamageSounds;

    public AudioClip deathSound;
    public AudioClip healthUpSound;
    public AudioClip timeUpSound;

    public AudioClip hello;

    public AudioClip RoundStartSound()
    {
        int soundPicker = Random.Range(0, roundStartSounds.Length);
        AudioClip soundToPlay = roundStartSounds[soundPicker];
        return soundToPlay;
    }

    public AudioClip TakeDamageSound()
    {
        int soundPicker = Random.Range(0, takeDamageSounds.Length);
        AudioClip soundToPlay = takeDamageSounds[soundPicker];
        return soundToPlay;
    }
}
