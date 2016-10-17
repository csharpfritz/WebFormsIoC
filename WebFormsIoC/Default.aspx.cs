using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormsIoC
{
  public partial class _Default : Page
  {

    public _Default(Models.TestRepository repo)
    {

      Debug.WriteLine("Logging from the injected constructor!");

    }

    protected _Default() { }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
  }

}