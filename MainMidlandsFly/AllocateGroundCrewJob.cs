using MainMidlandsFly.Models;

using Microsoft.IdentityModel.Protocols;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MainMidlandsFly
{
    public class AllocateGroundCrewJob : IJob
    {

        static string conn_string = "Server=tcp:m32com.database.windows.net,1433;Initial Catalog=m32com;Persist Security Info=False;User ID=M32comadmin;Password=7ts9tcWPnrCXtCXK;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


        //public  AllocateGroundCrewJob(AircraftMaintenanceContext context)
        //{


        //    //contains "Server=xxxxxx"
        //  //  conn_string = configuration.GetConnectionString("AircraftMaintenanceContext");
        //}




        public async Task send_email(SendEmailModel model)
        //  public void send_email(SendEmailModel model)
        {

            string json = JsonConvert.SerializeObject(model);

            HttpClient httpClient = new HttpClient();
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response =
                await httpClient.PostAsync("http://697962d4.ngrok.io/api/sendemail", content);
            string statusCode = response.StatusCode.ToString();


        }

        SqlConnection con = new SqlConnection(conn_string);

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("Select ID, AircraftRegNo, Type from Aircraft where FlyingHoursCount >= 200 AND Status = 'Available'", con);

                SqlDataReader rd = cmd.ExecuteReader();



                List<Aircraft> aircrafts = new List<Aircraft>();

                while (rd.Read())
                {
                    Aircraft x = new Aircraft();
                    x.ID = Int32.Parse(rd["ID"].ToString());
                    x.AircraftRegNo = rd["AircraftRegNo"].ToString();
                    x.Type = rd["Type"].ToString();
                    aircrafts.Add(x);
                }

                rd.Close();
                con.Close();


                for (int i = 0; i < aircrafts.Count; i++)
                {
                    List<Crew> crew_members = new List<Crew>();

                    con.Open();

                    SqlCommand cmd2 = new SqlCommand("Select CrewId, Email, Name from Crew where Type = 'Ground' AND Status = 'Available'", con);
                    SqlDataReader rd2 = cmd2.ExecuteReader();



                    while (rd2.Read())
                    {
                        Crew x = new Crew();
                        x.CrewId = Int32.Parse(rd2["CrewId"].ToString());
                        x.Email = rd2["Email"].ToString();
                        x.Name = rd2["Name"].ToString();

                        crew_members.Add(x);
                    }

                    rd2.Close();
                    con.Close();

                    List<Crew> suitable_crew_members = new List<Crew>();
                    if (crew_members.Count >= 10)
                    {


                        con.Open();
                        StringBuilder querybuilder = new StringBuilder();
                        querybuilder.Append("Select distinct am.Ground_Crew_Id, c.Email,c.Name from AircraftMaintenance am, Crew c where am.Ground_Crew_Id=c.CrewId  AND am.Ground_Crew_Id in (");
                        for (int m = 0; m < crew_members.Count; m++)
                        {
                            if (m == 0)
                            { querybuilder.Append(crew_members.ElementAt(m).CrewId); }

                            else
                            {
                                querybuilder.Append("," + crew_members.ElementAt(m).CrewId);
                            }

                        }

                        querybuilder.Append(") AND (Select MAX(AircraftId) from AircraftMaintenance where Ground_Crew_Id = am.Ground_Crew_Id)!=" + aircrafts.ElementAt(i).ID);




                        SqlCommand cmd6 = new SqlCommand(querybuilder.ToString(), con);
                        SqlDataReader rd6 = cmd2.ExecuteReader();

                        while (rd6.Read())
                        {
                            Crew x = new Crew();
                            x.CrewId = Int32.Parse(rd6["Ground_Crew_Id"].ToString());
                            x.Name = rd6["Email"].ToString();
                            x.Email = rd6["Name"].ToString();




                            suitable_crew_members.Add(x);
                        }

                        rd6.Close();
                        con.Close();

                        /////






                        //////////////////

                        List<Crew> selected_members;
                        if (suitable_crew_members.Count < 5)
                        {
                            selected_members =
                                    crew_members.Take(5).ToList();
                        }
                        else
                        {
                            selected_members =
                             suitable_crew_members.Take(5).ToList();
                        }

                        con.Open();

                        SqlCommand cmd3 = new SqlCommand("UPDATE Crew SET Status = 'Occupied' Where Crew_Id IN (" + selected_members.ElementAt(0).CrewId + "," + selected_members.ElementAt(1).CrewId + "," + selected_members.ElementAt(2).CrewId + "," + selected_members.ElementAt(3).CrewId + "," + selected_members.ElementAt(4).CrewId + ")", con);

                        if (cmd3.ExecuteNonQuery() > 0)
                        {
                            for (int l = 0; l < 5; l++)

                            {
                                SendEmailModel model = new SendEmailModel();

                                model.air_craft_num = aircrafts.ElementAt(i).AircraftRegNo;
                                model.air_craft_type = aircrafts.ElementAt(i).Type;
                                model.emp_email = selected_members.ElementAt(l).Email;
                                model.emp_name = selected_members.ElementAt(l).Name;
                                model.date = DateTime.Now.ToString();
                                send_email(model).Wait();
                            }
                        }
                        else
                            return;

                        con.Close();

                        /////




                        for (int j = 0; j < 5; j++)
                        {
                            con.Open();
                            string insert_query = "INSERT INTO AircraftMaintenance(AircraftId, Ground_Crew_Id, Date) Values (" + aircrafts.ElementAt(i).ID + "," + selected_members.ElementAt(j).CrewId + "," + "GETDATE() )";

                            SqlCommand cmd4 = new SqlCommand(insert_query, con);

                            if (cmd4.ExecuteNonQuery() > 0)
                            {


                            }
                            else
                                return;

                            con.Close();
                        }
                        /////






                    }

                    else if (crew_members.Count <= 4)
                    {
                        List<Crew> selected_members = crew_members.ToList();


                        for (int k = 0; k < selected_members.Count; k++)
                        {
                            SqlCommand cmd3 = new SqlCommand("UPDATE Crew SET Status = 'Occupied' Where CrewId = " + selected_members.ElementAt(k).CrewId, con);
                            con.Open();
                            if (cmd3.ExecuteNonQuery() > 0)
                            {

                                SendEmailModel model = new SendEmailModel();

                                model.air_craft_num = aircrafts.ElementAt(i).AircraftRegNo;
                                model.air_craft_type = aircrafts.ElementAt(i).Type;
                                model.emp_email = selected_members.ElementAt(k).Email;
                                model.emp_name = selected_members.ElementAt(k).Name;
                                model.date = DateTime.Now.ToString();


                                send_email(model).Wait();

                            }
                            else
                                return;

                            con.Close();
                        }

                        for (int j = 0; j < selected_members.Count; j++)
                        {
                            con.Open();
                            string insert_query = "INSERT INTO AircraftMaintenance(AircraftId, Ground_Crew_Id, Date) Values(" + aircrafts.ElementAt(i).ID + ", " + selected_members.ElementAt(j).CrewId + ", " + "GETDATE())";

                            SqlCommand cmd4 = new SqlCommand(insert_query, con);

                            if (cmd4.ExecuteNonQuery() > 0)
                            { }
                            else
                                return;

                            con.Close();
                        }


                    }

                    else
                    {
                        List<Crew> selected_members = crew_members.Take(5).ToList();

                        con.Open();

                        SqlCommand cmd3 = new SqlCommand("UPDATE Crew SET Status = 'Occupied' Where CrewId IN (" + selected_members.ElementAt(0).CrewId + "," + selected_members.ElementAt(1).CrewId + "," + selected_members.ElementAt(2).CrewId + "," + selected_members.ElementAt(3).CrewId + "," + selected_members.ElementAt(4).CrewId + ")", con);

                        if (cmd3.ExecuteNonQuery() > 0)
                        {
                            for (int l = 0; l < 5; l++)

                            {
                                SendEmailModel model = new SendEmailModel();

                                model.air_craft_num = aircrafts.ElementAt(i).AircraftRegNo;
                                model.air_craft_type = aircrafts.ElementAt(i).Type;
                                model.emp_email = selected_members.ElementAt(l).Email;
                                model.emp_name = selected_members.ElementAt(l).Name;
                                model.date = DateTime.Now.ToString();
                                send_email(model).Wait();
                            }


                        }
                        else
                            return;

                        con.Close();

                        /////




                        for (int j = 0; j < 5; j++)
                        {
                            con.Open();
                            string insert_query = "INSERT INTO AircraftMaintenance(AircraftId, Ground_Crew_Id, Date) Values (" + aircrafts.ElementAt(i).ID + "," + selected_members.ElementAt(j).CrewId + "," + "GETDATE() )";

                            SqlCommand cmd4 = new SqlCommand(insert_query, con);

                            if (cmd4.ExecuteNonQuery() > 0)
                            { }
                            else
                                return;

                            con.Close();
                        }
                        /////






                    }

                    /////////////
                    con.Open();

                    SqlCommand cmd5 = new SqlCommand("UPDATE Aircraft SET Status = 'Occupied', FlyingHoursCount = 0 Where ID=" + aircrafts.ElementAt(i).ID + ";", con);

                    if (cmd5.ExecuteNonQuery() > 0)
                    { }
                    else
                        return;

                    con.Close();






                }
                //response.status = true;
                //response.message = "Token List";
                //response.tokens = items;

            }
            catch (Exception ex)
            {


                //response.status = false;
                //response.message = ex.Message;
            }





        }

    }
}
