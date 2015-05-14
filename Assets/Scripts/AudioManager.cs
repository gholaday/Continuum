using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {


    public AudioClip[] clips;
    public bool shuffle = false;
    public Text nowPlaying;

    int count = 0;

    AudioSource source;


	// Use this for initialization
	void Awake () {

        source = GetComponent<AudioSource>();

        if(shuffle)
        {
            RandomizeArray(clips);
        }

        source.clip = clips[count];
        source.Play();
        count++;
        StartCoroutine(NowPlayingFade());
	
	}

    void Update()
    {
        if(!source.isPlaying)
        {
            NextTrack();
        }
    }
    
    void NextTrack()
    {
     
        source.clip = clips[count];
        source.Play();
        StartCoroutine(NowPlayingFade());
        count++;
        if (count >= clips.Length) count = 0;
        
    }

    void RandomizeArray(AudioClip[] arr)
    {
        for (int i = arr.Length - 1; i > 0; i--)
        {
            int r = Random.Range(0, i+1);
            AudioClip tmp = arr[i];
            arr[i] = arr[r];
            arr[r] = tmp;
        }
    }

    IEnumerator NowPlayingFade()
    {
        nowPlaying.CrossFadeAlpha(255, 1f, false);
        nowPlaying.text = "Now Playing";

        yield return new WaitForSeconds(1f);

        nowPlaying.CrossFadeAlpha(0, 1f, false);

        yield return new WaitForSeconds(1.5f);

        nowPlaying.CrossFadeAlpha(255, 1f, false);
        nowPlaying.text = source.clip.name;

        yield return new WaitForSeconds(3f);

        nowPlaying.CrossFadeAlpha(0, 1f, false);

    }

}
