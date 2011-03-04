using UnityEngine;
using System.Collections;

public class ModifierBox : MonoBehaviour {
	
	public int modifier = 1;
	
	// Keep ref to TextMesh
	private TextMesh textMesh;
		
	void Start()
	{
		// Update the text
		textMesh = gameObject.GetComponentInChildren<TextMesh>();
		textMesh.text = modifier.ToString();
	}
	
	void OnMouseUp()
	{			
		bool mouseUp = Input.GetMouseButtonUp(0);
		if( mouseUp )
		{		
			// End particle effect
			
			// Increment counter
			IncrementModifier();					
		}
	}
	
	// Divisor can only go from 1-9
	public void IncrementModifier()
	{
		modifier++;
		
		// Roll back to 1
		if( modifier >= 10 )
			modifier = 1;
		
		// Update text
		textMesh.text = modifier.ToString();
	}
	
	// Divisor can only go from 1-9
	public void DecrementModifier()
	{		
		modifier--;
	
		// Roll up to 9
		if( modifier <= 0 )
			modifier = 9;
		
		// Update text
		textMesh.text = modifier.ToString();
	}
}
