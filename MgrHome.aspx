
<%@ Page Title="Manager Home" 
         Language="C#" 
         MasterPageFile="~/MasterPages/SiteMgrHeader.Master"
         AutoEventWireup="true" 
         CodeBehind="Manager.aspx.cs" 
         Inherits="EPF.CAW.Users.Manager" %> 

 <asp:Content ID="Content1" 
              ContentPlaceHolderID="HeadContent"
              runat="server">
       <link href="../Styles/Grid.css" 
             rel="stylesheet" 
             type="text/css" />
       <link href="../Styles/Tabs.css" 
             rel="stylesheet" 
             type="text/css" />
    <script  language="JavaScript"
             type="text/javascript" 
             src="../Scripts/GridView.js">
    </script>     
 </asp:Content>

  <asp:Content ID="Content2"
             ContentPlaceHolderID="MainContent"
             runat="server">

<%--    <asp:HiddenField ID="hdnMgrEmplId" 
                    runat="server" 
                    ClientIDMode="Static" />--%>

  <%--        Displays the Headings  --%>
<%--    <div id="ContentTable">
        &nbsp;
        <h2 align="left">
            Corrective Action - Manager Home
        </h2>
    </div>--%>     
 <%--    
        <div class="page">
            <div class="main">
                <br />--%>
                <table cellpadding="0" 
                       cellspacing="0" 
                       width="99%">    
                    <tr>
      <%--                  <td align="left" style="width:69%">
                            Corrective Action for:                         
                            &nbsp;&nbsp;
                            
                            <asp:ObjectDataSource ID="odsDirectReports" 
                                                  runat="server"
                                                  OnSelected="odsDirectReports_Selected"
                                                  SelectMethod="GetMgrStaff"
                                                  TypeName="EPF.DataAccess.HRFullRosterDA">
                                <SelectParameters>
                                    <asp:SessionParameter Name="mgrId" 
                                                          SessionField="EmplId"
                                                          Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>

                            <asp:DropDownList ID="ddlDirectReports" 
                                              runat="server"
                                              AutoPostBack="True"
                                              AppendDataBoundItems="True"
                                              DataSourceID="odsDirectReports"
                                              DataTextField="Name"
                                              DataValueField="EmplId" 
                                              OnSelectedIndexChanged="ddlDirectReports_SelectedIndexChanged">
                                <asp:ListItem Text=" - Select a Team Member - " Value="" />
                            </asp:DropDownList>                           
                             &nbsp;&nbsp;&nbsp;&nbsp;
                            
                             <asp:Button ID="btnCreateCA" 
                                        runat="server" 
                                        Text="Create a Corrective Action Document"                                        
                                        onclick="btnCreateCA_Click"
                                        Enabled="false" />--%>

                              <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                             <%--</td>--%>
                            <br /> 
                            <td align="left">
                            <asp:Button ID="btnSearchTM" 
                                        runat="server" 
                                        Text="Search for Team Member" 
                                        onclick="btnSearchTM_Click" />                            
                            </td>
                    </tr>                   
                </table> 
                <br /><br />

            <asp:SqlDataSource ID="sdsDirectReportCA"
                           runat="server"
                           CancelSelectOnNullParameter="False"
                           ConnectionString="<%$ ConnectionStrings:CAW %>"                            
                           SelectCommand="Get_DirectReports_TM_CA"
                           OnSelected="sdsDirectReportCA_Selected"
                           SelectCommandType="StoredProcedure">                                                 
                          
         <SelectParameters>            
            <asp:SessionParameter Name="Mgr_EmplID" 
                                  SessionField="EmplId" 
                                  Type="String" /> 
                                  
         <%--     <asp:ControlParameter ControlID="hdnMgrEmplId"                                    
                                    Name="Mgr_EmplID"
                                    PropertyName="Value" 
                                    Type="String" /> --%>   
                                                    
         </SelectParameters>
       </asp:SqlDataSource>

       <asp:GridView ID="gvDirectReportCA" 
                        runat="server" 
                        AllowPaging="True" 
                        AllowSorting="True" 
                        AutoGenerateColumns="False"
                        CellPadding="4" 
                        ClientIDMode="Static"
                        DataKeyNames="Mgr_EmplID, Status_Code"
                        DataSourceID="sdsDirectReportCA"
                        EmptyDataText="No Data Found"
                        OnRowDataBound="gvDirectReportCA_RowDataBound"
                        OnRowCreated="gvDirectReportCA_RowCreated"
                        OnRowCommand="gvDirectReportCA_RowCommand"
                        OnSelectedIndexChanged="gvDirectReportCA_SelectedIndexChanged"
                        PagerSettings-Mode="NumericFirstLast"
                        PageSize="20"
                        ShowHeaderWhenEmpty="True">

       <%--     GridView Columns     --%>
            <Columns>

                <%--    Team Member Name     --%>
                <asp:BoundField DataField="Name"
                                HeaderText="Team Member" 
                                HeaderStyle-Width="150px" 
                                ItemStyle-Wrap="False" 
                                ReadOnly="True" 
                                SortExpression="Name" />
                
                <%--     Employee Id     --%>
                <asp:BoundField DataField="EmplId"
                                HeaderText="Employee ID" 
                                HeaderStyle-Width="100px" 
                                ItemStyle-Wrap="False" 
                                ReadOnly="True" 
                                SortExpression="EmplId" />

                <%--     Manager Employee Id     --%>
<%--                <asp:BoundField DataField="Mgr_EmplId"
                                HeaderText="Mgr Employee ID" 
                                HeaderStyle-Width="100px" 
                                ItemStyle-Wrap="False" 
                                ReadOnly="True" 
                                SortExpression="Mgr_EmplId" 
                                Visible="false" />--%>

                <%--     Template Type     --%>
                <asp:BoundField DataField="Type"
                                HeaderText="Type" 
                                HeaderStyle-Width="150px" 
                                ItemStyle-Wrap="False" 
                                ReadOnly="True"
                                SortExpression="Type" />

                <%--     Template Category     --%>
                <asp:BoundField DataField="Category"
                                HeaderText="Category" 
                                HeaderStyle-Width="150px" 
                                ItemStyle-Wrap="False" 
                                ReadOnly="True"
                                SortExpression="Category" />
                
                <%--     Last Modified     --%>
                <asp:BoundField DataField="Modified_Date"
                                HeaderText="Last Modified"
                                DataFormatString="{0:MM/dd/yyyy}"  
                                HeaderStyle-Width="150px" 
                                ItemStyle-Wrap="False" 
                                ReadOnly="True" 
                                SortExpression="Modified_Date" />

                <%--     Current Status     --%>
                <asp:BoundField DataField="Status_Description"
                                HeaderText="Current Status" 
                                HeaderStyle-Width="150px" 
                                ItemStyle-Wrap="False" 
                                ReadOnly="True"
                                SortExpression="Status_Description" />

                <%--     Next Action   --%>   
       <%--         <asp:CommandField ButtonType="Button"
                                  HeaderText="Next Action"
                                  HeaderStyle-CssClass="gvFixedHeader"                                 
                                  HeaderStyle-Width="250px"
                                  ItemStyle-Wrap="False"                                    
                                  ShowCancelButton="true"                                   
                                  ShowSelectButton="true"                                   
                                  ShowHeader="True" />--%>                                  

　
            <asp:TemplateField HeaderText="Next Action"
                               ControlStyle-Width="150px"
                               ItemStyle-HorizontalAlign="Left"
                               ItemStyle-Wrap="true">

                <ItemTemplate>
                    <asp:Button ID="btnCan" 
                                runat="server" 
                                Text="Cancel" />                    
                </ItemTemplate>

                <ItemTemplate>
                    <asp:Button ID="btnAction" 
                                runat="server" 
                                Text="Action" />                    
                </ItemTemplate>       

        </asp:TemplateField>

                <%--     Select Button    --%>
                <asp:CommandField ButtonType="Button"
                                  HeaderStyle-CssClass="hideColumn"
                                  ItemStyle-CssClass="hideColumn"
                                  ShowSelectButton="True" />
            </Columns>

            <%--     GridView Styles     --%>
            <AlternatingRowStyle CssClass="gvAlternatingRow" />                    
            <EditRowStyle CssClass="gvEditRow" />
            <FooterStyle CssClass="gvFooter" />
            <HeaderStyle CssClass="gvHeader" />
            <PagerStyle CssClass="gvPager" />

       </asp:GridView>

       <br /><br /><br />

       <table>
        <tr>
            <td>
                <b>Corrective Action Statistics:</b>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <b>In Progress: </b>
                <asp:Label ID="lblInProgress" 
                           runat="server" 
                           Text="">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <b>Completed: </b>
                <asp:Label ID="lblCompleted" 
                           runat="server" 
                           Text="">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td style="font-size:smaller">
               Note: Statistics represents a 12 month count for three levels down in the reporting structure.
            </td>
        </tr>
       </table>
<%--
            </div>
         </div>--%>
         <br /><br />          

  </asp:Content>

　
　
