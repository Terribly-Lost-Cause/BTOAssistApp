using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BTOAssistApp.Models;
using Npgsql;


namespace BTOAssistApp.Data
{
    class PostGre
    {
       
       
        public void CheckDataAsync(string id)
        {

            var cs = "Host=ec2-34-231-63-30.compute-1.amazonaws.com;Username=lpteuvuyvbcmqq;Password=fa09dd6a4daded9cc86272f4720db64315845753904cf049d5fdebbc8d7fa8ea;Database=d1mup2m4442d7t";

            var con = new NpgsqlConnection(cs);
            con.Open();
            //var get = Database.Table<PhoneInfo>().Where(i => item.deviceID == item.deviceID).FirstOrDefaultAsync();
            string sql = "SELECT COUNT(DeviceId) FROM PhoneInfo WHERE DeviceId = @id;";
            var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("id", id);
            int rdr = Convert.ToInt32(cmd.ExecuteScalar());
            Console.WriteLine("<><><><><><>");
            //
            
            if (rdr == 0)
            {
                Console.WriteLine("0");
            }
            else
            {
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
            var Btoentry = new BTO();

            var cs = "Host=ec2-34-231-63-30.compute-1.amazonaws.com;Username=lpteuvuyvbcmqq;Password=fa09dd6a4daded9cc86272f4720db64315845753904cf049d5fdebbc8d7fa8ea;Database=d1mup2m4442d7t";

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
