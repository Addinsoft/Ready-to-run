
namespace XLSTAT.Models
{
    /// <summary>
    /// Data object represents a dataset
    /// </summary>
    public class Data<T>
    {
        public string[] Labels { get; set; }        /*Label vector*/
        public T[][] Table { get; set; }            /*Data matrix*/
    }
}
