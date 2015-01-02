﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vooban.FreshBooks.Staff.Models;
using Vooban.FreshBooks.TimeEntry.Models;

namespace Vooban.FreshBooks.Reports.Timesheet
{
    [DebuggerDisplay("Timesheet for {Employee.FirstName} {Employee.Lastname}")]
    public class RangedTimeSheetDetail
    {
        public StaffModel Employee { get; internal set; }

        [DebuggerDisplay("All time entries for a total of {TotalTime} hours")]
        public IEnumerable<TimeEntryModel> AllTimeEntries { get; internal set; }

        [DebuggerDisplay("All the unworked but paid time for a total of {TotalIneligibleToOvertimeTime} hours")]
        public IEnumerable<TimeEntryModel> PaidTimeOffTimeEntries { get; internal set; }

        [DebuggerDisplay("All the unworked and unpaid time for a total of {TotalIneligibleToOvertimeTime} hours")]
        public IEnumerable<TimeEntryModel> UnpaidTimeOffTimeEntries { get; internal set; }

        [DebuggerDisplay("Payable labour time entries for a total of {TotalPayableLabourTime} hours")]
        public IEnumerable<TimeEntryModel> PayableTimeEntries { get; internal set; }

        [DebuggerDisplay("Sickness time entries for a total of {TotalSicknessTime} hours")]
        public IEnumerable<TimeEntryModel> SicknessTimeEntries { get; internal set; }

        [DebuggerDisplay("Vacations time entries for a total of {TotalVacationsTime} hours")]
        public IEnumerable<TimeEntryModel> VacationsTimeEntries { get; internal set; }

        [DebuggerDisplay("Holliday time entries for a total of {TotalHollidayTime} hours")]
        public IEnumerable<TimeEntryModel> HollidayTimeEntries { get; internal set; }

        [DebuggerDisplay("Banked absence time entries for a total of {TotalBankedTime} hours")]
        public IEnumerable<TimeEntryModel> BankedTimeEntries { get; internal set; }

        [DebuggerDisplay("Banked absence time entries for a total of {TotalBankedTime} hours")]
        public IEnumerable<TimeEntryModel> TrainingTimeEntries { get; internal set; }

        public double TotalTime
        {
            get
            {                
                return AllTimeEntries.Where(s => s.Hours.HasValue).Sum(s => s.Hours.Value);
            }
        }

        public double TotalPaidTimeOffTime
        {
            get
            {
                return PaidTimeOffTimeEntries.Where(s => s.Hours.HasValue).Sum(s => s.Hours.Value);
            }
        }

        public double TotalUnpaidTimeOffTime
        {
            get
            {
                return UnpaidTimeOffTimeEntries.Where(s => s.Hours.HasValue).Sum(s => s.Hours.Value);
            }
        }

        public double TotalPayableTime
        {
            get
            {
                return PayableTimeEntries.Where(s => s.Hours.HasValue).Sum(s => s.Hours.Value);
            }
        }

        public double TotalSicknessTime
        {
            get
            {
                return SicknessTimeEntries.Where(s => s.Hours.HasValue).Sum(s => s.Hours.Value);
            }
        }

        public double TotalVacationsTime
        {
            get
            {
                return VacationsTimeEntries.Where(s => s.Hours.HasValue).Sum(s => s.Hours.Value);
            }
        }

        public double TotalHollidayTime
        {
            get
            {
                return HollidayTimeEntries.Where(s => s.Hours.HasValue).Sum(s => s.Hours.Value);
            }
        }

        public double TotalBankedTime
        {
            get
            {
                return BankedTimeEntries.Where(s => s.Hours.HasValue).Sum(s => s.Hours.Value);
            }
        }

        public double TotalTrainingTime
        {
            get
            {
                return TrainingTimeEntries.Where(s => s.Hours.HasValue).Sum(s => s.Hours.Value);
            }
        }
    }

}