using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListCharacterUse : MonoBehaviour {

    public List<GameObject> listHeroUse = new List<GameObject>();
    public List<GameObject> listSkisUse = new List<GameObject>();

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Modules.listHeroUse = listHeroUse;
        Modules.listSkisUse = listSkisUse;
    }
}
