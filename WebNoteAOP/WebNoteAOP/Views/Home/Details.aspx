<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<WebNoteAOP.Models.NoteWithCategories>" %>
<%@ Import Namespace="WebNoteAOP.Code.HtmlHelperExtensions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Details of note number <%: Model.NoteId %></h1>

        <div class="drop_shadow bigpostit <%= Html.SelectColorClass(Model.Categories) %>">

            <fieldset>
        
                <div class="display-label rowLeft">Title:</div>
                <div class="display-field rowRight"><%: Model.Title %></div>

                <hr class="clear" />
        
                <div class="display-label rowLeft">Message:</div>
                <div class="display-field rowRight"><%= Html.EncodeLineBreaks(Model.Message) %></div>
        
                <hr class="clear" />

                <div class="display-label rowLeft">Added:</div>
                <div class="display-field rowRight"><%: String.Format("{0:g}", Model.Added) %></div>
        
                <hr class="clear" />

                <div class="display-label rowLeft">Categories:</div>
                <div class="display-field rowRight">
                    <ul>
                    <% foreach (var category in Model.Categories) { %>
                        <li style="color:<%: category.Color %>"><%: category.Name %></li>
                     <% } %>
                     </ul>
                </div>

            </fieldset>

        </div>
    <p>
        <%: Html.ActionLink("« Back to List", "Index")%> &nbsp;|&nbsp;
        <%: Html.ActionLink("Edit", "Edit", new { id=Model.NoteId }) %>
    </p>

</asp:Content>

