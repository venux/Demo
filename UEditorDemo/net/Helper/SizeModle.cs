using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class SizeModle
{
    private int widthField;
    private int heightField;

    /// <remarks/>
    public int width
    {
        get
        {
            return this.widthField;
        }
        set
        {
            this.widthField = value;
        }
    }

    /// <remarks/>
    public int height
    {
        get
        {
            return this.heightField;
        }
        set
        {
            this.heightField = value;
        }
    }
}
