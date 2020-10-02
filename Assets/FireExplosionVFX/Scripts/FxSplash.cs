using UnityEngine;
using System.Collections;

public class FxSplash : MonoBehaviour {
	public ParticleSystem Ps_Splash;
	public ParticleSystem Ps_Trail;
	
	void Start() {
		Ps_Trail.Stop();
		Ps_Splash.Play();
	}
}
