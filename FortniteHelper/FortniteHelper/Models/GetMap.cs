namespace FortniteHelper.Models
{
    public class GetMap
    {
        public int status {  get; set; }
        public Data Data { get; set; }
    }
    public class Data
    {
        public ImagesMap Images { get; set; }
        
    }
    public class ImagesMap
    {
        public string Pois { get; set; } 

    }
}
