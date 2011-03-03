using UnityEngine;
using System.Collections;

public class Divider : Gizmo {
				
	// Keep a reference to the ModifierBox
	public ModifierBox modifierBox;
	public int modifier = 1;
	
	new void Start()
	{
		// Call Gizmo's start first.
		base.Start();
		
		modifierBox = GetComponentInChildren<ModifierBox>();
		if( modifierBox == null )
		{
			print("[ERROR] This Divider does not have an child ModifierBox.");	
		}
	}
		
	public override void DoMathematicalOperation()
	{		
		ballCounter /= modifierBox.modifier;		
	}	
}
