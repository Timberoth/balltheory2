using UnityEngine;
using System.Collections;

public class Storage : Gizmo {
				
	public override void DoMathematicalOperation()
	{
		// Do nothing because this gizmo is only used for storage.
	}	
	
	public override void Update()
	{
		// Do nothing because storage is used for storage.
	}
	
	void OnMouseUp()
	{
		bool mouseUp = Input.GetMouseButtonUp(0);
		
		if( mouseUp )
		{
			if( !processing )
				StartCoroutine(BeginProcessing());
		}		
	}			
}
