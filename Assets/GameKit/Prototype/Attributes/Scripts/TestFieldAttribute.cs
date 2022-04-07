
public class TestFieldAttribute : System.Attribute  
{  
    string name;  
    public double version;  
  
    public TestFieldAttribute(string name)  
    {  
        this.name = name;  
  
        // Default value.  
        version = 1.0;  
    }  
  
    public string GetName()  
    {  
        return name;  
    }  
} 