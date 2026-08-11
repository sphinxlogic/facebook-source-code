<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="Facebook.Web.Mvc.FacebookViewPage<Facebook.Api.User>" %>

<asp:Content ID="h" runat="server" ContentPlaceHolderID="head">
    <script src="/Scripts/asyncForm.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="c" ContentPlaceHolderID="content" runat="server">

    <h1>Fql</h1>
    
    <div class="section">
        <a name="QueryJson"></a>
        <h2>QueryJson</h2>
        <div class="content">
            <form id="queryJson_form" action="/demo/fql/queryJson" method="post">
                <table class="form">
                    <tbody>
                        <tr>
                            <td><label for="queryJson_fqlQuery">FQL Query</label></td>
                            <td><textarea id="queryJson_fqlQuery" name="fqlQuery"></textarea></td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td></td>
                            <td><button type="submit">Run Query</button></td>
                        </tr>
                    </tfoot>
                </table>
            </form>
        </div>
        
        <a name="QueryXml"></a>
        <h2>QueryXml</h2>
        <div class="content">
            <form id="queryXml_form" action="/demo/fql/queryXml" method="post">
                <table class="form">
                    <tbody>
                        <tr>
                            <td><label for="queryXml_fqlQuery">FQL Query</label></td>
                            <td><textarea id="queryXml_fqlQuery" name="fqlQuery"></textarea></td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td></td>
                            <td><button type="submit">Run Query</button></td>
                        </tr>
                    </tfoot>
                </table>
            </form>
        </div>
        
        <a name="Get"></a>
        <h2>Get</h2>
        <div class="content">
          Returns <T> object based on method call
          <br />
          This information was called with the following code
          <br />
            <div style="width: 400px; height: 200px; overflow: scroll;">
                String fqlQuery = String.Format("select uid, first_name, last_name, name, pic_small, pic_big, pic_square, pic, affiliations, profile_update_time, timezone, religion, birthday, sex, hometown_location, meeting_sex, meeting_for, relationship_status, significant_other_id, political, current_location, activities, interests, is_app_user, music, tv, movies, books, quotes, about_me, hs_info, education_history, status, has_added_app, online_presence, locale, proxied_email, profile_url, pic_small_with_logo, pic_big_with_logo, pic_square_with_logo, pic_with_logo, allowed_restrictions, verified FROM user WHERE {0} in (uid);", this.FbContext.Session.Uid)
                <br />
                FacebookResponse&lt;User&gt; result = this.FbContext.Fql.Query&lt;User&gt;(fqlQuery);  
            </div>
          <ul class="kvp-list">
            <li>
                    <label>AboutMe:</label>
                    <div><%=this.Model.AboutMe.ToString()%></div>
            </li>
            <li>
                    <label>Activities:</label>
                    <div><%=this.Model.Activities.ToString()%></div>
            </li>
            <li>
                    <label>Affiliations:</label>
                    <div><% foreach(var affiliation in this.Model.Affiliations) %>
                         <% { %> <%=affiliation.Name%> <br /> <% } %></div>
            </li>
            <li>
                    <label>Birthday:</label>
                    <div><%=this.Model.Birthday.ToString()%></div>
            </li>
            <li>
                    <label>Books:</label>
                    <div><%=this.Model.Books.ToString()%></div>
            </li>
            <li>
                    <label>CurrentLocation:</label>
                    <div><%=this.Model.CurrentLocation.City %>, <%=this.Model.CurrentLocation.State %>, <%=this.Model.CurrentLocation.Country %>, <%=this.Model.CurrentLocation.Zip %></div>
            </li>
            <li>
                    <label>EducationHistory:</label>
                    <div><% foreach(var education in this.Model.EducationHistory )%>
                         <% { %><%= education.Name %> - <%= education.Year %><br /><% } %></div>
            </li>
            <li>
                    <label>FirstName:</label>
                    <div><%=this.Model.FirstName.ToString()%></div>
            </li>
            <li>
                    <label>HometownLocation:</label>
                    <div><%=this.Model.HometownLocation.City %>, <%=this.Model.HometownLocation.State %>, <%=this.Model.HometownLocation.Country %>, <%=this.Model.HometownLocation.Zip %></div>
            </li>
            <li>
                    <label>HsInfo:</label>
                    <div><%=this.Model.HsInfo.Hs1Name %></div>
            </li>
            <li>
                    <label>Interests:</label>
                    <div><%=this.Model.Interests.ToString()%></div>
            </li>
            <li>
                    <label>IsAppUser:</label>
                    <div></div>
            </li>
            <li>
                    <label>LastName:</label>
                    <div><%=this.Model.LastName.ToString()%></div>
            </li>
            <li>
                    <label>MeetingFor:</label>
                    <div><% foreach(String meetingFor in this.Model.MeetingFor) %>
                         <% { %><%= meetingFor%> <% } %></div>
            </li>
            <li>
                    <label>MeetingSex:</label>
                    <div><% foreach(String meetingSex in this.Model.MeetingSex) %>
                         <% { %><%= meetingSex%> <% } %></div>
            </li>
            <li>
                    <label>Movies:</label>
                    <div><%=this.Model.Movies.ToString()%></div>
            </li>
            <li>
                    <label>Music:</label>
                    <div><%=this.Model.Music.ToString()%></div>
            </li>
            <li>
                    <label>Name:</label>
                    <div><%=this.Model.Name.ToString()%></div>
            </li>
            <li>
                    <label>NotesCount:</label>
                    <div><%=this.Model.NotesCount.ToString()%></div>
            </li>
            <li>
                    <label>Pic:</label>
                    <div><%=this.Model.Pic.ToString()%></div>
            </li>
            <li>
                    <label>PicBig:</label>
                    <div><%=this.Model.PicBig.ToString()%></div>
            </li>
            <li>
                    <label>PicSmall:</label>
                    <div><%=this.Model.PicSmall.ToString()%></div>
            </li>
            <li>
                    <label>Political:</label>
                    <div><%=this.Model.Political.ToString()%></div>
            </li>
            <li>
                    <label>ProfileUpdateTime:</label>
                    <div><%=this.Model.ProfileUpdateTime.ToString()%></div>
            </li>
            <li>
                    <label>Quotes:</label>
                    <div><%=this.Model.Quotes.ToString()%></div>
            </li>
            <li>
                    <label>RelationshipStatus:</label>
                    <div><%=this.Model.RelationshipStatus.ToString()%></div>
            </li>
            <li>
                    <label>Religion:</label>
                    <div><%=this.Model.Religion.ToString()%></div>
            </li>
            <li>
                    <label>Sex:</label>
                    <div><%=this.Model.Sex.ToString()%></div>
            </li>
            <li>
                    <label>SignificantOtherId:</label>
                    <div><%=this.Model.SignificantOtherId.ToString()%></div>
            </li>
            <li>
                    <label>Status:</label>
                    <div><%=this.Model.Status.Message%> - <%= this.Model.Status.Time %> -- <%= this.Model.Status.Source %></div>
            </li>
            <li>
                    <label>Timezone:</label>
                    <div><%=this.Model.Timezone.ToString()%></div>
            </li>
            <li>
                    <label>Tv:</label>
                    <div><%=this.Model.Tv.ToString()%></div>
            </li>
            <li>
                    <label>Uid:</label>
                    <div><%=this.Model.Uid.ToString()%></div>
            </li>
            <li>
                    <label>WallCount:</label>
                    <div><%=this.Model.WallCount.ToString()%></div>
            </li>
            <li>
                    <label>PicSquare:</label>
                    <div><%=this.Model.RelationshipStatus.ToString()%></div>
            </li>
            <li>
                    <label>HasAddedApp:</label>
                    <div></div>
            </li>
            <li>
                    <label>Locale:</label>
                    <div><%=this.Model.Locale.ToString()%></div>
            </li>
          </ul>
        </div>
    </div>
        
        

    <script type="text/javascript">
        $('#queryXml_form').asyncForm('script');
        $('#queryJson_form').asyncForm('script');
    </script>
    
</asp:Content>
