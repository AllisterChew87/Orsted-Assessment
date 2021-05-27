using AssessmentApi.Model;
using ExcelDataReader;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace AssessmentApi.Provider
{
    public interface IExcelReader
    {
        List<Employee> ReadFromExcel();
    }


    /// <summary>
    /// To retrieve from excel file
    /// Excluded from unit test due to static class from file and excel reader is not able to mock
    /// </summary>
    /// <return>
    /// List of employee
    /// </return>
    [ExcludeFromCodeCoverage]
    public class ExcelReader: IExcelReader
    {
        public ExcelReader()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public List<Employee> ReadFromExcel()
        {
            var employeeRecords = new List<Employee>();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "EmployeeRecords.xlsx");

            using (var stream = File.OpenRead(path))
            {
                using var reader = ExcelReaderFactory.CreateReader(stream);
                while (reader.Read())
                {
                    if (reader.GetString(0) is null && reader.GetString(1) is null && reader.GetString(2) is null && reader.GetString(3) is null)
                        continue;

                    employeeRecords.Add(new Employee()
                    {
                        EmployeeNumber = reader.GetString(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        EmployeeStatus = reader.GetString(3)
                    });
                }
            }

            return employeeRecords;
        }
    }
}
