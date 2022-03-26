using SQLite;
using System;

namespace BTOAssistApp.Models
{
    public class BTO
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string Image { get; set; }
        public string Location { get; set; }
        public string Block { get; set; }
        public int YearsLeft { get; set; }
        public int MinLeaseLeft { get; set; }
        public int MaxLeaseLeft { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Applicants { get; set; }
        public int RoomType { get; set; }
        public int NumberofUnits { get; set; }
        public int SQM { get; set; }
        public string Bus { get; set; }
        public string MRT { get; set; }
        public string Direction { get; set; }
        public string LongDescription { get; set; }
        public double DownPayment { get; set; }
        public double FullPayment { get; set; }
    }
}
