using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class gyro2 : MonoBehaviour {
    /// <summary>
    /// ジャイロという名前の重力変化用スクリプト
    /// PCでも操作可能（Aキーで左に90℃変化・Dキーで右に90℃変化する。）
    /// </summary>
    [SerializeField]
    private float nowgyro;//現在の重力（他スクリプトから参照する）
    private bool iscontrol =true;//操作不可能状態を指す。もうちょっとマシな名前にできなかったものかとちょっと後悔
    private int oldgyro;//古い重力を保存しておき、重力に変化があったかを確かめる。
    [SerializeField]
    private float gravity=30f;
    private Vector3 gyroV;
    private float[] setgyro = new float[2];
    private int speed = 1;
    
    void Start () {
        Physics.gravity = new Vector3(0, -30f, 0);
	}
    void Update()
    {
#if UNITY_EDITOR
        //エディター上でのみ行われる処理
        //重力操作を可能にする
        if (Input.GetKey(KeyCode.LeftShift)) speed = 3;
        else speed = 1;
        if (Input.GetKey(KeyCode.A))
        {
            if (gyroV.z >= 360)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                gyroV = new Vector3(0, 0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - 1 * speed);
                gyroV = new Vector3(0, 0, gyroV.z + 1*speed);
            }

        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (gyroV.z <= 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 360);
                gyroV = new Vector3(0, 0, 360);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + 1 * speed);
                gyroV = new Vector3(0, 0, gyroV.z - 1 * speed);
            }
        }
#elif UNITY_ANDROID
        //逆にアンドロイドでのみ行われる処理。VisualStudioだとここ白くなってすごく見辛い。
        Input.gyro.enabled = true;
        if (Input.gyro.enabled)
        {
            Quaternion gyro = Input.gyro.attitude;//スマホの向きを取り
            gyro = Quaternion.Euler(90, 0, 0) * (new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w));//縦持ち用に角度を調整して
            gyroV = gyro.eulerAngles;//オイラー角に変換する。
        }
#endif
        //Debug.Log(gyroV.z);
        //Debug.Log(setgyro[0]);
        //Debug.Log(setgyro[1]);
        if (gyroV.z <= 90 || gyroV.z >= 270)
        {
            //下に重力y-90
            if (gyroV.z >= 270) setgyro[0] = 270 - gyroV.z;
            else setgyro[0] = gyroV.z - 90;
        }
        else
        {
            //上に重力y+90
            if (gyroV.z <= 180) setgyro[0] = gyroV.z - 90;
            else setgyro[0] = gyroV.z - 180;
        }
        if (gyroV.z <= 180)
        {
            //左に重力x-90
            if (gyroV.z <= 90) setgyro[1] = gyroV.z * -1;
            else setgyro[1] = gyroV.z - 180;
        }
        else
        {
            //右に重力x+90
            if (gyroV.z <= 270) setgyro[1] = gyroV.z - 180;
            else setgyro[1] = 360 - gyroV.z;
        }
        for (int i = 0; i < 2; i++)
        {
            setgyro[i] /= 90;
        }
        nowgyro = gyroV.z;

        Physics.gravity = new Vector3(setgyro[1] * gravity, setgyro[0] * gravity, 0);
    }
    public float GetNowGyro
    {
        get
        {
            return nowgyro;
        }
    }
}
