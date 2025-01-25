using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.Common;
using System.Linq;

namespace httpValidateCpf
{
    public static class azfuncvalidacpf
    {
        [FunctionName("azfuncvalidacpf")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("CPF verification stared...");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            if (data == null)
            {
                return new BadRequestObjectResult("Please pass a CPF value in the request body");
            }

            string cpf = data?.cpf;

            string responseMessage = ValidateCpf(cpf) ? "CPF is valid" : "CPF is not valid";

            return new OkObjectResult(responseMessage);
        }

        public static bool ValidateCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf)) return false;

            // Remove non-digit characters
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            // Check if CPF is 000.000.000-00
            if (cpf == "00000000000")
                return false;

            // Check if CPF has 11 digits
            if (cpf.Length != 11)
                return false;

            // Check if CPF has repeated digits
            if (cpf.Distinct().Count() == 1)
                return false;

            // Calculate the first check digit
            int sum1 = 0;
            for (int i = 0; i < 9; i++)
                sum1 += int.Parse(cpf[i].ToString()) * (10 - i);
            int check1 = 11 - (sum1 % 11);
            if (check1 > 9)
                check1 = 0;

            // Calculate the second check digit
            int sum2 = 0;
            for (int i = 0; i < 10; i++)
                sum2 += int.Parse(cpf[i].ToString()) * (11 - i);
            int check2 = 11 - (sum2 % 11);
            if (check2 > 9)
                check2 = 0;

            // Check if the calculated check digits match the provided ones
            return cpf[9] == check1.ToString()[0] && cpf[10] == check2.ToString()[0];
        }
    }
}
