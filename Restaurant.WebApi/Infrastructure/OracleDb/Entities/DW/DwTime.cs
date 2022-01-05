using System;

namespace Restaurant.WebApi.Infrastructure.OracleDb.Entities.DW
{
    public class DwTime
    {
        public int Id { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }
        public string Year_Name { get; set; }
        public DateTime FullDate { get; set; }
        public string Day_Name { get; set; }
        public int Semester { get; set; }
        public int Quarter { get; set; }
        public int DayOfWeek { get; set; }
        public string DayOfWeek_name { get; set; }
        public bool IsWeekend { get; set; }
        public int DayNumberInMonth { get; set; }
    }
}
