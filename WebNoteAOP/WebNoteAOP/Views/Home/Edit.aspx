<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<WebNoteAOP.Models.NoteWithCategories>" %>
<%@ Import Namespace="WebNoteAOP.Code.HtmlHelperExtensions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Edit note number <%: Model.NoteId %></h1>

    <% if (ViewData["Message"] != null){%><b style="color:red;"><%=ViewData["Message"]%></b><% }%>
    
    <div class="drop_shadow bigpostit  <%= Html.SelectColorClass(Model.Categories) %>">

        <% using (Html.BeginForm()) {%>
            <%: Html.ValidationSummary(true) %>

            <fieldset>
            
                <div style="display:none;">
                    <%: Html.TextBoxFor(model => model.NoteId) %>
                </div>

                <div class="display-label rowLeft">
                    <%: Html.LabelFor(model => model.Title) %>
                </div>
                <div class="display-field rowRight">
                    <%: Html.TextBoxFor(model => model.Title) %>
                    <%: Html.ValidationMessageFor(model => model.Title) %>
                </div>

                <br style="clear:both;" />
            
                <div class="display-label rowLeft">
                    <%: Html.LabelFor(model => model.Message) %>
                </div>
                <div class="display-field rowRight">
                    <%: Html.TextAreaFor(model => model.Message) %>
                    <%: Html.ValidationMessageFor(model => model.Message) %>
                </div>

                <br style="clear:both;" />

                <div class="display-label rowLeft">Categories:</div>
                <div class="display-field rowRight">
                    <%= Html.ColoredCheckBoxes(Model, ViewData["AllAvailableCategories"], "newCategories") %>
                </div>

                <br style="clear:both;" />
                        
                <p>
                    <input type="submit" value="Save" style="float:right;" />
                </p>

                </fieldset>

        <% } %>

    </div>

    <div>
        <%: Html.ActionLink("« Back to List", "Index")%>
    </div>

</asp:Content>