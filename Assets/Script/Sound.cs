using System.Collections;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public static IEnumerator PlaySoundAtPoint(AudioClip clip, Vector3 position, float volume = 1f)
    {
        if (clip == null)
        {
            Debug.LogWarning("AudioClip is null. Cannot play sound.");
            yield break;
        }
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.position = position;
        AudioSource aSource = tempGO.AddComponent<AudioSource>();
        aSource.volume = volume;
        aSource.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        Object.Destroy(tempGO);
    }
}