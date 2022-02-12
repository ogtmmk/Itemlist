// OkCancelDialog.cs

using System;
using UnityEngine;

public class OkCancelDialog : MonoBehaviour
{
    public enum DialogResult
    {
        OK,
        Cancel,
    }

    // �_�C�A���O�����삳�ꂽ�Ƃ��ɔ�������C�x���g
    public Action<DialogResult> FixDialog { get; set; }

    // OK�{�^���������ꂽ�Ƃ�
    public void OnOk()
    {
        this.FixDialog?.Invoke(DialogResult.OK);
        Destroy(this.gameObject);
    }

    // Cancel�{�^���������ꂽ�Ƃ�
    public void OnCancel()
    {
        // �C�x���g�ʒm�悪����Βʒm���ă_�C�A���O��j�����Ă��܂�
        this.FixDialog?.Invoke(DialogResult.Cancel);
        Destroy(this.gameObject);
    }
}