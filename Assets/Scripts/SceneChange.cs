using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    /// <summary>
    /// ボタンを押したらシーン遷移
    /// </summary>
    /// <param name="sceneNo"></param>
    public void ChangeScene(int sceneNo) {

        switch (sceneNo) {
            case 0:
                SceneManager.LoadScene("Field_1");
                break;
            case 1:
                SceneManager.LoadScene("Field_2");
                break;
            case 2:
                SceneManager.LoadScene("Field_3");
                break;
            case 3:
                SceneManager.LoadScene("Field_4");
                break;
            case 4:
                SceneManager.LoadScene("Field_5");
                break;

        }
    }


    // ①シーンの遷移を発生させる　→　特定の場所に移動したら
    // ②Field というシーンを毎回読み込む　→　常に同じシーンを読み込むようにする
    // ③Field シーンをロードし終わったとき、あるスクリプトの Start メソッドなどで
    // フィールド自体をプレファブにしておいて、それをインスタンスする
    // そうすれば、Field シーンでありつつ、アリアハン、ロマリアだったりできる


    public enum FieldName {
        アリアハン,
        ロマリア,
        イシス
    }

    [SerializeField]
    private GameObject[] filedPrefas;

    /// <summary>
    /// ①ここがシーン遷移　→　移動地点とかに入ったときに実行するメソッド
    /// </summary>
    /// <param name="fieldName"></param>
    public void ChangeScene(FieldName fieldName) {
        SceneManager.LoadScene("Field");   // フィールド自体をプレファブにしてあるため
        // ②空っぽのフィールドをロードする


        SetUpField(fieldName);
    }

    /// <summary>
    /// ③フィールド自体をプレファブにしておいて、それをインスタンスする
    /// </summary>
    /// <param name="fieldName"></param>
    public void SetUpField(FieldName fieldName) {

        // fieldName 変数を元に分岐を作って、今回のフィールドを特定し、それをフィールドとして生成する

        Instantiate(filedPrefas[(int)fieldName]);

        //switch (fieldName) {
        //    case FieldName.アリアハン:
        //        // アリアハンのフィールドのプレファブを使って、アリアハンを生成する
        //        break;


        //}

    }

}
