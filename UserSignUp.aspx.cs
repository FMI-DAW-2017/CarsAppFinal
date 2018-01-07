using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
    {
        if (!Roles.RoleExists("Admin"))
        {
            Roles.CreateRole("Admin");
        }
        Roles.AddUserToRole(CreateUserWizard1.UserName, "Admin");
    }

    protected void CreateUserWizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {
        try
        {
            Profile.FirstName = TBFirstName.Text;
            Profile.LastName = TBLastName.Text;
            Profile.Birthday = DateTime.Parse(TBBirthday.Text);
            Profile.Gender = int.Parse(RBLGender.SelectedValue);
        }
        catch (Exception ex)
        {
            
        }
    }

    protected void CreateUserWizard1_ContinueButtonClick(object sender, EventArgs e)
    {
        Response.Redirect("Index.aspx");
    }
}