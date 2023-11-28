﻿using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Application.Common.Utility;
using WhiteLagoon.Web.ViewModels;

namespace WhiteLagoon.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        static int previousMonth = DateTime.Now.Month == 1 ? 12 : DateTime.Now.Month - 1;
        readonly DateTime previousMonthStartDate = new(DateTime.Now.Year, previousMonth, 1);
        readonly DateTime currentMonthStartDate = new(DateTime.Now.Year, DateTime.Now.Month, 1);
        public DashboardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> GetTotalBookingRadialChartData()
        {
            var totalBookings = _unitOfWork.Booking.GetAll(i => i.Status != SD.StatusPending
            || i.Status == SD.StatusCancelled);

            var countByCurrentMonth = totalBookings.Count(i => i.BookingDate >= currentMonthStartDate
            && i.BookingDate <= DateTime.Now);

            var countByPreviousMonth = totalBookings.Count(i => i.BookingDate >= previousMonthStartDate
            && i.BookingDate <= currentMonthStartDate);

            RadialBarChartVM radialBarChartVM = new();

            int increaseDecreaseRatio = 100;

            if(countByCurrentMonth != 0 && countByPreviousMonth != 0)
            {
                increaseDecreaseRatio = Convert.ToInt32((countByCurrentMonth-countByPreviousMonth)/countByPreviousMonth * 100);
            }
            if(countByCurrentMonth != 0 && countByPreviousMonth == 0)
            {
                increaseDecreaseRatio = Convert.ToInt32((countByCurrentMonth - countByPreviousMonth) / 1 * 100);
            }

            radialBarChartVM.TotalCount = totalBookings.Count();
            radialBarChartVM.CountInCurrentMonth = countByCurrentMonth;
            radialBarChartVM.HasRatioIncreased = countByCurrentMonth > countByPreviousMonth;
            radialBarChartVM.Series = new int[] { increaseDecreaseRatio };

            return Json(radialBarChartVM);
        }
    }
}
