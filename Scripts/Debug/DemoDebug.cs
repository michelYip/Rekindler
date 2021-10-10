using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoDebug : Behaviour
{
	#region Exposed
	#endregion

	#region Unity API

	private void OnGUI()
	{
		if (!m_isDebug) return; // Design pattern : Exit Early

		if (GUILayout.Button("My first Debug Button"))
		{
			Debug.Log("toto");
		}
	}

	#endregion

	#region Main Methods
	#endregion

	#region Privates
	#endregion
}
