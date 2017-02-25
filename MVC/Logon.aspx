<%@ Page language="c#" AutoEventWireup="true" %>
<%@ Import Namespace="MVC.AdServices" %>
<html>
  <body>
    <form id="Login" method="post" runat="server">
      <asp:Label ID="Label1" Runat=server >Domain:</asp:Label>
      <asp:TextBox ID="txtDomain" Runat=server ></asp:TextBox><br>    
      <asp:Label ID="Label2" Runat=server >Username:</asp:Label>
      <asp:TextBox ID=txtUsername Runat=server ></asp:TextBox><br>
      <asp:Label ID="Label3" Runat=server >Password:</asp:Label>
      <asp:TextBox ID="txtPassword" Runat=server TextMode=Password></asp:TextBox><br>
      <asp:Button ID="btnLogin" Runat=server Text="Login" OnClick="Login_Click"></asp:Button><br>
      <asp:Label ID="errorLabel" Runat=server ForeColor=#ff3300></asp:Label><br>
         <asp:Label ID="groupsLab" Runat=server ForeColor=#0000ff></asp:Label><br>
      <asp:CheckBox ID=chkPersist Runat=server Text="Persist Cookie" />
    </form>
  </body>
</html>
<script runat=server>
    void Login_Click(Object sender, EventArgs e)
    {

        String adPath = "LDAP://win-adds.devstorm.tn"; //Fully-qualified Domain Name
        Authentification auth = new Authentification( txtUsername.Text, txtPassword.Text,adPath);
        //LdapAuthentication adAuth = new LdapAuthentication(adPath,errorLabel);
        try
        {
            if(true == auth.IsAuthenticated())
            {
                //String groups = auth.GetGroups(txtDomain.Text, txtUsername.Text, txtPassword.Text);

                ////Create the ticket, and add the groups.
                //bool isCookiePersistent = chkPersist.Checked;
                //FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1,  txtUsername.Text,
                //DateTime.Now, DateTime.Now.AddMinutes(60), isCookiePersistent, groups);

                ////Encrypt the ticket.
                //String encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                ////Create a cookie, and then add the encrypted ticket to the cookie as data.
                //HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                //if(true == isCookiePersistent)
                //authCookie.Expires = authTicket.Expiration;

                ////Add the cookie to the outgoing cookies collection.
                //Response.Cookies.Add(authCookie);

                ////You can redirect now.
                //Response.Redirect(FormsAuthentication.GetRedirectUrl(txtUsername.Text, false));

                errorLabel.Text = errorLabel.Text + " | It works  OrganisationUnit : "+auth.GetOrganisationUnit();
                groupsLab.Text = groupsLab.Text + " |  Count :  | " + auth.GetGroups().Count;
                foreach (var item in auth.GetGroups())
                {
                    groupsLab.Text = groupsLab.Text + "groupe : " + item + " | ";
                }
               
            }
            else
            {
                errorLabel.Text = "Authentication did not succeed. Check user name and password.";
            }
        }
        catch(Exception ex)
        {
            errorLabel.Text = "Error authenticating. a" + ex.Message;
        }
    }
</script>