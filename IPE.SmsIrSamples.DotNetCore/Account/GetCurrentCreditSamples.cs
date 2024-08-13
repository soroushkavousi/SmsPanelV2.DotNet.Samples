using IPE.SmsIrClient;
using IPE.SmsIrClient.Exceptions;
using IPE.SmsIrClient.Models.Results;
using System;
using System.Threading.Tasks;

namespace IPE.SmsIrSamples.DotNetCore.Report.Account;

public static class GetCurrentCreditSamples
{
    /// <summary>
    /// دریافت مقدار اعتبار فعلی
    /// برای مشاهده مقدار اعتبار فعلی از متد زیر استفاده نمایید.
    /// https://app.sms.ir/developer/help/credit
    /// </summary>
    public static async Task GetCurrentCreditAsync()
    {
        try
        {
            // کلید ای‌پی‌آی ساخته‌شده در سامانه
            SmsIr smsIr = new("uw7ppC4vGibwGFgAwLyRexHjyEb82yFFEXbbwOoOVT9GVMAQXoDO1vTkx59cOgoJ");

            // انجام دریافت اعتبار
            SmsIrResult<decimal> response = await smsIr.GetCreditAsync();

            // دریافت اعتبار شما در اینجا با موفقیت انجام شده‌است.

            // گرفتن بخش دیتا خروجی
            decimal credit = response.Data;

            // توضیحات خروجی
            string description = $"Credit: {credit}";

            await Console.Out.WriteLineAsync(description);
        }
        catch (SmsIrException ex) // درخواست ناموفق
        {
            // جدول توضیحات کد وضعیت
            // https://app.sms.ir/developer/help/statusCode

            string errorDescription = ex switch
            {
                LogicalException => $"Logical Error (Status: {ex.Status}): {ex.Message}. Please check your request parameters.",
                UnauthorizedException => $"Unauthorized (Status: {ex.Status}): {ex.Message}. Please check your API key.",
                AccessDeniedException => $"Access Denied (Status: {ex.Status}): {ex.Message}. Please check your plan.",
                TooManyRequestException => $"Too Many Requests (Status: {ex.Status}): {ex.Message}. Please try again later.",
                UnexpectedException => $"Unexpected Error (Status: {ex.Status}): {ex.Message}. An unexpected error occured.",
                _ => $"SmsIr Error (Status: {ex.Status}): {ex.Message}"
            };

            await Console.Out.WriteLineAsync(errorDescription);
        }
        catch (Exception ex)
        {
            string errorMessage = $"{ex.GetType().Name}: {ex.Message}";
            await Console.Out.WriteLineAsync(errorMessage);
        }
    }

    public static async Task GetCurrentCredit2Async()
    {
        try
        {
            // کلید ای‌پی‌آی ساخته‌شده در سامانه
            SmsIr smsIr = new("uw7ppC4vGibwGFgAwLyRexHjyEb82yFFEXbbwOoOVT9GVMAQXoDO1vTkx59cOgoJ");

            // انجام دریافت اعتبار
            SmsIrResult<decimal> response = await smsIr.GetCreditAsync();

            // دریافت اعتبار شما در اینجا با موفقیت انجام شده‌است.

            // گرفتن بخش دیتا خروجی
            decimal credit = response.Data;

            // توضیحات خروجی
            string description = $"Credit: {credit}";

            await Console.Out.WriteLineAsync(description);
        }
        catch (Exception ex) // درخواست ناموفق
        {
            // جدول توضیحات کد وضعیت
            // https://app.sms.ir/developer/help/statusCode

            string errorDescription = ex switch
            {
                SmsIrException smsIrException =>
                    smsIrException switch
                    {
                        LogicalException => $"Logical Error (Status: {smsIrException.Status})" +
                            $": {smsIrException.Message}. Please check your request parameters.",

                        UnauthorizedException => $"Unauthorized (Status: {smsIrException.Status})" +
                            $": {smsIrException.Message}. Please check your API key.",

                        AccessDeniedException => $"Access Denied (Status: {smsIrException.Status})" +
                            $": {smsIrException.Message}. Please check your plan.",

                        TooManyRequestException => $"Too Many Requests (Status: {smsIrException.Status})" +
                            $": {smsIrException.Message}. Please try again later.",

                        UnexpectedException => $"Unexpected Error (Status: {smsIrException.Status})" +
                            $": {smsIrException.Message}. An unexpected error occured.",

                        _ => $"SmsIr Error (Status: {smsIrException.Status}): {smsIrException.Message}"
                    },
                _ => $"{ex.GetType().Name}: {ex.Message}"
            };

            await Console.Out.WriteLineAsync(errorDescription);
        }
    }
}