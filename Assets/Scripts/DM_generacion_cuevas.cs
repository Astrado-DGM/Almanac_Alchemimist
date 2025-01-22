using UnityEngine;
using System.Collections.Generic;

public class DM_generacion_cuevas : MonoBehaviour
{
    private DM_generacion_recursos generaRecursos;
    private SpawinpointPlayer spawn;
    [Header("Valores referencia")]
    public float Multiplicador_Perlin = 10f;//Multiplcador de sonido perlin
    public int ancho = 13; //ancho mapa
    public int largo = 13; //largo mapa     Aumentar conforme se avanza de nivel, talvez +2
    public float offsetX ; //variable azar que modifica el sonido perlin
    public float offsetY ; //variable azar que modifica el sonido perlin
    [Header("Objeto suelo")]
    public GameObject casilla;//referencia al objeto que genera como suelo
    
    //lista de objetos que genera para eliminarla luego
    private List<GameObject> listaCasillas = new List<GameObject>();
    private int[ , ] mapa ;//array de 0 y 1, representa el mapa generado, 0 siendo el suelo
    private int tamanoActual=0;
    private int tamanoNuevo=0;    
     private List<int> coordenadasActualesX = new List<int>();
     private List<int> coordenadasActualesY = new List<int>();
     private List<int> coordenadasNuevasX = new List<int>();
     private List<int> coordenadasNuevasY = new List<int>();
    const int suelo=0;
    const int vacio=1;
    const int sueloReal=2;

    void Start(){ 
        generaRecursos = GetComponent<DM_generacion_recursos>();//definimos el script de generacion cuevas para compartir el mapa
        spawn = GetComponent<SpawinpointPlayer>();//definimos el script de generacion cuevas para compartir el mapa
        
        fillMap();
        generaRecursos.genera();
        spawn.eliminarCrearSpawn();
        
    }
    
    
    void Update(){}
    
    public int[,] getMapa(){//devuelve el array del mapa
        return this.mapa;
    }
    void OnGUI(){//boton de pruebas, actualiza el mapa
        if (GUI.Button(new Rect(10, 10, 75, 40), "Actualizar")){
            emptyMap();
            fillMap();
            spawn.eliminarCrearSpawn();
            generaRecursos.genera();
        }
    }

    private void emptyMap(){//repasamos lista de objetos suelo y los eliminamos
        GameObject[] objeto = GameObject.FindGameObjectsWithTag("Cueva");
        for (int i = 0; i < objeto.GetLength(0); i++){
            Destroy(objeto[i]);
        }
        
    }
    
    private void fillMap(){//crea el mapa y llena los suelos
        offsetX = Random.Range(0, 99999f);
        offsetY = Random.Range(0, 99999f);
        mapa = new int [ancho, largo];    //inicializamos los valores offset y recreamos el mapa

        for (int i = 0; i < ancho; i++){//por el tamaño y el largo creamos el mapa y generamos el suelo
            for (int j = 0; j < largo; j++){
                    float innerI = (float)i / largo * Multiplicador_Perlin + offsetX;//calculo de valor que va dentro del perlin
                    float innerJ = (float)j / ancho * Multiplicador_Perlin + offsetY;
                    float perlin = Mathf.PerlinNoise(innerI, innerJ); //tomamos los valores calculados y los metemos en la funcion perlin

                    if (perlin > 0.5f){//si el valor es mayor a .5 marcamos en el mapa 1, esto representara las paredes
                        mapa[i, j] = vacio;
                    }
                    else{//si es menor a .5, en el mapa se pone 0 y se genera en la respectiva coordenada el suelo
                        mapa[i, j] = suelo;                                 
                    }          
                }
            }
            micelio();
            recreaMapa();
            creaSuelo();
        }
        
    
    private void creaSuelo(){
        for (int i = 0; i < ancho; i++){
            for (int j = 0; j < largo; j++){
                    if(mapa[i,j]==sueloReal){
                    GameObject nuevaCasilla = Instantiate(casilla,new Vector3(( i* 2), 0,(j * 2)),Quaternion.Euler(new Vector3(90, 0, 0)));
                    
                    }
            }
        }
    }
    private void recreaMapa(){
        for (int i = 0; i < ancho; i++){
            for (int j = 0; j < largo; j++){
                mapa[i,j]=vacio;
                
            }
        }
        for (int k=0; k < coordenadasActualesX.Count; k++){
            mapa[coordenadasActualesX[k],coordenadasActualesY[k]]=sueloReal;
        }
    }   

    
     //Sistema micelio
    
    private void micelio(){
        for(int i=0;i<ancho;i++){
            for(int j=0;j<largo;j++){
                tamanoNuevo=0;
                
                if(mapa[i,j]==suelo){
                    coordenadasNuevasX.Clear();
                    coordenadasNuevasY.Clear();
                    expandeVecinos(i,j);
                    if (tamanoNuevo > tamanoActual){
                    tamanoActual = tamanoNuevo;
                    coordenadasActualesX.Clear();
                    coordenadasActualesY.Clear();
                    // Guardamos las coordenadas de la regin "ganadora"
                    coordenadasActualesX = new List<int>(coordenadasNuevasX);
                    coordenadasActualesY = new List<int>(coordenadasNuevasY);
                }
                else{
                    // Descartamos esta regin
                    for (int k=0; k < coordenadasNuevasX.Count; k++)
                    {
                        int x = coordenadasNuevasX[k];
                        int y = coordenadasNuevasY[k];
                        mapa[x, y] = vacio;
                    }
                    
                }
                coordenadasNuevasX.Clear();
                coordenadasNuevasY.Clear();
                }
                
            }
        }
        
        coordenadasNuevasX.Clear();
        coordenadasNuevasY.Clear();
        tamanoActual=0;
        tamanoNuevo=0;

    }
    private void expandeVecinos(int x,int y){
        if(mapa[x,y]!=suelo){return;}

            mapa[x,y]=sueloReal;
            tamanoNuevo++;
            coordenadasNuevasX.Add(x);
            coordenadasNuevasY.Add(y);
        
        if(x+1<ancho){
            expandeVecinos(x+1,y);
        }
        if(x-1>=0){
            expandeVecinos(x-1,y);
        }
        if(y+1<largo){
            expandeVecinos(x,y+1);
        }
        if(y-1>=0){
            expandeVecinos(x,y-1);
        }
    }
}