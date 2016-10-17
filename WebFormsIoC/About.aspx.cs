using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormsIoC
{
  public partial class About : Page
  {

    public About(Models.TestRepository repo)
    {
      Debug.WriteLine("In the injectable constructor");
    }

    protected About() { }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
  }
}