using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CairnManagement : MonoBehaviour
{

    // Je gère une liste de cailloux à rajouter dans l'ordre dans le cairn 
    // Je renvoie une liste de coordonnée de cailloux 

    // Le cairn est une grille carée de taille = la largeur de l'étage le plus bas 
    // exemple :                                         
    //  _ _ _ _ _            un cailloux de largeur 3 et de coordonées (3,0) sela ici  _ _ _ _ _   et occupe l'espace suivant  _ _ _ _ _
    // | | | | | |    4                                                               | | | | | |                             | | | | | |
    //  _ _ _ _ _                                                                      _ _ _ _ _                               _ _ _ _ _
    // | | | | | |    3                                                               | | | | | |                             | | | | | |
    //  _ _ _ _ _                                                                      _ _ _ _ _                               _ _ _ _ _
    // | | | | | |    2                                                               | | | | | |                             | | | | | |
    //  _ _ _ _ _                                                                      _ _ _ _ _                               _ _ _ _ _
    // | | | | | |    1                                                               | | | | | |                             | | | | | |
    //  _ _ _ _ _                                                                      _ _ _ _ _                               _ _ _ _ _
    // | | | | | |    0                                                               | | |x| | |                             | | |x|x|x|

    //  0 1 2 3 4 

    private List<Rock> rocksInCairn = new List<Rock>();
    private List<bool> spaces = new List<bool>();
    private List<int[]> rocksInCairnCoords = new List<int[]>();

    public int lowestFloorWidth = 9;

    void FitRockInPlace(int idxRockStart, Rock rock , int floor, int idxRockPosInFloor){
        for (int i =0; i<rock.rockWidth ;i++){
            spaces[idxRockStart+i]= true;
        }
        int[] rockCoords = new int[2] {floor+idxRockPosInFloor,floor};
        rocksInCairnCoords.Add(rockCoords);
    }

    void FindRockPlace(Rock rock){
        int w = rock.rockWidth;
        int idxFloorStart =0;
        int floorWidth = lowestFloorWidth;
        bool fitted = false;
        bool canFit;

        //  je parcours tous les etages du cairn pour trouver une place au caillou
        for (int i=0; i< (lowestFloorWidth+1) /2; i++){ //pour chaque etage du cern
            if(fitted){
                break;
            }

            for(int k=0; k<floorWidth;k++){ // a chaque position de cet etage 
                if(k+w <= floorWidth){ //Si le caillou peut rentrer à cet etage depuis cette position, je regarde si l'espace est libre ou deja pris par d'autre caillou 
                    canFit= true;
                    for (int j =0; j<w;j++){
                        if(spaces[idxFloorStart+k+j]==true){
                            canFit =false;
                            break;
                        }
                    }
                    if(canFit){ // auquel cas je place le caillou et je sors du process pour passer au cailloux d'apres 
                        FitRockInPlace(idxFloorStart+k, rock, i,k);
                        fitted= true;
                        break;
                    }
                }
            }

            idxFloorStart+= floorWidth;
            floorWidth-=2;
        }
    }

    void FillCairnWithOrderedRockList(){ 
        foreach (Rock rock in rocksInCairn){
            FindRockPlace(rock);
        }
    }

    // fonction qui crée un cairn vide ( liste de booleen where true = espace vide)
    void InitializeCairnSpace(){
        int n = lowestFloorWidth+1 ;
        for( int i=0; i< (lowestFloorWidth+1) /2; i++ ){ // pour chaque etage du cairn
            for( int j=0; j< n; j++ ){
                spaces.Add(false);
            }
            n-=2; // etage suivant = 2 espaces de moins car pyramidal
        }
    }

    public void ResetCairn(){
        if(spaces.Count>0){
            ResetCairnSpace();
        }
        else{
            InitializeCairnSpace();
        }
        ResetRockCoords();
        FillCairnWithOrderedRockList();
    }

    public void ResetCairnSpace(){
        for( int i=0; i<  spaces.Count; i++ ){ // pour chaque etage du cairn
                spaces[i]= false;
        }
    }

    public void ResetRockCoords(){
        rocksInCairnCoords = new List<int[]>();
    }

    public bool HasRock(int floor, int location)
    {
        int floorWidth= lowestFloorWidth;
        int idxStartFloor=0;
        for (int i=0; i<floor;i++){
            idxStartFloor+= floorWidth;
            floorWidth-=2;
        }
        return spaces[idxStartFloor+location];
    }
    
    public Rock GetRockAtLocation(int floor, int location)
    {
        int idx=0;
        foreach(Rock r in rocksInCairn){
            if(floor == rocksInCairnCoords[idx][1] && location == rocksInCairnCoords[idx][0]){
                return r;
            }
            idx+=1;
        }
        return null;
    }
    
    public Rock[] GetRocks(int floor)
    {
        Rock[] rocks = new Rock[lowestFloorWidth];
        for(int i=0; i<lowestFloorWidth;i++){
            rocks[i] = GetRockAtLocation(floor,i);
            
        }
        return rocks;
    }

    public int GetFloorNumber(){
        return (lowestFloorWidth+1)/2;
    }
    
    //TODO add rodck - discard rock - changer code to allow for multiple pyramids
}
