using UnityEngine;
using System.Collections;

public class Modulus : Gizmo {
				
	// This is the second ball output used in the modulus gizmo.
	private Vector3 ballSpawnPoint2;
	
	// Keep a reference to the ModifierBox
	public ModifierBox modifierBox;
	
	public int modifier = 1;
	public int remainder = 0;
	
	new void Start()
	{
		// Call Gizmo's start first.
		base.Start();
		
		modifierBox = GetComponentInChildren<ModifierBox>();
		if( modifierBox == null )
		{
			print("[ERROR] This Modulus does not have an child ModifierBox.");	
		}
	}
	
	public override void DoMathematicalOperation()
	{		
		ballCounter /= modifierBox.modifier;
		remainder = ballCounter%modifierBox.modifier;
	}
}
