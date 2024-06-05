namespace FortniteHelper.Models
{
    public class ShopModel
    {
        public DataShop data { get; set; }
    }
    public class DataShop
    {
        public Featured featured { get; set; }
        public SpecialFeatured specialFeatured { get; set; }
        public Daily daily { get; set; }
        public SpecialDaily specialDaily { get; set; }
        public Votes votes { get; set; }
        public VoteWinners voteWinners { get; set; }
    }
    public class VoteWinners
    {
        public List<Entries> entries { get; set; }
    }
    public class Votes
    {
        public List<Entries> entries { get; set; }
    }
    public class SpecialFeatured
    {
        public List<Entries> entries { get; set; }
    }
    public class SpecialDaily
    {
        public List<Entries> entries { get; set; }
    }
    public class Daily
    {
        public List<Entries> entries { get; set; }
    }
    public class Featured
    {
        public List<Entries> entries { get; set; }
    }
    public class Entries
    {
        public int regularPrice { get; set; }
        public int finalPrice { get; set; }
        public Bundle bundle { get; set; }
        public List<ShopItem> items { get; set; }
        public NewDisplayAsset newDisplayAsset { get; set; }
    }
    public class NewDisplayAsset
    {
        public List<MaterialInstances> materialInstances { get; set; }
    }
    public class MaterialInstances
    {
        public Dictionary<string, string> images { get; set; }
    }

    public class ShopItem
    {
        public string name { get; set; }
        public string description { get; set; }
        public Type type { get; set; }
        public Rarity rarity { get; set; }
        public Set set { get; set; }
        public Introduction introduction { get; set; }
        public ImagesShop images { get; set; }
    }
    public class ImagesShop
    {
        public string featured { get; set; }
        public string icon { get; set; }
    }
    public class Introduction
    {
        public string chapter { get; set; }
        public string season { get; set; }
        public string text { get; set; }
    }
    public class Set
    {
        public string text { get; set; }
    }
    public class Type
    {
        public string displayValue { get; set; }
    }
    public class Rarity
    {
        public string displayValue { get; set; }
    }
    public class Bundle
    {
        public string name { get; set; }
        public string image { get; set; }
    }
}
