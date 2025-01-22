using UnityEngine;

public class SpawinpointPlayer : MonoBehaviour
{
    private DM_generacion_cuevas GeneracionCuevas;
    private int[,] mapa;
    private bool selecionado = false;
    public Vector3 coordenadasSpawn;
    public GameObject Player;
    private GameObject save;

    void Start()
    {
        GeneracionCuevas = GetComponent<DM_generacion_cuevas>();//definimos el script de generacion cuevas para compartir el mapa
    }

    public void eliminarCrearSpawn(){
        
        Destroy(save);
        selecionado = false;
        run();
    }    

    private void run(){
                GeneracionCuevas = GetComponent<DM_generacion_cuevas>();//definimos el script de generacion cuevas para compartir el mapa
                mapa = GeneracionCuevas.getMapa();
                int ancho = mapa.GetLength(0);//tomamos el tamaño del mapa
                int largo = mapa.GetLength(1);
                
                while(selecionado==false){
                    int x = Random.Range(0,ancho);//tomamos coordenadas al azar
                    int y = Random.Range(0,largo);
                    if(mapa[x,y]==2){
                            coordenadasSpawn = new Vector3(x*2,1,y*2);
                            mapa[x,y] = 5;
                            selecionado = true;
                    }
                }
                //spawn apartir de las coordenadas selecionadas
                save = Instantiate(Player,coordenadasSpawn, Quaternion.Euler(0, 0, 0));
                
                return;
     }   
}
