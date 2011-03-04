using UnityEngine;
using System.Collections;

public class Multiplier : Gizmo {
				
	// Keep a reference to the ModifierBox
	public ModifierBox modifierBox;	
	
	new void Start()
	{
		// Call Gizmo's start first.
		base.Start();
		
		modifierBox = GetComponentInChildren<ModifierBox>();
		if( modifierBox == null )
		{
			print("[ERROR] This Multiplier does not have an child ModifierBox.");	
		}
	}
		
	public override void DoMathematicalOperation()
	{		
		ballCounter *= modifierBox.modifier;		
	}	
}
