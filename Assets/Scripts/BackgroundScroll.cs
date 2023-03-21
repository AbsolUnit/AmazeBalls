using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroll : MonoBehaviour
{
    public Image image;
    private Material mat;

    private void Start()
	{
        mat = image.material;
    }

	// Update is called once per frame
	void FixedUpdate()
    {
        mat.SetVector("_Offset", new Vector4(mat.GetVector("_Offset").x + 0.0001f, mat.GetVector("_Offset").y - 0.0001f, 0, 0));
    }
}
