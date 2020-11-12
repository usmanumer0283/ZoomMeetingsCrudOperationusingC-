using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;

namespace WebForm.Models
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        ZoomFunctions zoomFunctions = new ZoomFunctions();
        public string Hdata, hmeetingId, htopic, hagenda, hstart_time, hduration;

        DataTable dt = new DataTable();
        StringBuilder table = new StringBuilder();
        DataRow dr;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Meeting meeting = new Meeting();
                meeting.ApiSecrate = this.TextApiSecrate.Text;
                meeting.jwtToken = this.TextjwtToken.Text;
                meeting.ApiKey = this.TextApiKey.Text;
                meeting.end_time = this.TextEndingTime.Text;
                meeting.start_time = this.TextStartingTime.Text;
                meeting.agenda = this.TextAgenda.Text;
                meeting.duration = this.TextDuration.Text;
                meeting.topic = this.TextTopic.Text;

                zoomFunctions.CreateMeeting(meeting);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                Meeting meeting = new Meeting();
                meeting.ApiSecrate = this.TextApiSecrate.Text;
                meeting.jwtToken = this.TextjwtToken.Text;
                meeting.ApiKey = this.TextApiKey.Text;
                meeting.MeetingId = this.TextMeetingId.Text;
                zoomFunctions.DeleteMeetings(meeting);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                MeetingData();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void MeetingData()
        {
            Meeting meeting = new Meeting();
            meeting.ApiSecrate = this.TextApiSecrate.Text;
            meeting.jwtToken = this.TextjwtToken.Text;
            meeting.ApiKey = this.TextApiKey.Text;

            string data = zoomFunctions.GetMeetings(meeting);
            string replace = data.Replace("[", string.Empty);
            string replace1 = replace.Replace("]", string.Empty);
            string Curlybraket = replace1.Replace("{", string.Empty);

            string[] separatingStrings = { "}," };
            string[] strlist = Curlybraket.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
            dt.Columns.AddRange(new DataColumn[9] { 
                new DataColumn("UserId",typeof(string)),
                new DataColumn("MeetingID",typeof(string)),
                new DataColumn("Host Id",typeof(string)),
                new DataColumn("Topic", typeof(string)),
                new DataColumn("Duration",typeof(string)),
                new DataColumn("JoiningURl",typeof(string)),
                new DataColumn("StartTime",typeof(string)),
                new DataColumn("Agenda", typeof(string)),
                new DataColumn("Type",typeof(string))});
            for (int i = 0; i < strlist.Length; i++)
            {
                string[] newsplit = strlist[i].Split(',');

                //for (int j = 0; j <= newsplit.Length - 1; j++)
                //{
                //    string separator = ":";
                //    int separatorIndex = newsplit[j].IndexOf(separator);
                //    if (separatorIndex >= 0 && j == 0)
                //    {
                //        dt.Rows.Add(newsplit[0].Substring(separatorIndex + separator.Length));
                //    }
                //    if (separatorIndex >= 0 && j == 1)
                //    {
                //        dt.Rows.Add(newsplit[1].Substring(separatorIndex + separator.Length));
                //    }
                //}
                dt.Rows.Add(newsplit[0], newsplit[1], newsplit[2], newsplit[3], newsplit[6], newsplit[10], newsplit[5], newsplit[8], newsplit[4]);

            }
        
            StringBuilder sb = new StringBuilder();
                sb.Append("<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:Arial'>");
                sb.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + column.ColumnName + "</th>");
                }
                sb.Append("</tr>");
                foreach (DataRow row in dt.Rows)
                {
                 
                    sb.Append("<tr>");
                    foreach (DataColumn column in dt.Columns)
                    {
                        sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + row[column.ColumnName].ToString() + "</td>");
                    }
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
                ltTable.Text = sb.ToString();
        }
    }
}











//JArray textArray = JArray.Parse(ob);
//List<string> arr = new List<string>();
//            for (int i = 0; i<textArray.Count; i++)
//            {
//                arr.Add(textArray[i]["MeetingId"].Value<string>());
//                arr.Add(textArray[i]["topic"].Value<string>());
//                arr.Add(textArray[i]["agenda"].Value<string>());
//                arr.Add(textArray[i]["start_time"].Value<string>());
//                arr.Add(textArray[i]["duration"].Value<string>());


//            }





//JArray textArray = JArray.Parse(ob);
//List<string> tempArry = new List<string>();
//for (int i = 0; i < textArray.Count; i++)
//{
//    this.hmeetingId = textArray[i]["MeetingId"].Value<string>();
//    this.htopic = textArray[i]["topic"].Value<string>();
//    this.hagenda = textArray[i]["agenda"].Value<string>();
//    this.hstart_time = textArray[i]["start_time"].Value<string>();
//    this.hduration = textArray[i]["duration"].Value<string>();


//}

//JArray textArray = JArray.Parse(value);
//            for (int i = 0; i<textArray.Count; i++)
//            {
//                this.hmeetingId = textArray[i]["MeetingId"].Value<string>();
//                this.htopic = textArray[i]["topic"].Value<string>();
//                this.hagenda = textArray[i]["agenda"].Value<string>();
//                this.hstart_time = textArray[i]["start_time"].Value<string>();
//                this.hduration = textArray[i]["duration"].Value<string>();
//                for(int j= i; j<i; j++)
//                {
//    this.hmeetingId = textArray[j]["MeetingId"].Value<string>();
//    this.htopic = textArray[j]["topic"].Value<string>();
//    this.hagenda = textArray[j]["agenda"].Value<string>();
//    this.hstart_time = textArray[j]["start_time"].Value<string>();
//    this.hduration = textArray[j]["duration"].Value<string>();
//}
//}


               
                //    dt.Columns.Add("UserId", typeof(string));
                //    dt.Columns.Add("MeetingId", typeof(string));
                //    dt.Columns.Add("Host Id", typeof(string));
                //    dt.Columns.Add("Topic", typeof(string));
                //    dt.Columns.Add("Duration", typeof(string));
                //    dt.Columns.Add("JoiningURl", typeof(string));
                //    dr = dt.NewRow();
                //for (int i = 0; i <= strlist.Length; i++)
                //{
                //    dr["UserId"] = newsplit[i];
                //    dr["MeetingId"] = newsplit[i + 1];
                //    dr["Host Id"] = newsplit[i + 2];
                //    dr["Topic"] = newsplit[i + 3];
                //    dr["Duration"] = newsplit[i + 6];
                //    dr["JoiningURl"] = newsplit[i + 10];
                //    if (strlist.Length<1)
                //    { 
                //        for(int j=i; j<i; j++)
                //        { 
                //            dt.Rows.Add(dr);
                //            GridView1.DataSource = dt;
                //            GridView1.DataBind();
                //            ViewState["table1"] = dt;
                //        }
                //    }
                //    dt.Rows.Add(dr);
                //    GridView1.DataSource = dt;
                //    GridView1.DataBind();
                //    ViewState["table1"] = dt;


//                    for (int i = 0; i <=strlist.Length; i++)
//                    {
//                        for (int j = 0; j <= i; j++)
//                        {
//                            dt.Columns.AddRange(new DataColumn[7] { new DataColumn("UserId",typeof(string)),
//                            new DataColumn("MeetingID",typeof(string)),
//                            new DataColumn("Host Id",typeof(string)),
//                            new DataColumn("Topic", typeof(string)),
//                            new DataColumn("Duration",typeof(string)),
//                            new DataColumn("JoiningURl",typeof(string)),
//                            new DataColumn("StartTime",typeof(string))});
                       
//                            dt.Rows.Add(newsplit[i], newsplit[i + 1], newsplit[i + 2], newsplit[i + 3], newsplit[i + 6], newsplit[i + 10]);
//                        }
//                    }
                       
//                        StringBuilder sb = new StringBuilder();
//sb.Append("<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:Arial'>");
//                        sb.Append("<tr>");
//                        foreach (DataColumn column in dt.Columns)
//                        {
//                            sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + column.ColumnName + "</th>");
//                        }
//                        sb.Append("</tr>");
//                        foreach (DataRow row in dt.Rows)
//                        {
//                            sb.Append("<tr>");
//                            foreach (DataColumn column in dt.Columns)
//                            {
//                                sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + row[column.ColumnName].ToString() + "</td>");
//                            }
//                            sb.Append("</tr>");
//                        }
//                        sb.Append("</table>");
//                        ltTable.Text = sb.ToString();

    //table.Append("<table>");
    //                table.Append("<tr>");
    //                table.Append("<th>");
    //                table.Append(dt.Columns.Add("UserId", Type.GetType("System.String")));
    //                table.Append("</th>");
    //                table.Append("<th>");
    //                table.Append(dt.Columns.Add("MeetingId", Type.GetType("System.String")));
    //                table.Append("</th>");
    //                table.Append("<th>");
    //                table.Append(dt.Columns.Add("HostId", Type.GetType("System.String")));
    //                table.Append("</th>");
    //                table.Append("<th>");
    //                table.Append(dt.Columns.Add("Duration ", Type.GetType("System.String")));
    //                table.Append("</th>");
    //                table.Append("<th>");
    //                table.Append(dt.Columns.Add("Topic", Type.GetType("System.String")));
    //                table.Append("</th>");
    //                table.Append("</tr>");

    //                table.Append("</table>");
    //                for (int i = 0; i<a.Length; i++)
    //                {
    //                    table.Append("<tr>");
    //                    dt.Rows.Add();
    //                    table.Append("<td>");
    //                    table.Append(dt.Rows[dt.Rows.Count - 1]["UserdId"]= newsplit[i]);
    //                    table.Append("</td>");

    //                    table.Append("<td>");
    //                    table.Append(dt.Rows[dt.Rows.Count - 1]["MeetingId"] = newsplit[i]);
    //                    table.Append("</td>");
    //                    table.Append("<td>");
    //                    table.Append(dt.Rows[dt.Rows.Count - 1]["HostId"] = newsplit[i]);
    //                    table.Append("</td>");
    //                    table.Append("<td>");
    //                    table.Append(dt.Rows[dt.Rows.Count - 1]["Topic"] = newsplit[i]);
    //                    table.Append("</td>");
    //                    table.Append("<td>");
    //                    table.Append(dt.Rows[dt.Rows.Count - 1]["Duration"] = newsplit[i]);
    //                    table.Append("</td>");
    //                    table.Append("</tr>");