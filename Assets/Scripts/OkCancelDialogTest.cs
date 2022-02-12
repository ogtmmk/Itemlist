// OkCancelDialogTest.cs

using UnityEngine;

public class OkCancelDialogTest : MonoBehaviour
{
    // �_�C�A���O��ǉ�����e��Canvas
    [SerializeField] private Canvas parent = default;
    // �\������_�C�A���O
    [SerializeField] private OkCancelDialog dialog = default;

    public void ShowDialog()
    {
        // ��������Canvas�̎q�v�f�ɐݒ�
        var _dialog = Instantiate(dialog);
        _dialog.transform.SetParent(parent.transform, false);
        // �{�^���������ꂽ�Ƃ��̃C�x���g����
        _dialog.FixDialog = result => Debug.Log(result);
    }
}