using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGun : MonoBehaviour
{
    [Range(0, 16)]
    public float autoFireFrequency=5;
   
    [Range(5, 70)]
    public float velocity=10;
    public GameObject source;

	[Range(0, 10)]
    public float mass=1;
	float nextFire;
	Transform bulletCase;
	public Action OnFire;
	public float sourceSpread=0.2f;
	void Start()
	{
		var g = new GameObject("BulletCase");
		g.transform.SetParent(transform.parent);
		bulletCase=g.transform;
	}
	public bool autoFire;
    [ExposeMethodInEditor]
    void Fire()
    {
        GameObject s = Instantiate(source);
		s.transform.SetParent(bulletCase);
		s.transform.localPosition=transform.localPosition + sourceSpread*UnityEngine.Random.onUnitSphere;
		s.transform.localRotation=UnityEngine.Random.rotation;
		
        Rigidbody rb = s.AddComponent<Rigidbody>();
		s.SetActive(true);
        rb.AddForce( transform.forward * velocity*velocity);
		rb.mass=mass;
		if (OnFire!=null) OnFire.Invoke();
    }

	void Update()
	{
		if (!autoFire  || Time.time<nextFire) return;
		 nextFire=Time.time+1/(autoFireFrequency*autoFireFrequency);
		Fire();
	}


}
