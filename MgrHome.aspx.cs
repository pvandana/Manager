using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPF.Utilities;
using EPF.DataAccess;
using System.Data;

//------------------------------------------------------------------------------
//                                Manager.aspx.cs
//
//      This class is the code behind for the Manager Home screen.
// 
//------------------------------------------------------------------------------
// 
//                          Modification Control Log                           
//                                                                             
//    Date     By                 Description                                
//  --------  ---  -------------------------------------------------------------
//  05-16-17  VP  Initial creation of program                               
// 
//------------------------------------------------------------------------------

namespace EPF.CAW.Users
{
    public partial class Manager : System.Web.UI.Page
    {
        #region Member Variables

        // Module Level Variables
        HRSCLogsDA _HRSCLogsDA = null;

        #endregion

        /// <summary>
        /// Page Load
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Initialize the Logging Class
            _HRSCLogsDA = new HRSCLogsDA();

            //Check if 1st time
            if (!IsPostBack)
            {
                // Log the action
                _HRSCLogsDA.Insert("Manager Home Screen");

                // Display Screen Name on the Master Page
                Label lblScreenName = (Label)Master.FindControl("lblScreenName");
                string screenName = "Corrective Action - Manager Home";

                if (lblScreenName != null)
                {
                    lblScreenName.Text = screenName;
                    Title = screenName;
                }

                string emplId = HttpContext.Current.Session["EmplId"].ToString();
                //string TM_Mgr = HttpContext.Current.Session["TM_Mgr"].ToString();
            } 
        }

        protected void btnSearchTM_Click(object sender, EventArgs e)
        {
            if (Session["HasManagerRole"].ToString().ToLower() == "true")
            {
                btnSearchTM.OnClientClick = "javascript:openPopUp('/CAW/MgrCASearch.aspx?searchType=MgrSearch', 'CAWSearch', 950, 700);";
            }
        }

　
        /// <summary>
        /// gvDirectReportCA - RowCreated
        /// </summary>
        protected void gvDirectReportCA_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                // Process Header
                //if (e.Row.RowType == DataControlRowType.Header)
                //{
                //    // Create a New Record button
                //    Button btnNew = new Button();
                //    btnNew.ID = "btnNew";
                //    btnNew.Text = "Add";
                //    btnNew.Command += new CommandEventHandler(btnNew_Click);
                //    btnNew.CausesValidation = false;
                //    e.Row.Cells[0].Controls.Add(btnNew);
                //}

                // Process DataRow
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.DataItem != null)
                    {
                        DataRowView drv = (DataRowView)e.Row.DataItem;

                        // Check for all the rows in the grid
                        for (int i = 0; i <= e.Row.RowIndex; i++)
                        {
                            // Check for Edit Button
                            //if (((Button)e.Row.Cells[6].Controls[0]).Text == "Edit")
                            //{
                                // Hide the Edit Button
                                //((Button)e.Row.Cells[6].Controls[0]).Style["display"] = "None";

                                if (Session["Status"] != null)
                                {
                                    if (Session["Status"].ToString().ToUpper() == "WIP")
                                    {
                                        // Change the Text on the Action Button to Pending Manager TM Discussion
                                        ((Button)e.Row.Cells[6].Controls[0]).Text = "Pnd Mgr TM Disc";
                                    }

                                    if (Session["Status"].ToString().ToUpper() == "PNDTMDISC")
                                    {
                                        // Change the Text on the Action Button to Pending TM Ack
                                        ((Button)e.Row.Cells[6].Controls[0]).Text = "Pnd TM Ack";
                                    }

                                    if (Session["Status"].ToString().ToUpper() == "PNDTMACK")
                                    {
                                        // Hide the Button
                                        ((Button)e.Row.Cells[6].Controls[0]).Style["display"] = "None";
                                    }

                                    if (Session["Status"].ToString().ToUpper() == "TMLOA")
                                    {
                                        // Hide the Button
                                        ((Button)e.Row.Cells[6].Controls[0]).Style["display"] = "None";
                                    }
                                }
                            //}
                        }

                    }

                    //// Check for Edit Button
                    //if (((Button)e.Row.Cells[6].Controls[0]).Text == "Edit")
                    //{
                    //    // Hide the Edit Button
                    //    ((Button)e.Row.Cells[6].Controls[0]).Style["display"] = "None";
                    //}
                }
            }

            catch (InternalException err)
            {
                // Display a Messagebox
                AlertMessage.Show(err.UserFriendlyMsg, this.Page);
            }
            catch (Exception err)
            {
                // Error
                string errMsg = string.Format("{0} - gvDirectReportCA Row Created Error - {1}",
                                              GetType().FullName,
                                              err.Message);

                // Log the Error 
                AppLogWrapper.LogError(errMsg);

                // Save a User Friendly Error Message in Session Variable
                errMsg = "There was a problem creating a row in the CA grid for Direct report TMs.  If the problem persists, please contact Technical Support.";

                // Display a Messagebox
                AlertMessage.Show(errMsg, this.Page);
            }
        }

　
        /// <summary>
        /// gvDirectReportCA - OnRowDataBound   
        /// </summary>
        protected void gvDirectReportCA_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            const string WIPText = "Work in Progress";
            const string PndTMDiscText = "Pnd Mgr TM Disc";
            const string PndTMAckText = "Pnd TM Ack";
            //const string CancelText = "Cancel";

            try
            {
                // Process DataRow
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int idx = Convert.ToInt32(e.Row.RowIndex);
                    string statusCode = gvDirectReportCA.DataKeys[idx].Values["Status_Code"].ToString();

                    //Button btnCancel = (Button)e.Row.Cells[6].Controls[0];
                    Button btnCancel = (Button)e.Row.Cells[6].FindControl("cancel");
                    btnCancel.Visible = false;
                    Button btnPndTMDisc = (Button)e.Row.Cells[6].FindControl("pndTMDisc");
                    btnPndTMDisc.Visible = false;
                    Button btnPndTMAck = (Button)e.Row.Cells[6].FindControl("pndTMAck");
                    btnPndTMAck.Visible = false;

                    DataRowView drv = (DataRowView)e.Row.DataItem;

                    if (statusCode.ToString().ToUpper() == "WIP")
                    {                        
                        btnPndTMDisc.Visible = true;
                    }

                    if (drv.Row.ItemArray[5].ToString() == WIPText)
                    {
                        btnPndTMDisc.Visible = true;
                    }

                    if (drv.Row.ItemArray[5].ToString() == PndTMDiscText)
                    {
                        btnPndTMDisc.Visible = true;
                    }

                    // Check for Pnd TM Discussion Button
                    if (btnPndTMDisc.Text == PndTMDiscText)
                    {
                        btnPndTMDisc.Attributes["onclick"] = "javascript:if (! confirm('Are you sure you want to change the status. '))" +
                                                          "{return false;}";
                        btnPndTMDisc.Visible = false;
                        btnPndTMAck.Visible = true;
                    }

                    // Check for Pnd TM Ack Button
                    if (btnPndTMAck.Text == PndTMAckText)
                    {
                        // Add a confirmation                      
                        //btnPndTMAck.Attributes["onclick"] = "javascript:if (! confirm('Are you sure you want to change the status. '))" +
                        //                                  "{return false;}";

                        if (btnPndTMAck.Attributes["onclick"].Equals(true))
                            btnPndTMAck.Visible = false;

                    }

                    // Check if row is NOT in Edit mode
                    if (e.Row.RowState != DataControlRowState.Edit
                        && e.Row.RowState != (DataControlRowState.Edit | DataControlRowState.Alternate))
                    {
                        // Change the Background Color of the row during Hover
                        GridFormatting.GridViewStyle(e);

                        // Set OnClick event to go into Select Mode
                        e.Row.Attributes.Add("onClick", Page.ClientScript.GetPostBackClientHyperlink(gvDirectReportCA, "Select$" + e.Row.RowIndex));
                    }
                }
            }
            catch (InternalException err)
            {
                // Display a Messagebox
                AlertMessage.Show(err.UserFriendlyMsg, this.Page);
            }
            catch (Exception err)
            {
                // Error
                string errMsg = string.Format("{0} - gvDirectReportCA Row Data Bound Error - {1}",
                                              GetType().FullName,
                                              err.Message);

                // Log the Error 
                AppLogWrapper.LogError(errMsg);

                // Save a User Friendly Error Message in Session Variable
                errMsg = "There was a problem creating a row in the CA for DirectReport TM grid.  If the problem persists, please contact Technical Support.";

                // Display a Messagebox
                AlertMessage.Show(errMsg, this.Page);
            }
        }

　
        /// <summary>
        /// gvDirectReportCA - RowCommand
        /// </summary>
        protected void gvDirectReportCA_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    int idx = Convert.ToInt32(e.CommandArgument);
                    string statusCode = gvDirectReportCA.DataKeys[idx].Values["Status_Code"].ToString();

                    switch (statusCode.ToUpper())
                    {
                        // Display the Modify CA screen when in the following status

                        case "WIP": Session["Status"] = "WIP";
                            Response.Redirect("CreateCA.aspx");
                            break;

                        case "TMLOA": Session["Status"] = "TMLOA";
                            Response.Redirect("CreateCA.aspx");
                            break;

                        case "PNDTMDISC": Session["Status"] = "PNDTMDISC";
                            //(GridView)sender).Page.ClientScript.RegisterStartupScript(((GridView)sender).Page.GetType(), "OpenScreen", "openScreen('/CAW/CreateCA.aspx');", true);
                            Response.Redirect("CreateCA.aspx");
                            break;
                        //using response direct in a switch stmt is causing issue as it is getting redirected to a different page and then we give a break. so it is giving an exception
                        // when using it in gridview control using openscreen it gives JS error                                             

                        // Merge data with template and rendition to PDF and then show dialog for opening/saving PDF
                        case "PNDTMACK": Session["Status"] = "PNDTMACK";
                            AlertMessage.Show(string.Format("Show PDF"), this.Page);
                            break;
                    }
                }
            }

            catch (InternalException err)
            {
                // Display a Messagebox
                AlertMessage.Show(err.UserFriendlyMsg, this.Page);
            }

            catch (Exception err)
            {
                // Error
                string errMsg = string.Format("{0} - gvDirectReportCA RowCommand Error - {1}",
                                              GetType().FullName,
                                              err.Message);

                // Log the Error 
                AppLogWrapper.LogError(errMsg);

                // Save a User Friendly Error Message in Session Variable
                errMsg = "There was a problem creating a row in the Direct report TMs CA grid.  If the problem persists, please contact Technical Support.";

                // Display a Messagebox
                AlertMessage.Show(errMsg, this.Page);
            }
        }       

　
        /// <summary>
        /// gvDirectReportCA - SelectedIndexChanged   
        /// </summary>
        protected void gvDirectReportCA_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Selected Row
                if (gvDirectReportCA.SelectedValue != null)
                {
                    string tmEmplId = gvDirectReportCA.SelectedValue.ToString();

                    //  Log the Action
                    string logMsg = string.Format("Manager Home screen - Row Selected - EmplId: {0}",
                                                  tmEmplId);

                    _HRSCLogsDA.Insert(logMsg);              
                   
                }
            }

            catch (InternalException err)
            {
                // Display a Messagebox
                //AlertMessage.Show(err.UserFriendlyMsg, this.Page);
                string errMsg = err.Message;
            }

            catch (Exception err)
            {
                // Database Error
                string errMsg = string.Format("{0} - gvDirectReportCA Selected Index Changed Error - {1}",
                                              GetType().FullName,
                                              err.Message);
                // Log the Error 
                AppLogWrapper.LogError(errMsg);

                // Display a User Friendly Error Message 
                errMsg = "There was a problem selecting a record on the Manager Home screen.  If the problem persists, please contact Technical Support.";

                // Display a Messagebox
                AlertMessage.Show(errMsg, this.Page);
            }
        }

　
        /// <summary>
        /// sdsDirectReportCA - Selected   
        /// </summary>
        protected void sdsDirectReportCA_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            // Check for an exception
            if (e.Exception != null
                && e.Exception.InnerException != null)
            {
                Exception inner = e.Exception.InnerException;
                string errMsg = string.Empty;

                // Check for a previously handled exception
                if (inner is InternalException)
                {
                    // Already Logged
                }
                else
                {
                    // Error
                    errMsg = string.Format("{0} - sdsDirectReportCA Selected Error - {1}",
                                           GetType().FullName,
                                           inner.Message);

                    // Log the Error 
                    AppLogWrapper.LogError(errMsg);
                }

                // Indicate that the exception has been handled
                e.ExceptionHandled = true;
            }
        }  
        
    }
}
