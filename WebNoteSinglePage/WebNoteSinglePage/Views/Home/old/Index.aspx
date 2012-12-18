<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<WebNoteSinglePage.Models.NoteWithCategories>>" %>
<%@ Import Namespace="WebNoteSinglePage.Code.HtmlHelperExtensions" %>
<%@ Import Namespace="WebNoteSinglePage.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Your Notes</h1>
    
    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>

    <% foreach (NoteWithCategories note in Model) { %>
    
    <div class="drop_shadow postit <%= Html.SelectColorClass(note.Categories) %>" >

        <%: Html.ActionLink("x", "Delete", new { id = note.Id }, new { @class = "deleteLink" })%>
        <%= Html.SexyDate(note.Added) %>
               
        <h2><%: note.Title %></h2>

        <p><%= Html.EncodeLineBreaks(note.Message) %></p>

        <%: Html.ActionLink("Edit", "Edit", new { id=note.Id }) %> |
        <%: Html.ActionLink("Details", "Details", new { id = note.Id })%> 

    </div>

    <% } %>

</asp:Content>
