using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeMeshHoverboard : MonoBehaviour {

    public List<SetupMeshHoverboard> listHoverboard;
    void OnEnable()
    {
        foreach (SetupMeshHoverboard smh in listHoverboard)
        {
            if (smh.id == Modules.codeSkisUse)
            {
                transform.parent.GetComponent<MeshFilter>().mesh = smh.mesh;
                transform.parent.GetComponent<MeshRenderer>().material = smh.material;
                break;
            }
        }
    }
}
[System.Serializable]//de show ra phan input cua unity editor
public class SetupMeshHoverboard
{
    public string id;
    public Mesh mesh;
    public Material material;

    public SetupMeshHoverboard(string idInput, Mesh meshInput, Material materialInput)
    {
        this.id = idInput;
        this.mesh = meshInput;
        this.material = materialInput;
    }
}