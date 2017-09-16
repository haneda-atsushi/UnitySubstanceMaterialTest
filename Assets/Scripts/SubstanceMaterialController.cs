using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SubstanceMaterialController : MonoBehaviour
{
    Renderer             m_Renderer;
    ProceduralMaterial[] m_ProcMaterialArray;

    void Start ()
    {
        m_Renderer = GetComponent<Renderer>();
        m_ProcMaterialArray =
            Resources.FindObjectsOfTypeAll( typeof( ProceduralMaterial ) ) as ProceduralMaterial[];
        Debug.Log( "m_ProcMaterialArray.Length = " + m_ProcMaterialArray.Length );
        foreach ( ProceduralMaterial proc_mat in m_ProcMaterialArray )
        {
            Debug.Log( "proc_mat.name = " + proc_mat.name );
        }
        StartCoroutine( "ChangeProcMaterial" );

        /*
        m_Texture = 
            Resources.Load("ground_rock_face/resources/ground_rock_face_basecolor") as Texture2D;
        Debug.Log( "m_Texture.name = " + m_Texture.name );
        */

        /*
#if UNITY_EDITOR
        obj_array = Resources.LoadAll( "SubstanceArch", typeof( UnityEditor.SubstanceArchive ) );
        foreach ( Object obj in obj_array )
        {
            SubstanceArchive sbs = obj as SubstanceArchive;
            Debug.Log( "sbs = " + sbs.name );
        }
#endif
        */
    }
	
	void Update()
    {
	}

    private IEnumerator ChangeProcMaterial()
    {
        foreach( ProceduralMaterial proc_mat in m_ProcMaterialArray )
        {
            Debug.Log( "Set material to" + proc_mat.name );
            m_Renderer.sharedMaterial = proc_mat;
            yield return new WaitForSeconds( 1.0f );
        }

        Debug.Log( "Finished" );
    }
}
