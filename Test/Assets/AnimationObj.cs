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
        //* Don't work
        Debug.Log(segm_test(new Vector3(16,-12),new Vector3(22,5),new Vector3(0,0),new Vector3(7,-6)));
        anim = new Animations(arpoly(new Vector3(0,0),4,8,6),true,10);
        transform.position = anim.Points[0];
        //*/
        //anim = new Animations(new Vector3[3]{new Vector3(10,10),new Vector3(10,20f),new Vector3(20,20f)},true,10);
    }
    

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(Random.Range(-10, 10), 1, 0);
        if (anim !=null && anim.State != -1) anim.StartAnim(this);
    }
    //*
    float f(Vector3 a, Vector3 b, Vector3 Point){
        Vector3 firstVector = Point - a;
        Vector3 secondVector = b - a;

        return (firstVector.x*secondVector.y-firstVector.y*secondVector.x);
    }
    public bool segm_test(Vector3 a, Vector3 b, Vector3 c, Vector3 d){
        return ((f(a,b,c)*f(a,b,d)<=0)&&(f(c,d,a)*f(c,d,b)<=0));
    }
    public Vector3[] arpoly(Vector3 T,int a, int b,int M){
        Vector3[] Points = new Vector3[M];
        Points[0] = T;
        Vector3 q=T;
        for(int n = 0; n< M;n++){
            Points[n]=q;
            for(int i = 0;i<10000 && n<M-1;i++){
                int d = a + Random.Range(0,b-a);
                float fi = Random.Range(0f,2*Mathf.PI);
                Vector3 localQ = new Vector3(d*Mathf.Cos(fi)+Points[n].x,d*Mathf.Sin(fi)+Points[n].y);
                if(n>=2){
                    bool collision = false;
                    for(int k = 0; k<n;k++){
                        if(k+1!=M-2){
                            if(k<n-1 && segm_test(Points[n],localQ,Points[k],Points[k+1])){ //if "if" dont have k<n-1 then collision in last iteration always true
                                collision = true;
                            }
                        }
                        else{
                            if(segm_test(localQ,Points[0],Points[k],Points[k+1])){
                                collision = true;
                            }
                        }
                    }
                    if(!collision){
                        q = localQ;
                        break;
                    }
                }
                else{
                    q = localQ;
                    break;
                }
            }
            
        }
        return Points;

        //int d = a + Random.Range(0,b-a);
        //float fi = Random.Range(0f,2*Mathf.PI);
        //q = new Vector3(d*Mathf.Cos(fi)+Points[n].x,d*Mathf.Sin(fi)+Points[n].y);
    }
    //*/
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
class Matrix2x2{
    private float[,] matrix = new float[2,2];
    public float this[int i,int ii]
    {
        get
        {
            return matrix[i,ii];
        }
        set
        {
            matrix[i,ii] = value;
        }
    }

    public Matrix2x2(float a11,float a12,float a21,float a22){
        matrix[0,0] = a11;
        matrix[0,1] = a12;
        matrix[1,0] = a21;
        matrix[1,1] = a22;
    }
    public Matrix2x2(Vector3 a, Vector3 b){
        matrix[0,0] = a.x;
        matrix[1,0] = a.y;
        matrix[0,1] = b.x;
        matrix[1,1] = b.y;
    }
    //a[0,0]*b[0,0]+a[0,1]*b[1,0],  a[0,0]*b[0,1]+a[0,1]*b[1,1],
    //a[1,0]*b[0,0]+a[1,1]*b[1,0],  a[1,0]*b[0,1]+a[1,1]*b[1,1]
    
    static public Matrix2x2 Multiplay(Matrix2x2 a,Matrix2x2 b){
        return new Matrix2x2(a[0,0]*b[0,0]+a[0,1]*b[1,0],a[0,0]*b[0,1]+a[0,1]*b[1,1],a[1,0]*b[0,0]+a[1,1]*b[1,0],a[1,0]*b[0,1]+a[1,1]*b[1,1]);
    }
}


//Don't work

/*/



//---MEthods
    

//*/