namespace Pisgor.Inventories {
    public interface IItemManager
    {
        public void RegisterItem(Item newItem);
        public void UnregisterItem(Item item);
    }
}