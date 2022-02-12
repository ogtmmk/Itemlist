using UnityEngine;
using UnityEngine.UI;

public class ItemContents : MonoBehaviour
{

    [SerializeField] private Button _selfBtn = null;
    [SerializeField] private Text _thisBtnTxt = null;

    public delegate void OnDelegate(int num, ItemContents itemcontents);//delegate��`

    private int _itemId = 0;                                           //Item����Id
    private OnDelegate _delegateOnClickButton = null;	               //�f���Q�[�g�ϐ�

    /// <summary>
    /// ���g�̕R�Â����{�^�����擾���AItemListManager����󂯎����Id��\���B
    /// �{�^�����������ꂽ��f���Q�[�g�ϐ��ɑ�����ꂽ�֐�������_itemId��p���ČĂяo��
    /// </summary>
    private void Start()
    {
        //���g(Item)�̕\����ݒ�
        _thisBtnTxt.text = "Item : " + _itemId;
        _selfBtn.onClick.AddListener(() =>
        {
            _delegateOnClickButton(_itemId, this);
        }
        );

    }
    /// <summary>
    /// �f���Q�[�g�ϐ��ɁAItemListManager����󂯎�����֐�����
    /// </summary>
    /// <param name="del">ItemListManager����󂯎�����֐�</param>
    public void SendOnClickDelegate(OnDelegate del)
    {
        _delegateOnClickButton = del;
    }


    /// <summary>
    /// //���g�̔ԍ����󂯎��A_itemId�ɔԍ����i�[
    /// </summary>
    /// <param name="num">ItemListManager����󂯎����Id</param>
    public void SendIdToItem(int num)
    {
        _itemId = num;
    }

}
