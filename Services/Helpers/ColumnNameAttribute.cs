namespace RaveAppAPI.Services.Helpers
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnNameAttribute : Attribute
    {
        public string Name { get; set; }
        public ColumnNameAttribute(string name)
        {
            Name = name.ToUpper();
        }
    }

}
