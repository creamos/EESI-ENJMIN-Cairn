using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CairnManagement : MonoBehaviour
{
    // Ce serait des scriptable object en fait et pas des GameObjects
    private List<Rock> rocksInCairn = new List<Rock>();
    private List<bool> spaces;

    public int LowestRowWidth;

    

    void FillCairn(){ // In progress, jprend mon temps mdr
        int w;
        foreach (Rock rock in rocksInCairn){
            w = rock.rockWidth;

            // Pour chaque rock je parcours tous les etages du cairn pour trouver une place au caillou
            for (int i=0; i< (LowestRowWidth+1) /2; i++){

            }
        }
    }

    // fonction qui crée un cairn vide ( liste de booleen where true = espace vide)
    void InitializeCairnSpace(){
        int n = LowestRowWidth+1 ;
        for( int i=0; i< (LowestRowWidth+1) /2; i++ ){ // pour chaque etage du cairn
            for( int j=0; j< n; j++ ){
                spaces.Add(true);
            }
            n-=2; // etage suivant = 2 espaces de moins car pyramidal
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeCairnSpace();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
