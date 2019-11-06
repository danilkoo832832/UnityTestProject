using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AnimationObj : MonoBehaviour
{
    public float speed;
    public Animations anim;

    // Start is called before the first frame update
    void Start()
    {
        /* Don't work
        Debug.Log(segm_test(new Vector3(0,0),new Vector3(0,5),new Vector3(0,0),new Vector3(-5,-0)));
        anim = new Animations(arpoly(new Vector3(0,0),4,12,6),true,10);
        //*/

        //anim = new Animations(new Vector3[3]{new Vector3(10,10),new Vector3(10,20f),new Vector3(20,20f)},true,10);
    }
    

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(Random.Range(-10, 10), 1, 0);
        if (anim !=null && anim.State != -1) anim.StartAnim(this);
    }
}

public class Animations
{
    public Vector3[] Points;
    [SerializeField]
    private byte _loop;
    public byte Loop{get => _loop; set => _loop = value;}
    [SerializeField]
    private float _speed;
    public float Speed{get => _speed; set => _speed = value;}
    public int State{get;private set;}

    
    public Animations(){}
    public Animations(Vector3[] points, byte loop, float speed) {
        Points = points;
        Loop = loop;
        Speed = speed;
    }
    public Animations(Vector3[] points, bool loop, float speed) {
        Points = points;
        if(loop)Loop = 1;
        else Loop = 0;
        Speed = speed;
    }

    public void Reset(){
        State = 0;
    }
    public void StartAnim(AnimationObj obj){
        if((Loop == 1 || State >= 0) && (Loop == 1 || State < Points.Length-1) && State < Points.Length && Speed > 0){
            Vector3 minusVector = -(Points[State % Points.Length]-Points[(State+1) % Points.Length]);
            Vector3 objToPointVector = -(obj.transform.position - Points[(State+1) % Points.Length]);
            objToPointVector.Normalize();
            minusVector.Normalize();
            if ( objToPointVector == minusVector){
                minusVector *= Speed * Time.deltaTime;
                obj.transform.Translate(minusVector);
            }
            else{
                obj.transform.position = Points[(State+1) % Points.Length];
                Debug.Log(State);
                State++;
                if (State == Points.Length) State = 0;
                if(State == Points.Length-1 && Loop == 0){
                State = -1;
                }
            }
        }
    }
}
//Don't work

/*/
class Matrix2x2{
    public float[,] matrix = new float[2,2];

    public Matrix2x2(float a11,float a12,float a21,float a22){
        matrix[0,0] = a11;
        matrix[0,1] = a12;
        matrix[1,0] = a21;
        matrix[1,1] = a22;
    }
    public Matrix2x2(Vector3 a, Vector3 b){
        matrix[0,0] = a.x;
        matrix[0,1] = a.y;
        matrix[1,0] = b.x;
        matrix[1,1] = b.y;
    }

    static public Matrix2x2 Multiplay(Matrix2x2 a,Matrix2x2 b){
        return new Matrix2x2(a.matrix[0,0]*b.matrix[0,0]+a.matrix[0,1]*b.matrix[1,0],a.matrix[0,0]*b.matrix[0,1]+a.matrix[0,1]*b.matrix[1,1],a.matrix[1,0]*b.matrix[0,0]+a.matrix[1,1]*b.matrix[1,0],a.matrix[1,0]*b.matrix[0,1]+a.matrix[1,1]*b.matrix[1,1]);
    }
}


//---MEthods
    Matrix2x2 f(Vector3 a, Vector3 b, Vector3 Point){
        b -= a;
        a = Point - a;
        a.x = Mathf.Abs(a.x);
        a.y = Mathf.Abs(a.y);
        a.z = Mathf.Abs(a.z);
        b.x = Mathf.Abs(b.x);
        b.y = Mathf.Abs(b.y);
        b.z = Mathf.Abs(b.z);
        return new Matrix2x2(a.x,b.x,a.y,b.y);
    }
    public bool segm_test(Vector3 a, Vector3 b, Vector3 c, Vector3 d){
        Matrix2x2 matrix1 = f(a,b,c);
        Matrix2x2 matrix2 = f(a,b,d);
        Matrix2x2 matrixM = Matrix2x2.Multiplay(matrix1,matrix2);
        bool t =false;
        if (matrixM.matrix[0,0] <= 0 || matrixM.matrix[0,1] <= 0 || matrixM.matrix[1,0] <= 0 || matrixM.matrix[1,1] <= 0){
            t = true;
        }
        matrix1 = f(b,c,a);
        matrix2 = f(b,c,b);
        matrixM = Matrix2x2.Multiplay(matrix1,matrix2);
        if (t&&(matrixM.matrix[0,0] <= 0 || matrixM.matrix[0,1] <= 0 || matrixM.matrix[1,0] <= 0 || matrixM.matrix[1,1] <= 0)){
            return true;
        }
        return false;
    }
    public Vector3[] arpoly(Vector3 T,int a, int b,int M){
        Vector3[] Points = new Vector3[M];
        Vector3 q=T;
        for(int n=0; n < M; n++){
            Points[n] = q;
            
            if(n>0){
                for(int i = 0; i<1000;i++){ 
                    int d = a + Random.Range(0,b-a);
                    float fi = Random.Range(0f,2*Mathf.PI);
                    q = new Vector3(d*Mathf.Cos(fi)+Points[n].x,d*Mathf.Sin(fi)+Points[n].y);
                    if(n>=3)
                    {
                        bool collision = false;
                        for(int k=0; k < n-1;k++){
                            if(k+1 != M-1) {
                                if(segm_test(Points[n],q,Points[k],Points[k+1])){
                                    collision = true;
                                    
                                }
                            }
                            else{
                                if(segm_test(q,Points[0],Points[k],Points[k+1])){
                                    collision = true;
                                    Debug.Log(n);
                                }
                            }
                        }

                        if (collision == false){
                            Debug.Log(collision);
                            Points[n]=q;
                            break;
                        }
                    }
                    else
                    {
                        Points[n] = q;
                        break;
                    }

                }
            }
        }
        return Points;
    }

//*/