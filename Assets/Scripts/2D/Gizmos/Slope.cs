using UnityEngine;
using System.Collections;

public class Slope : MonoBehaviour {
		
	// Double Click Delay
	float DELAY = 0.25f;
	
	float doubleClickStart = -1.0f;	
	bool disableClicks = false;
	
	private void OnMouseUp()
	{
	    if (disableClicks)
	        return;
	    
	    //make sure doubleClickStart isn't negative, that'll break things
	    if (doubleClickStart > 0 && (Time.time - doubleClickStart) < DELAY)
	    {
	        OnDoubleClick();
	        doubleClickStart = -1;
	        StartCoroutine(LockClicks());
	    }
	    else
	    {
	        doubleClickStart = Time.time;
	    }
	}
	
	
	private IEnumerator LockClicks()
	{
	    disableClicks = true;
	    yield return new WaitForSeconds( DELAY );
	    disableClicks = false;
	}	
	
	private void OnDoubleClick()
	{   
	    gameObject.transform.Rotate( 0, 0, 90.0f );
	}
}
