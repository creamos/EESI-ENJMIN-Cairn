using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CairnManagement : MonoBehaviour
{
    private List<Rock> rocksInCairn = new List<Rock>();
    private List<bool> spaces = new List<bool>();
    private List<int[]> rocksInCairnCoords = new List<int[]>();

    public int lowestFloorWidth = 9;

    void Fit(int idxRockStart, Rock rock , int floor, int idxRockPosInFloor){
        for (int i =0; i<rock.rockWidth ;i++){
            spaces[idxRockStart+i]= false;
        }
        int[] rockCoords = new int[2] {floor+idxRockPosInFloor,floor};
        rocksInCairnCoords.Add(rockCoords);
    }

    void FillCairn(){ // In progress, jprend mon temps mdr
        int w;
        int idxFloorStart;
        int floorWidth ;
        bool canFit;
        bool fitted;

        foreach (Rock rock in rocksInCairn){
            w = rock.rockWidth;
            fitted = false;

            // Pour chaque rock je parcours tous les etages du cairn pour trouver une place au caillou
            idxFloorStart=0;
            floorWidth= lowestFloorWidth;
            for (int i=0; i< (lowestFloorWidth+1) /2; i++){ //pour chaque etage du cern
                if(fitted){
                    break;
                }

                for(int k=0; k<floorWidth;k++){ // a chaque position de cet etage 
                    if(k+w <= floorWidth){ //Si le caillou peut rentrer à cet etage depuis cette position, je regarde si l'espace est libre ou deja pris par d'autre caillou 
                        canFit= true;
                        for (int j =0; j<w;j++){
                            if(spaces[idxFloorStart+k+j]==false){
                                canFit =false;
                                break;
                            }
                        }
                        if(canFit){ // auquel cas je place le caillou et je sors du process pour passer au cailloux d'apres 
                            Fit(idxFloorStart+k, rock, i,k);
                            fitted= true;
                            break;
                        }
                    }
                }

                idxFloorStart+= floorWidth;
                floorWidth-=2;
            }
        }
    }

    // fonction qui crée un cairn vide ( liste de booleen where true = espace vide)
    void InitializeCairnSpace(){
        int n = lowestFloorWidth+1 ;
        for( int i=0; i< (lowestFloorWidth+1) /2; i++ ){ // pour chaque etage du cairn
            for( int j=0; j< n; j++ ){
                spaces.Add(true);
            }
            n-=2; // etage suivant = 2 espaces de moins car pyramidal
        }
    }

    public void ResetCairnSpace(){
        for( int i=0; i<  spaces.Count; i++ ){ // pour chaque etage du cairn
                spaces[i]= false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeCairnSpace();
        FillCairn();
    }

}
