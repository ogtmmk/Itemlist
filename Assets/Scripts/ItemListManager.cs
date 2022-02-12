using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemListManager : MonoBehaviour
{
    private enum BTN
    {
        Add = 0,
        Remove,
        Use
    }

    [SerializeField] private Button[] _funcButtons = default;
    [SerializeField] private GameObject _itemPrefab = default;
    [SerializeField] private GameObject _content = default;//�{�^�������ꏊ
    [SerializeField] private Text _itemInfoText = default; //�{�^���̏���\������e�L�X�g
    private List<ItemContents> _itemButtons;               //�{�^�����X�g
    private int DEFAULT_ITEM_SIZE = 10;

    private int _current_ManagedId = 0;           //�󂯎�����{�^���̔ԍ����i�[
    private ItemContents _current_ItemContents = null;

    /// <summary>
    /// �e�{�^���ݒ�AUserInventory����ɃA�C�e���𐶐�
    /// </summary>
    void Start()
    {
        //button_ItemAdd
        _funcButtons[(int)BTN.Add].onClick.AddListener(OnClickAddItemButton);
        //button_ItemRemove
        _funcButtons[(int)BTN.Remove].onClick.AddListener(OnClickRemoveItemButton);
        //button_ItemUse
        _funcButtons[(int)BTN.Use].onClick.AddListener(OnClickUseItemButton);
        _funcButtons[(int)BTN.Use].interactable = false;

        _itemButtons = new List<ItemContents>();


        for (int index = 0; index < DEFAULT_ITEM_SIZE; index++)
        {
            MakeButton(index);
        }
    }

    /// <summary>
    /// �N���b�N���ꂽ�{�^���̏����󂯎��B_managedId�ɃA�C�e������ID���i�[�A�p�l���ɃA�C�e���̏���\��������
    /// </summary>
    /// <param name="itemId">ItemContents�N���X����ItemId���󂯎��</param>
    public void OnClickItemButton(int itemId, ItemContents fromItemContents)
    {
        _current_ManagedId = itemId;
        _current_ItemContents = fromItemContents;

        _itemInfoText.text = "Item : " + _current_ManagedId;

        //Use�{�^���̗L����
        _funcButtons[(int)BTN.Use].interactable = true;
    }

    /// <summary>
    /// �{�^������
    /// </summary>
    /// <param name="itemId">�󂯎����Id����Ƀ{�^�������A�������ꂽ�{�^���ɂ�Id��`����</param>
	private void MakeButton(int itemId)
    {
        //����
        GameObject itemObject = Instantiate(_itemPrefab, Vector3.zero, Quaternion.identity);
        itemObject.transform.SetParent(_content.transform);

        //���X�g�ǉ�
        ItemContents item_content = itemObject.GetComponent<ItemContents>();
        _itemButtons.Add(item_content);

        item_content.SendIdToItem(itemId);                    // �A�C�e������Id��^����
        item_content.SendOnClickDelegate(OnClickItemButton);	// �A�C�e���ɃA�C�e���{�^���������ꂽ���Ɏ��s���Ăق����֐���`���遁�f���Q�[�g

    }

    /// <summary>
    /// �����Ŏ󂯎�����^�[�Q�b�g��Item���폜����BUseItem�{�^�����������ꂽ�ꍇ�͈����Ȃ���Remove()���ĂԁB
    /// </summary>
    /// <param name="targetNum">�폜�������A�C�e����int�^�Ŏw��</param>
    private void RemoveItem(int targetNum)
    {
        Destroy(_itemButtons[targetNum].gameObject);
        _itemButtons.RemoveAt(targetNum);
        //UseButton������
        _funcButtons[(int)BTN.Use].interactable = false;
        if (_itemButtons.Count <= 0)
        {
            _funcButtons[(int)BTN.Remove].interactable = false;
        }
    }
    /// <summary>
    /// remove�Ώۂ̃A�C�e����ItemContents�N���X����󂯎��AGetTargetNum()�ŊY������_itemButtons�������ARemoveItem()���Ăяo��
    /// </summary>
    private void RemoveCurrentItem()
    {
        int contentNumber = GetItemTargetNum(_current_ItemContents);
        if (contentNumber >= 0)
        {
            RemoveItem(contentNumber);
        }
    }

    /// <summary>
    /// remove�Ώۂ�_itemButtons�������A���̔ԍ���int�ŕԂ�
    /// </summary>
    /// <param name="target">ItemContents�N���X����󂯎����remove�Ώۂ̃A�C�e��</param>
    /// <returns></returns>
    private int GetItemTargetNum(ItemContents target)
    {
        for (int index = 0; index < _itemButtons.Count; index++)
        {
            if (target == _itemButtons[index])
            {
                return index;
            }
        }
        return -1;
    }

    /// <summary>
    /// Add�{�^���������AitemMasterData�̂��A�C�e���f�[�^���烉���_���Ő���
    /// </summary>
    private void OnClickAddItemButton()
    {
        int itemPickup = Random.Range(0, DEFAULT_ITEM_SIZE);
        MakeButton(itemPickup);

        //Remove�{�^���L����
        _funcButtons[(int)BTN.Remove].interactable = true;
    }

    /// <summary>
    /// Remove�{�^���������A���X�g��1�ȉ��Ȃ�A�C�e�����폜���{�^���𖳌����A���X�g��2�ȏ�̏ꍇ�̓A�C�e���폜�̂�
    /// </summary>
    private void OnClickRemoveItemButton()
    {
        if (_itemButtons.Count > 0)
        {
            //_itemButtons�̗v�f�����J�E���g���A�Ō�̃A�C�e�����폜
            RemoveItem(_itemButtons.Count - 1);
        }
    }

    /// <summary>
    /// �p�l�����{�^���������͂���ɊY������A�C�e�����폜
    /// </summary>
    private void OnClickUseItemButton()
    {
        RemoveCurrentItem();
    }
}
