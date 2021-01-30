using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_Following : MonoBehaviour
{
    

	public float speed;
	public float aggro_range;

	private Transform target;
	public Transform[] points;
	private Transform player;
	private Transform t;
	int current;

    void Start()
    {
    	t = this.transform;
	    current = 0;
	    player = GameObject.FindGameObjectWithTag("Player").transform;
	    	
    }

    void Update()
    {
    	if (player != null)
    	{

	    	if (Vector2.Distance(t.position,player.position) < aggro_range)
	    	{
	    		transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
	    	}
	    	else
	    	{
	    		if (transform.position != points[current].position)
		    	{
		    		transform.position = Vector2.MoveTowards(transform.position, points[current].position, speed * Time.deltaTime);
		    	}
		    	else
		    	{
		    		current = (current + 1) % points.Length;
		    	}
			}
		}
	    //print(Vector2.Distance(t.position,player.position));


    	
    	/*if( GameObject.FindGameObjectWithTag("Player") != null )
    	{
    		transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    	}*/
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }

    void OnTriggerEnter(Collider other) 
    {
    	if (other.tag == "Player") target = other.transform;
 	}
 
	void OnTriggerExit(Collider other) 
	{
    	if (other.tag == "Player") target = null;
 	}
}
