
public class ClampGameItem
{
    public static void Clamp(GameItemType itemType, int amount)
    {
        switch (itemType)
        {
            case GameItemType.Coin:
                {
                    GameCache.GC.coin += amount;
                    UICoinText.Instance.UpdateCoinText();
                    break;
                }
            case GameItemType.Shuffle:
                {
                    GameCache.GC.shuffleBoosterAmount += amount;
                    break;
                }
            case GameItemType.Sort:
                {
                    GameCache.GC.sortBoosterAmount += amount;
                    break;
                }
            case GameItemType.VipSlot:
                {
                    GameCache.GC.vipSlotBoosterAmount += amount;
                    break;
                }
        }
    }
    public static void Clamp(ItemSheet itemSheet)
    {
        Clamp(itemSheet.itemType, itemSheet.amount);
    }
    public static void Clamp(GiftInItemSheet itemSheet)
    {
        Clamp(itemSheet.type, itemSheet.amount);
    }
}
