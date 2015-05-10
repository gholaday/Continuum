using UnityEngine;
using System.Collections;

public class ColorChange : MonoBehaviour, AudioProcessor.AudioCallbacks
{

    ParticleSystem ps;
    ParticleSystem.Particle[] particles;

    int aliveParticles = 1;

    float spectrumColor;

    public Color color1;
    public Color color2;

    AudioProcessor pro;

	// Use this for initialization
	void Start () {

        ps = GetComponent<ParticleSystem>();
        pro = FindObjectOfType<AudioProcessor>();

        Invoke("CallBack", 1f);
        
        
	
	}
	
	// Update is called once per frame
	void Update () {

        particles = new ParticleSystem.Particle[ps.particleCount];

        aliveParticles = ps.GetParticles(particles);
        
        for (int i = 0; i < aliveParticles; i++)
        {
           particles[i].color = Color.Lerp(color1,color2, spectrumColor);
        }

        
        ps.SetParticles(particles, aliveParticles);
	
	}

    public void onOnbeatDetected()
    {
        
    }

    public void onSpectrum(float[] spectrum)
    {
        
        spectrumColor = (spectrum[5] * 100) / 10;
   
    }

    public void CallBack()
    {
        
        pro.addAudioCallback(this);
    }
}
