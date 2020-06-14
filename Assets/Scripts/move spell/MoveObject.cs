using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR.InteractionSystem;

public class MoveObject : Spell {

    public PlaySound spellSound;
    public MoveRoomRules rules;
    public GameObject obj;
    public Vector3 finalPos;
    public float speed = 3;
    private static bool done1=false, done2 = false, done3=false, done4=false, done5=false;
    private Vector3 startPos;
    private Vector3 endPos;
   // private float distance = 3f;
    private float lerpTime = 5;
    private float currentLerpTime = 0;
    private bool keyHitB = false, keyHitC = false, keyHitF=false, keyHitE = false, keyHitG = false;
    private MeshCollider meshCollider = null;

    protected Tutorial tutorialRules = null;
	// Use this for initialization
	void Start () {

        tutorialRules = FindObjectOfType<Tutorial>();
        obj = gameObject;
        meshCollider = this.gameObject.GetComponent<MeshCollider>();
        
	}

    // Update is called once per frame
    void Update() {
       

        if (Input.GetKeyDown(KeyCode.B))
        {
            
            keyHitB = true;
            //this.doSpell();
        } else if (Input.GetKeyDown(KeyCode.C)) {
            keyHitC = true;

        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            keyHitF = true;

        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            keyHitE = true;

        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            keyHitG = true;

        }
        if ((keyHitB == true) && (obj.name == "dfk_wardrobe_01"))
        {
            doSpell();
            done1 = true;
            keyHitB = false;
        }else if ((keyHitC == true)&& (obj.name == "dfk_chair_01"))
        {
            doSpell();
            done2 = true;
            keyHitC = false;
        }
        else if ((keyHitF == true)&& (obj.name == "dfk_desk_02"))
        {
            doSpell();
            done3 = true;
            keyHitF = false;
        }else if ((keyHitE == true)&& (obj.name == "dfk_bed_single_02"))
        {
            doSpell();
            done4 = true;
            keyHitE = false;
        }
        else if ((keyHitG == true) && (obj.name == "dfk_chest_02_closed2"))
        {
            doSpell();
            done5 = true;
            keyHitG = false;
        }
        if ((done1==true)&& (done2 == true)&& (done3 == true)&& (done4 == true)&& (done5 == true))
        {
            print("SOLVED");
        }
    }

    public override void doSpell() {
        
        if(this.spellSound)
            this.spellSound.Play(-1);
        StartCoroutine(MoveCoroutine());
        
        if(rules != null)
            rules.SpellsToCast--;
        if(tutorialRules != null)
            tutorialRules.SpellsToCast--;
        
    }

     IEnumerator MoveCoroutine()
    {   
        startPos = obj.transform.localPosition;
        while (this.gameObject.transform.localPosition != finalPos)
        {
             currentLerpTime += Time.deltaTime * speed;
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
            }
            float Perc = currentLerpTime / lerpTime;
            obj.transform.localPosition = Vector3.Lerp(startPos, finalPos, Perc);
            
            if(this.gameObject.transform.localPosition == this.finalPos)
                if(this.meshCollider != null)
                    this.meshCollider.enabled = false;

            yield return null;
        }
    }
}
