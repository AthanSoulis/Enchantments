using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Torchelight : Spell
{

    private ParticleSystem hitParticles;
    public Light TorchLight;
    public GameObject MainFlame;
    public GameObject BaseFlame;
    public GameObject Etincelles;
    public GameObject Fumee;
    public float MaxLightIntensity;
    public float IntensityLight;

    protected FireRoomRules rules;
    protected Tutorial tutorialRules = null;

    void Start()
    {
        tutorialRules = FindObjectOfType<Tutorial>();
        rules = FindObjectOfType<FireRoomRules>();

        hitParticles = GetComponentInChildren<ParticleSystem>();
        TorchLight.enabled = false;
        TorchLight.GetComponent<Light>().intensity = IntensityLight;
        MainFlame.GetComponent<ParticleSystem>().emissionRate = IntensityLight * 20f;
        BaseFlame.GetComponent<ParticleSystem>().emissionRate = IntensityLight * 15f;
        Etincelles.GetComponent<ParticleSystem>().emissionRate = IntensityLight * 7f;
        Fumee.GetComponent<ParticleSystem>().emissionRate = IntensityLight * 12f;
    }



    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            PlayParticles();
        }
    }

    public override void doSpell(){
        this.PlayParticles();
        
        if(rules != null)
            rules.SpellsToCast--;
        if(tutorialRules != null)
            tutorialRules.SpellsToCast--;
    }

    void PlayParticles()
    {
        hitParticles.Play();
        TorchLight.enabled = true;
    }
}
