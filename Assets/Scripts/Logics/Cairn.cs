using System.Collections;
using System.Collections.Generic;
using ScriptableEvents;
using UnityEngine;

public class Cairn : MonoBehaviour
{

    // Je gère une liste de cailloux à rajouter dans l'ordre dans le cairn 
    // Je renvoie une liste de coordonnée de cailloux 

    // Le cairn est une grille carée de taille = la largeur de l'étage le plus bas 
    // exemple :                                         
    //  _ _ _ _ _            un cailloux de largeur 3 et de coordonées (0,3) sela ici  _ _ _ _ _   et occupe l'espace suivant  _ _ _ _ _
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

    [SerializeField] private CairnData cairnData;
    private List<Rock> rocksInCairn = new List<Rock>();
    private List<bool> spaces = new List<bool>();
    private List<int[]> rocksInCairnCoords = new List<int[]>();
    private int floorNb =0;

    public int floorWidth => cairnData ? cairnData.cairnWidth : 9;

    public GameEvent OnCairnModified;

    // Polish code du cern pour reorganiser cailloux d'un etage avec les gros au milieu ou aleatoirement

    private void FitRockInPlace( Rock rock , int floor, int Pos){
        for (int i =0; i<rock.rockWidth ;i++){
            spaces[floorWidth * floor +Pos+i]= true;
        }
        int[] rockCoords = new int[2] {floor, Pos};
        rocksInCairnCoords.Add(rockCoords);
    }

    private void UnfitRockInPlace(Rock rock , int floor, int Pos){
        for (int i =0; i<rock.rockWidth ;i++){
            spaces[floorWidth * floor +Pos+i]= false;
        }
    }

    private void FindRockPlace(Rock rock){
        int w = rock.rockWidth;
        bool fitted = false;
        bool canFit;

        //  je parcours tous les etages du cairn pour trouver une place au caillou
        for (int i=0; i< floorNb; i++){ //pour chaque etage du cern
            if(fitted){
                break;
            }

            for(int k=0; k<floorWidth;k++){ // a chaque position de cet etage 
                if(k+w <= floorWidth){ //Si le caillou peut rentrer à cet etage depuis cette position, je regarde si l'espace est libre ou deja pris par d'autre caillou 
                    canFit= true;
                    for (int j =0; j<w;j++){
                        if(spaces[floorWidth*i+k+j]==true){
                            canFit =false;
                            break;
                        }
                    }
                    if(canFit){ // auquel cas je place le caillou et je sors du process 
                        FitRockInPlace( rock, i,k);
                        fitted= true;
                        break;
                    }
                }
            }
        }
        if(!fitted){ // si le caillou n'a pu rentrer nulle part dans les étages existants 
            AddFloor();   // j'ajoute un étage 
            FitRockInPlace( rock, floorNb-1,0); // et j'ajoute le caillou au debut de l'étage
        }
    }

    private void FillCairnWithOrderedRockList(){ 
        foreach (Rock rock in rocksInCairn){
            FindRockPlace(rock);
        }
    }

    private void FillCairnWithOrderedRockListFromIndex(int idx){ 
        for(int i=idx;i<rocksInCairn.Count;i++){
            FindRockPlace(rocksInCairn[i]);
        }
    }

    private void AddFloor(){
        for (int i=0;i<floorWidth;i++){
            spaces.Add(false);
        }
        floorNb++;
    }

    // fonction qui crée un cairn vide à 1 étage
    private void InitializeCairnSpace(){
        floorNb = 0;
        spaces = new List<bool>();
        AddFloor();
    }

    private void ResetCairnSpace(){
        for( int i=0; i<  spaces.Count; i++ ){ // vide le cairn
                spaces[i]= false;
        }
    }

    private void ResetRockCoords(){ // efface les coordonées des rochers 
        rocksInCairnCoords = new List<int[]>();
    }

    private void RemoveLastFloor(){
        for(int i=0;i<floorWidth;i++){
            spaces.RemoveAt(spaces.Count-1); //ICI
        }
        floorNb--;
    }

    private void RemoveRocksfromCairnFromIndex(int idx){
        for(int i=idx;i<rocksInCairn.Count;i++){
            UnfitRockInPlace( rocksInCairn[i] , rocksInCairnCoords[i][0], rocksInCairnCoords[i][1]);
        }
        for(int i=idx;i<rocksInCairn.Count;i++){ //ICI
            rocksInCairnCoords.RemoveAt(idx);
        }
    }

    private void RemoveEmptyFloors(){
        while (floorNb>0){
            bool emptyLastFloor=true;
            for (int j=0;j<floorWidth;j++){
                if(spaces[floorWidth*floorNb-(j+1)]==true){
                    emptyLastFloor=false;
                    break;
                }
            }
            if(emptyLastFloor){
                RemoveLastFloor();
            }
            else{
                break;
            }
        }
    }

    public void ResetCairn(){
        InitializeCairnSpace();
        ResetRockCoords();
        FillCairnWithOrderedRockList();
    }

    public void AddRock(Rock rock)
    {
        AddRock(rock, true);
    }
    
    public void AddRock(Rock rock, bool save){
        rocksInCairn.Add(rock);
        FindRockPlace(rock);

        if (save) Save();
        OnCairnModified?.Raise();
    }

    public void RemoveRandomRock(){
        int idxRockToRemove = Random.Range(0,rocksInCairn.Count);
        RemoveRocksfromCairnFromIndex(idxRockToRemove);
        rocksInCairn.RemoveAt(idxRockToRemove); 
        FillCairnWithOrderedRockListFromIndex(idxRockToRemove);  
        RemoveEmptyFloors();
    }

    public bool HasRock(int floor, int location){
        return spaces[floor*floorWidth+location];
    }
    
    public Rock GetRockAtLocation(int floor, int location){
        int idx=0;
        foreach(Rock r in rocksInCairn){
            if(floor == rocksInCairnCoords[idx][0] && location == rocksInCairnCoords[idx][1]){
                return r;
            }
            idx+=1;
        }
        return null;
    }
    
    public Rock[] GetRocks(int floor){
        Rock[] rocks = new Rock[floorWidth];
        for(int i=0; i<floorWidth;i++){
            rocks[i] = GetRockAtLocation(floor,i);
            
        }
        return rocks;
    }

    public int GetFloorNumber(){
        return floorNb;
    }

    public void Save()
    {
        List<Rock> pebbles = new List<Rock>();
        int layersCount = GetFloorNumber();
        for (int layerID = 0; layerID < layersCount; layerID++)
        {
            Rock[] rocks = GetRocks(layerID);
            int pebbleIndex = 0;
            while(pebbleIndex < rocks.Length)
            {
                Rock pebble = rocks[pebbleIndex];
                if (pebble != null)
                {
                    pebbles.Add(pebble);
                    pebbleIndex += pebble.rockWidth;
                }
                else pebbleIndex++;
            }
        }
        
        cairnData.Save(pebbles.ToArray());
    }
}