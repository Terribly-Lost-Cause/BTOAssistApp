using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BTOAssistApp.Models;
using Npgsql;

namespace BTOAssistApp.Data
{
    class PostGre
    {
       
       
        public void CheckDataAsync(PhoneInfo phoneinfo)
        {
            

            string defaultEnvVar = nameof(defaultEnvVar);
            string processEnvVar = nameof(processEnvVar);
            string userEnvVar = nameof(userEnvVar);
            string machineEnvVar = nameof(machineEnvVar);
            string[] envVars = { defaultEnvVar, processEnvVar, userEnvVar, machineEnvVar };

            // Try to get the environment variables from each target.
            // The default (no specified target).
            Console.WriteLine("Retrieving environment variables from the default target:");
            foreach (var envVar in envVars)
            {
                var value = Environment.GetEnvironmentVariable(envVar) ?? "(none)";
                Console.WriteLine($"   {envVar}: {value}");
            }
            /*
            
            //System.Environment.SetEnvironmentVariable(cred, "Host=ec2-34-231-63-30.compute-1.amazonaws.com;Username=lpteuvuyvbcmqq;Password=fa09dd6a4daded9cc86272f4720db64315845753904cf049d5fdebbc8d7fa8ea;Database=d1mup2m4442d7t");
            Environment.SetEnvironmentVariable("Test", "Host=ec2-34-231-63-30.compute-1.amazonaws.com;Username=lpteuvuyvbcmqq;Password=fa09dd6a4daded9cc86272f4720db64315845753904cf049d5fdebbc8d7fa8ea;Database=d1mup2m4442d7t", EnvironmentVariableTarget.Machine);
            string test = Environment.GetEnvironmentVariable("Test");
            Console.WriteLine(">>>>>>>>>>>>>" + Environment.GetEnvironmentVariable(var) + "<<<<<<<<<<<<<<<<<");
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                // Change the directory to %WINDIR%
                Environment.CurrentDirectory = Environment.GetEnvironmentVariable("windir");
                DirectoryInfo info = new DirectoryInfo(".");

                Console.WriteLine("Directory Info:   " + info.FullName);
            }
            else
            {
                Console.WriteLine("This example runs on Windows only.");
            }
*/
            //string cred = Environment.GetEnvironmentVariable("DATABASE_VAR");
            /*string var = "DB_VAL";
            string val= "Host=ec2-34-231-63-30.compute-1.amazonaws.com;Username=lpteuvuyvbcmqq;Password=fa09dd6a4daded9cc86272f4720db64315845753904cf049d5fdebbc8d7fa8ea;Database=d1mup2m4442d7t";
            Environment.SetEnvironmentVariable(var, val, EnvironmentVariableTarget.Machine);
            Console.WriteLine(">>>>>>>>",Environment.GetEnvironmentVariable(var));
            foreach (System.Collections.DictionaryEntry env in Environment.GetEnvironmentVariables())
            {
                string name = (string)env.Key;
                string value = (string)env.Value;
                Console.WriteLine(">>>>>>>>>>>>>>>> {0}={1}", name, value);
            }*/
            string text;
            text = "echo hello";
            System.Diagnostics.Process.Start("CMD.exe", text);
            string strCmdText;

            string rootpath = "C:"+"/"+"Users"+"/"+"ASUS"+"/"+"Desktop";
            Console.WriteLine(rootpath);
            foreach (System.Collections.DictionaryEntry env in Environment.GetEnvironmentVariables())
            {
                string name = (string)env.Key;
                string value = (string)env.Value;
                Console.WriteLine("{0}={1}", name, value);
            }

            string cmdPath = System.IO.Path.Combine(Environment.SystemDirectory, "cmd.exe"); 
            

            Console.WriteLine(">>>>>>>>>>>>>>>>>>>", Environment.SystemDirectory.ToString());
            System.Diagnostics.Process.Start(cmdPath, "/c");
            var cs = Environment.GetEnvironmentVariable("BOOTCLASSPATH");
            //Console.WriteLine(">>>>>>>>>>>>>"+cs+"<<<<<<<<<<<<<<<<<");
            var con = new NpgsqlConnection(cs);
            con.Open();
            //var get = Database.Table<PhoneInfo>().Where(i => item.deviceID == item.deviceID).FirstOrDefaultAsync();
            string sql = "SELECT COUNT(DeviceId) FROM PhoneInfo WHERE DeviceId = @id;";
            var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("id", phoneinfo.deviceID);
            int rdr = Convert.ToInt32(cmd.ExecuteScalar());
            Console.WriteLine("<><><><><><>");
            //
            
            if (rdr == 0)
            {
                sql = "INSERT INTO PhoneInfo (deviceid, accesstoken, sub)VALUES(@id, @accesstoken, @sub);";
                var cmd2 = new NpgsqlCommand(sql, con);
                cmd2.Parameters.AddWithValue("id", phoneinfo.deviceID);
                cmd2.Parameters.AddWithValue("accesstoken", phoneinfo.accessToken);
                cmd2.Parameters.AddWithValue("sub", phoneinfo.sub);
                cmd2.ExecuteNonQuery();

                Console.WriteLine("0");
            }
            else
            {
                //update
                sql = "UPDATE PhoneInfo SET deviceid = @id, accesstoken = @accesstoken, sub = @sub WHERE deviceid = @id;";
                var cmd2 = new NpgsqlCommand(sql, con);
                cmd2.Parameters.AddWithValue("id", phoneinfo.deviceID);
                cmd2.Parameters.AddWithValue("accesstoken", phoneinfo.accessToken);
                cmd2.Parameters.AddWithValue("sub", phoneinfo.sub);
                cmd2.ExecuteNonQuery();
                Console.WriteLine("not 0");
            }
            


        } /*

        public Task<PhoneInfo> GetBTODataAsync(string id)
        {
            return Database.Table<PhoneInfo>().Where(i => i.deviceID == id).FirstOrDefaultAsync();
        }

        public Task<List<PhoneInfo>> GetAllPhoneInfoAsync()
        {
            return Database.Table<PhoneInfo>().ToListAsync();
        }

        public Task<int> DeleteAllPhoneInfoAsync()
        {
            return Database.DeleteAllAsync<PhoneInfo>();
        }
         */

        public BTO GetBTODetailsAsync(string id)
        {
            HttpClient client;
            var Btoentry = new BTO();
            client = new HttpClient();
            Uri getToken = new Uri("https://uwuwuwuwuuwuwuwuwuuwuwuwuwuuwu.herokuapp.com/getBTOData");
            //getBTOInfo
            var cs = "";
            var con = new NpgsqlConnection(cs);
            con.Open();
            string sql = "SELECT * FROM BTO WHERE ID = @id;";
            var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Prepare();

            NpgsqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Btoentry.ID = rdr.GetString(0);
                Btoentry.Image = rdr.GetString(1);
                Btoentry.Location = rdr.GetString(2);
                Btoentry.Block = rdr.GetString(3);
                Btoentry.YearsLeft = rdr.GetInt32(4);
                Btoentry.MinLeaseLeft = rdr.GetInt32(5);
                Btoentry.MaxLeaseLeft = rdr.GetInt32(6);
                Btoentry.StartDate = rdr.GetDateTime(7);
                Btoentry.EndDate = rdr.GetDateTime(8);
                Btoentry.Applicants = rdr.GetInt32(9);
                Btoentry.RoomType = rdr.GetInt32(10);
                Btoentry.NumberofUnits = rdr.GetInt32(11);
                Btoentry.SQM = rdr.GetInt32(12);
                Btoentry.Bus = rdr.GetString(13);
                Btoentry.MRT = rdr.GetString(14);
                Btoentry.Direction = rdr.GetString(15);
                Btoentry.LongDescription = rdr.GetString(16);
                Btoentry.DownPayment = rdr.GetDouble(17);
                Btoentry.FullPayment = rdr.GetDouble(18);
            }
            con.Close();

            return Btoentry;
        }

        public List<BTO> GetBTOPopularityAsync()
        {
            List<BTO> allbtos = new List<BTO>();

            var cs = "Host=ec2-34-231-63-30.compute-1.amazonaws.com;Username=lpteuvuyvbcmqq;Password=fa09dd6a4daded9cc86272f4720db64315845753904cf049d5fdebbc8d7fa8ea;Database=d1mup2m4442d7t";

            var con = new NpgsqlConnection(cs);
            con.Open();
            string sql = "SELECT * FROM BTO ORDER BY APPLICANTS DESC;";
            var cmd = new NpgsqlCommand(sql, con);

            NpgsqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                var Btoentry = new BTO();
                Btoentry.ID = rdr.GetString(0);
                Btoentry.Image = rdr.GetString(1);
                Btoentry.Location = rdr.GetString(2);
                Btoentry.Block = rdr.GetString(3);
                Btoentry.YearsLeft = rdr.GetInt32(4);
                Btoentry.MinLeaseLeft = rdr.GetInt32(5);
                Btoentry.MaxLeaseLeft = rdr.GetInt32(6);
                Btoentry.StartDate = rdr.GetDateTime(7);
                Btoentry.EndDate = rdr.GetDateTime(8);
                Btoentry.Applicants = rdr.GetInt32(9);
                Btoentry.RoomType = rdr.GetInt32(10);
                Btoentry.NumberofUnits = rdr.GetInt32(11);
                Btoentry.SQM = rdr.GetInt32(12);
                Btoentry.Bus = rdr.GetString(13);
                Btoentry.MRT = rdr.GetString(14);
                Btoentry.Direction = rdr.GetString(15);
                Btoentry.LongDescription = rdr.GetString(16);
                Btoentry.DownPayment = rdr.GetDouble(17);
                Btoentry.FullPayment = rdr.GetDouble(18);

                allbtos.Add(Btoentry);
            }
            con.Close();

            return allbtos;
        }

        public List<BTO> GetAllBTOAsync()
        {
            List<BTO> allbtos = new List<BTO>();

            var cs = "Host=ec2-34-231-63-30.compute-1.amazonaws.com;Username=lpteuvuyvbcmqq;Password=fa09dd6a4daded9cc86272f4720db64315845753904cf049d5fdebbc8d7fa8ea;Database=d1mup2m4442d7t";

            var con = new NpgsqlConnection(cs);
            con.Open();

            string sql = "SELECT * FROM BTO;";
            var cmd = new NpgsqlCommand(sql, con);

            NpgsqlDataReader rdr = cmd.ExecuteReader();
            
            while (rdr.Read())
            {
                var Btoentry = new BTO();
                Btoentry.ID = rdr.GetString(0);
                Btoentry.Image = rdr.GetString(1);
                Btoentry.Location = rdr.GetString(2);
                Btoentry.Block = rdr.GetString(3);
                Btoentry.YearsLeft = rdr.GetInt32(4);
                Btoentry.MinLeaseLeft = rdr.GetInt32(5);
                Btoentry.MaxLeaseLeft = rdr.GetInt32(6);
                Btoentry.StartDate = rdr.GetDateTime(7);
                Btoentry.EndDate = rdr.GetDateTime(8);
                Btoentry.Applicants = rdr.GetInt32(9);
                Btoentry.RoomType = rdr.GetInt32(10);
                Btoentry.NumberofUnits = rdr.GetInt32(11);
                Btoentry.SQM = rdr.GetInt32(12);
                Btoentry.Bus = rdr.GetString(13);
                Btoentry.MRT = rdr.GetString(14);
                Btoentry.Direction = rdr.GetString(15);
                Btoentry.LongDescription = rdr.GetString(16);
                Btoentry.DownPayment = rdr.GetDouble(17);
                Btoentry.FullPayment = rdr.GetDouble(18);

                allbtos.Add(Btoentry);
            }
            con.Close();

            return allbtos;
        }
    }

}
