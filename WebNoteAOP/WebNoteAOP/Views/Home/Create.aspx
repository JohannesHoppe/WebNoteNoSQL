<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<WebNoteAOP.Models.WebNote.Note>" %>
<%@ Import Namespace="WebNoteAOP.Code.HtmlHelperExtensions" %>
<%@ Import Namespace="WebNoteAOP.Models.WebNoteCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Create a new note</h1>

    <% if (ViewData["Message"] != null){%><b style="color:red;"><%=ViewData["Message"]%></b><% }%>

    <div class="drop_shadow bigpostit">

        <% using (Html.BeginForm()) {%>
            <%: Html.ValidationSummary(true) %>

            <fieldset>
            
                <div class="display-label">
                    <%: Html.LabelFor(model => model.Title) %>
                </div>

                <div class="display-field">
                    <%: Html.TextBoxFor(model => model.Title) %>
                    <%: Html.ValidationMessageFor(model => model.Title) %>
                </div>
            
                <div class="display-label">
                    <%: Html.LabelFor(model => model.Message) %>
                </div>
                <div class="display-field">
                    <%: Html.TextAreaFor(model => model.Message) %>
                    <%: Html.ValidationMessageFor(model => model.Message) %>
                </div>
           
                <br style="clear:both;" />

                <div class="display-label rowLeft">Categories:</div>
                <div class="display-field rowRight">
                    <%= Html.ColoredCheckBoxes(ViewData["AllAvailableCategories"], "newCategories") %>
                </div>

                <br style="clear:both;" />

                <p>
                    <input type="submit" value="Create" style="float:right;" />
                </p>
            </fieldset>

        <% } %>

    </div>

    <div>
        <%: Html.ActionLink("« Back to List", "Index")%>
    </div>

</asp:Content>

