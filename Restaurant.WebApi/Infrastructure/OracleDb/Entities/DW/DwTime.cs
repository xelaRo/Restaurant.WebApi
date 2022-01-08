using System;

namespace Restaurant.WebApi.Infrastructure.OracleDb.Entities.DW
{
    public class DwTime
    {
        public int Id { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }
        public int MONTHNUMBER { get; set; }
        public string MONTH_NAME { get; set; }
        public DateTime FullDate { get; set; }
        public string Day_Name { get; set; }
        public int Semester { get; set; }
        public string Quarter { get; set; }
        public int DayOfWeek { get; set; }
        public string DAYOFWEEK { get; set; }
        public int IsWeekend { get; set; }
        public int DayNumberInMonth { get; set; }
        public int WeekNumber { get; set; }
    }
}
