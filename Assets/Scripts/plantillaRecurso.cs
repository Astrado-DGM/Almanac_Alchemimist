using UnityEngine;

public class plantillaRecurso : MonoBehaviour
{
    [Header("Insertar script AparicionRecursos respectivo")]
    public AparicionRecursos caracteristicas;
    private GameObject player;
    
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("Player")!=null){player = GameObject.FindGameObjectWithTag("Player");}
        this.name = caracteristicas.nombreRecurso;
        GetComponent<Renderer>().material.mainTexture = caracteristicas.imagen.texture;

    }
    
    //        GameObject.Find("Main Camera").GetComponent<Transform>().Rotate(0f,grado,0f,Space.Self);

    // Update is called once per frame
    void Update(){
        if(GameObject.FindGameObjectWithTag("Player")!=null){player = GameObject.FindGameObjectWithTag("Player");}
        transform.LookAt(player.transform.position);
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y + 180, 0);        
    }
    public int getRareza(){
        return caracteristicas.rareza;
    }
    public int getZonaDisponible(){
        return caracteristicas.zonaAparicion;
    }
    void OnCollisionEnter(Collision collision) {
        if(!collision.transform.gameObject.tag.Equals("Player")){return;}//si no es el jugador con el que colisiona el objeto , no pasa nada
        bool esRecojido = player.GetComponent<Inventario>().agregarInventario(caracteristicas);
        if(esRecojido){
            Destroy(gameObject);
        }
    }
}
