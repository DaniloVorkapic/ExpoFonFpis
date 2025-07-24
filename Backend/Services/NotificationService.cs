using System.Net;
using System.Net.Mail;
using System.Text;
using Backend.Entities;
using Backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IGenericRepository<NotificationRecipient> _recipientRepository;
        private readonly IGenericRepository<Employee> _employeeRepository;

        public NotificationService(IGenericRepository<NotificationRecipient> recipientRepository, IGenericRepository<Employee> employeeRepository)
        {
            _recipientRepository = recipientRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task SendEmailToRecipients()
        {
            var content = new StringBuilder();
            var employees = await _employeeRepository.GetQueryable().Include(e => e.Children).ToListAsync();

            await BuildNotificationContent(employees, content);

            await SendEmailsToRecipients(content.ToString());
        }

        private async Task BuildNotificationContent(List<Employee> employees, StringBuilder content)
        {
            foreach (var employee in employees)
            {
                var employeeContent = new StringBuilder();
                AppendEmployeeDetails(employee, employeeContent);

                if (employee is FemaleEmployee femaleEmployee)
                {
                    await _employeeRepository.GetQueryable()
                        .Include(e => ((FemaleEmployee)e).Pregnancies)
                        .FirstOrDefaultAsync(e => e.Id == femaleEmployee.Id);

                    AppendFemaleEmployeeDetails(femaleEmployee, employeeContent);
                }

                if (HasRelevantInfo(employeeContent, employee))
                {
                    content.AppendLine(employeeContent.ToString());
                }
            }
        }

        private void AppendEmployeeDetails(Employee employee, StringBuilder employeeContent)
        {
            employeeContent.AppendLine($"Zaposleni {employee.FirstName} {employee.LastName}<br>");
            foreach (var child in employee.Children)
            {
                if (ChildHasBirthdayIn30DaysOrLess(child))
                {
                    var daysUntilChildBirthday = CalculateDaysUntil(new DateTime(DateTime.Now.Year, child.DateOfBirth.Month, child.DateOfBirth.Day));
                    employeeContent.AppendLine($"<li> dete {child.FirstName} {child.LastName} ima rođendan za {daysUntilChildBirthday} dana.</li>");
                }
            }
        }

        private void AppendFemaleEmployeeDetails(FemaleEmployee femaleEmployee, StringBuilder employeeContent)
        {
            var lastActivePregnancy = femaleEmployee.Pregnancies.LastOrDefault(p => p.IsActive);
            if (lastActivePregnancy == null)
            {
                return;
            }

            if (DateIsIn30DaysOrLess(lastActivePregnancy.DateOfChildbirth))
            {
                var daysUntilChildbirth = CalculateDaysUntil(lastActivePregnancy.DateOfChildbirth);
                employeeContent.AppendLine($"<li> zaposleni ima porođaj za {daysUntilChildbirth} dana.</li>");
            }

            if (DateIsIn30DaysOrLess(lastActivePregnancy.DateOfOpeningPregnancy))
            {
                var daysUntilOpeningPregnancy = CalculateDaysUntil(lastActivePregnancy.DateOfOpeningPregnancy);
                employeeContent.AppendLine($"<li> zaposleni otvara trudničko za {daysUntilOpeningPregnancy} dana.</li>");
            }

            if (DateIsIn30DaysOrLess(femaleEmployee.ReturnDate?.Date))
            {
                var daysUntilReturnDate = CalculateDaysUntil(femaleEmployee.ReturnDate?.Date);
                employeeContent.AppendLine($"<li> zaposleni se vraća na posao za {daysUntilReturnDate} dana.</li>");
            }
        }

        private bool HasRelevantInfo(StringBuilder employeeContent, Employee employee)
        {
            return employeeContent.Length > $"Zaposleni {employee.FirstName} {employee.LastName}<br>".Length +
                Environment.NewLine.Length;
        }

        private int CalculateDaysUntil(DateTime? date)
        {
            return date.HasValue ? (int)(date.Value.Date - DateTime.Today).TotalDays : int.MaxValue;
        }

        private bool DateIsIn30DaysOrLess(DateTime? date)
        {
            if (!date.HasValue)
            {
                return false;
            }
            var daysUntil = CalculateDaysUntil(date);
            return daysUntil is > 0 and <= 30;
        }

        private bool ChildHasBirthdayIn30DaysOrLess(Child child)
        {
            var birthdayThisYear = new DateTime(DateTime.Now.Year, child.DateOfBirth.Month, child.DateOfBirth.Day);
            return DateIsIn30DaysOrLess(birthdayThisYear);
        }

        private async Task SendEmailsToRecipients(string content)
        {
            var recipients = await _recipientRepository.GetAllAsync();
            foreach (var recipient in recipients)
            {
                SendEmailTo(recipient.Email,"Dnevni Izveštaj", content);
            }
        }

        private static void SendEmailTo(string recipientEmail, string subject, string content)
        {
            var client = new SmtpClient("mail.tubeiq.rs", 25)
            {
                EnableSsl = false,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("no-reply@tubeiq.rs", "NoAnswer!1")
            };

            var message = ConstructMessage(recipientEmail, subject, content);
            if (!string.IsNullOrEmpty(message.Body))
            {
                client.Send(message);
            }
        }

        private static MailMessage ConstructMessage(string recipientEmail, string subject, string content)
        {
            var message = new MailMessage
            {
                From = new MailAddress("no-reply@tubeiq.rs", "no-reply@tubeiq.rs"),
            };

            message.To.Add(recipientEmail);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = content;
            return message;
        }

    }
}