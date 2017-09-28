using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEditor;

public class SubstanceMaterialController : MonoBehaviour
{
    public GameObject        m_TargetGameObject;
    Renderer                 m_Renderer;
    List<ProceduralMaterial> m_ProcMaterialList = new List<ProceduralMaterial>();

    void Start ()
    {
        Assert.IsNotNull( m_TargetGameObject );

        m_Renderer = m_TargetGameObject.GetComponent<Renderer>();
        Assert.IsNotNull( m_Renderer );

        Object[] obj_array = Resources.LoadAll( "SubstanceArch", typeof( UnityEditor.SubstanceArchive ) );
        foreach ( Object obj in obj_array )
        {
            SubstanceArchive sbs               = obj as SubstanceArchive;
            // https://forum.unity.com/threads/substance-proceduralmaterial-to-material-possible.200767/
            string fromPath                    = AssetDatabase.GetAssetPath( sbs.GetInstanceID() );
            SubstanceImporter fromImporter     = AssetImporter.GetAtPath( fromPath ) as SubstanceImporter;
            int fromMaterialCount              = fromImporter.GetMaterialCount();
            ProceduralMaterial[] fromMaterials = fromImporter.GetMaterials();

            foreach ( ProceduralMaterial proc in fromMaterials )
            {
                m_ProcMaterialList.Add( proc );
            }
        }
        Debug.Log( "m_ProcMaterialArray.Length = " + m_ProcMaterialList.Count );

        /*
        m_ProcMaterialArray =
            Resources.FindObjectsOfTypeAll( typeof( ProceduralMaterial ) ) as ProceduralMaterial[];
        Debug.Log( "m_ProcMaterialArray.Length = " + m_ProcMaterialArray.Length );
        foreach ( ProceduralMaterial proc_mat in m_ProcMaterialArray )
        {
            Debug.Log( "proc_mat.name = " + proc_mat.name );
        }
        */

        /*
        m_Texture = 
            Resources.Load("ground_rock_face/resources/ground_rock_face_basecolor") as Texture2D;
        Debug.Log( "m_Texture.name = " + m_Texture.name );
        */
        StartCoroutine( "ChangeProcMaterial" );
    }
	
	void Update()
    {
	}

    private IEnumerator ChangeProcMaterial()
    {
        // yield return new WaitForSeconds( 1.0f );

        foreach( ProceduralMaterial proc_mat in m_ProcMaterialList )
        {
            Debug.Log( "Set material to" + proc_mat.name );
            m_Renderer.sharedMaterial = proc_mat;
            yield return new WaitForSeconds( 1.0f );
        }

        Debug.Log( "Finished" );
    }
}
