﻿using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Results;
using IPE.SmsIrSamples.DotNetCore.Report.Models;
using System;
using System.Threading.Tasks;

namespace IPE.SmsIrSamples.DotNetCore.Report.Send;

public static class GetSendPackSamples
{
    /// <summary>
    /// گزارش مجموعه ارسال
    /// شما می‌توانید با استفاده از شناسه مجموعه ارسال، گزارشی از پیامک‌های ارسالی در آن درخواست به‌ همراه وضعیت‌هایشان را دریافت نمایید.
    /// https://app.sms.ir/developer/help/sendPack
    /// </summary>
    public static async Task GetSendPackLiveReportAsync()
    {
        try
        {
            // کلید ای‌پی‌آی ساخته‌شده در سامانه
            SmsIr smsIr = new SmsIr("uw7ppC4vGibwGFgAwLyRexHjyEb82yFFEXbbwOoOVT9GVMAQXoDO1vTkx59cOgoJ");

            // شناسه مجموعه ارسال
            Guid packId = new Guid("86D96B0E-FD89-4C19-B303-C0B4D3874063");

            // انجام دریافت گزارش
            var response = await smsIr.GetReportAsync(packId);

            // دریافت گزارش شما در اینجا با موفقیت انجام شده‌است.

            // گرفتن بخش دیتا خروجی
            MessageReportResult[] messages = response.Data;

            // بررسی و نمایش پیامک‌های گزارش
            string reportDescription = $"Pack Id: {packId}" + Environment.NewLine + Environment.NewLine;
            foreach (var message in messages)
            {
                // شناسه پیامک
                int messageId = message.MessageId;

                // خط استفاده شده برای ارسال
                long lineNumber = message.LineNumber;

                // شماره موبایل مقصد
                long mobile = message.Mobile;

                // پیام فرستاده شده
                string messageText = message.MessageText;

                // زمان ارسال
                int sendDateTimeInUnix = message.SendDateTime;

                // وضعیت دریافت
                DeliveryState delivertState = message.DeliveryState.HasValue ? (DeliveryState)message.DeliveryState : DeliveryState.Unknown;
                string deliveryStateDescription = delivertState.ToString();

                // زمان دریافت
                int? deliveryUnixTime = message.DeliveryDateTime;
                DateTime? deliveryDateTime = deliveryUnixTime.HasValue ?
                    DateTimeOffset.FromUnixTimeSeconds(deliveryUnixTime.Value).DateTime.ToLocalTime() : null;

                // هزینه ارسال این پیامک
                decimal cost = message.Cost;

                // خلاصه‌ای از هر پیامک
                string messageDescription = $"Mobile: {mobile}" +
                    $" - Delivery State: {deliveryStateDescription}" +
                    $" - Delivery DateTime: {deliveryDateTime?.ToString("dddd, dd MMMM yyyy HH:mm:ss") ?? "-"}" +
                    Environment.NewLine;

                reportDescription += messageDescription;
            }

            await Console.Out.WriteLineAsync(reportDescription);
        }
        catch (Exception ex) // درخواست ناموفق
        {
            // جدول توضیحات کد وضعیت
            // https://app.sms.ir/developer/help/statusCode

            string errorName = ex.GetType().Name;
            string errorNameDescription = errorName switch
            {
                "UnauthorizedException" => "Token is not valid or access denied",
                "LogicalException" => "Please fix the request parameters",
                "TooManyRequestException" => "Request count has exceed the limitation",
                "UnexpectedException" or "InvalidOperationException" => "Remote server error",
                _ => "Can not send the request",
            };

            var errorDescription = "There is a problem with the request." +
                $"\n - Error: {errorName} - {errorNameDescription} - {ex.Message}";

            await Console.Out.WriteLineAsync(errorDescription);
        }
    }
}