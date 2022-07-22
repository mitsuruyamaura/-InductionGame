using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    /// <summary>
    /// �{�^������������V�[���J��
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


    // �@�V�[���̑J�ڂ𔭐�������@���@����̏ꏊ�Ɉړ�������
    // �AField �Ƃ����V�[���𖈉�ǂݍ��ށ@���@��ɓ����V�[����ǂݍ��ނ悤�ɂ���
    // �BField �V�[�������[�h���I������Ƃ��A����X�N���v�g�� Start ���\�b�h�Ȃǂ�
    // �t�B�[���h���̂��v���t�@�u�ɂ��Ă����āA������C���X�^���X����
    // ��������΁AField �V�[���ł���A�A���A�n���A���}���A��������ł���


    public enum FieldName {
        �A���A�n��,
        ���}���A,
        �C�V�X
    }

    [SerializeField]
    private GameObject[] filedPrefas;

    /// <summary>
    /// �@�������V�[���J�ځ@���@�ړ��n�_�Ƃ��ɓ������Ƃ��Ɏ��s���郁�\�b�h
    /// </summary>
    /// <param name="fieldName"></param>
    public void ChangeScene(FieldName fieldName) {
        SceneManager.LoadScene("Field");   // �t�B�[���h���̂��v���t�@�u�ɂ��Ă��邽��
        // �A����ۂ̃t�B�[���h�����[�h����


        SetUpField(fieldName);
    }

    /// <summary>
    /// �B�t�B�[���h���̂��v���t�@�u�ɂ��Ă����āA������C���X�^���X����
    /// </summary>
    /// <param name="fieldName"></param>
    public void SetUpField(FieldName fieldName) {

        // fieldName �ϐ������ɕ��������āA����̃t�B�[���h����肵�A������t�B�[���h�Ƃ��Đ�������

        Instantiate(filedPrefas[(int)fieldName]);

        //switch (fieldName) {
        //    case FieldName.�A���A�n��:
        //        // �A���A�n���̃t�B�[���h�̃v���t�@�u���g���āA�A���A�n���𐶐�����
        //        break;


        //}

    }

}
