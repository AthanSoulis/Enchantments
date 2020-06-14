using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitSpell : Spell {

	public override void doSpell(){
		Application.Quit();
	}
}
