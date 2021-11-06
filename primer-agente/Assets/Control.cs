using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
	public List <Transform> Nodos = new List <Transform>();
	
	private Transform targetNodoPoint;
	private int targetNodoPointIndex = 0;
	private int lastNodoIndex;
	private float minDistance = 0.3f;
	
	private float speed = 3.0f;
	
	// Start is called before the first frame update
	void Start()
	{
		lastNodoIndex = Nodos.Count -1;
		targetNodoPoint = Nodos[targetNodoPointIndex];
		
	}

	// Update is called once per frame
	void Update()
	{
		float movementStep = speed * Time.deltaTime;
		
		Vector3 direction2target = targetNodoPoint.position - transform.position;
		
		float currentDistance = Vector3.Distance(transform.position, targetNodoPoint.position);
		
		checkDistance(currentDistance);
		
		transform.position = Vector3.MoveTowards(transform.position, targetNodoPoint.position, movementStep);
	}
	
	void checkDistance(float currentDistance)
	{
		if( currentDistance <= minDistance)
		{
			targetNodoPointIndex ++;
			UpdatetargetNodoPoint();
		}
	}
	
	void UpdatetargetNodoPoint()
	{
		
		Debug.Log("Current Node: " + targetNodoPointIndex);
		
		if( targetNodoPointIndex >  lastNodoIndex ){
			targetNodoPointIndex = 0;
		}
	
		targetNodoPoint = Nodos[targetNodoPointIndex];
	
	}
	
}
