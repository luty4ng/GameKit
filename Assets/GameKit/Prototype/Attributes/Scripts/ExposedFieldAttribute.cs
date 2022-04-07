using System.Collections;
using System.Collections.Generic;
[System.AttributeUsage(System.AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
public class ExposedFieldAttribute : System.Attribute
{
    // See the attribute guidelines at
    //  http://go.microsoft.com/fwlink/?LinkId=85236
    readonly string positionalString;

    // This is a positional argument
    public ExposedFieldAttribute()
    {
        // this.positionalString = positionalString;

        
        // TODO: Implement code here
        // throw new System.NotImplementedException();
    }
    
    public string PositionalString
    {
        get { return positionalString; }
    }
    
    // This is a named argument
    public int NamedInt { get; set; }
}