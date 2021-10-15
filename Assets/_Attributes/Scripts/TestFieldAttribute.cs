[System.AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = true)]
sealed class TestFieldAttribute : System.Attribute
{
    // See the attribute guidelines at
    //  http://go.microsoft.com/fwlink/?LinkId=85236
    readonly string _attrId;
    
    // This is a positional argument
    public TestFieldAttribute(string attrId)
    {
        this._attrId = attrId;
        
        // TODO: Implement code here
        throw new System.NotImplementedException();
    }
    
    public string attrId
    {
        get { return attrId; }
    }
    
    // This is a named argument
    public int NamedInt { get; set; }
}