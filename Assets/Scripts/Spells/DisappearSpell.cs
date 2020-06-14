using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearSpell : Spell {
    Component src;
    public bool fadeOut = false;
    float maxSize;
    float speed;
    float waitTime;
    float KeyDownTime = float.MinValue;
    private Material mat;
    Vector3 a;
    protected Tutorial tutorialRules = null;
    void Start()
    {
        speed = 3;
        waitTime = 1/3;
        maxSize = transform.localScale.x*2;
        a = transform.localScale;
        mat = gameObject.GetComponentInChildren<MeshRenderer>().material;
        src = GetComponent<MeshCollider>();
        tutorialRules = FindObjectOfType<Tutorial>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            doSpell();
        }
            
    }

    public override void doSpell ()
    {
       
        if (!fadeOut && src!=null)
        {
            StartCoroutine(Scale());
        }
         else
        { 
            StartCoroutine(FadeOut());
        }
        if(tutorialRules)
            tutorialRules.SpellsToCast--;
    }
    public void diScale() { StartCoroutine(Scale()); }
    public void diFade() { StartCoroutine(FadeOut()); }
    IEnumerator Scale()
    {
        KeyDownTime = Time.time;
        float timer = 0;

        while (Time.time < KeyDownTime + 2) 
        {
            
            while (maxSize > transform.localScale.x)
            {
                timer += Time.deltaTime;
                transform.localScale += new Vector3(a.x, a.y, a.z) * Time.deltaTime * speed;
                yield return null;
            }

            yield return new WaitForSeconds(waitTime);

            timer = 0;
            while (a.x < transform.localScale.x)
            {
                timer += Time.deltaTime;
                transform.localScale -= new Vector3(a.x, a.y, a.z) * Time.deltaTime * speed;
                yield return null;
            }

            timer = 0;
            yield return new WaitForSeconds(waitTime);
        }

        Destroy(gameObject);
    }
    IEnumerator FadeOut()
    {
        Color newColor = mat.color;
        while (newColor.a > 0)
        {
            newColor.a -= Time.deltaTime;
            mat.color = newColor;
            gameObject.GetComponentInChildren<MeshRenderer>().material = mat;
            yield return null;
        }
        Destroy(gameObject);
    }
}
