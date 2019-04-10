namespace WebDataReader.Domain.ValueObjects.Transform
{
  public class ColumnMetadata
  {
    public string ColumnName { get; set; }
    public bool AllowDbNull { get; set; }
    public string DataType { get; set; }
    public int ColumnSize { get; set; }
    public int ColumnOrdinal { get; set; }
  }
}