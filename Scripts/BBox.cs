using UnityEngine;
using System.Collections;

public class BBox : MonoBehaviour {

    // Use this for initialization
    Animator anim;
    bool end = false;
    float delay;
	void Start () {
        anim = GetComponent<Animator>();
        anim.SetBool("a", true);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (end)
            delay += Time.deltaTime;
        else
            anim.SetBool("a", true);


        if (delay > .5)
            Object.Destroy(gameObject);
                //Destroy(this);
	}

    void OnTriggerStay2D(Collider2D o)
    {
        //Debug.Log("box collision");
        if(o.CompareTag("punch"))
        {
            Debug.Log("puff");
            anim.SetTrigger("Puff");
            end = true;
            anim.SetBool("a", false);
        }

    }
}
