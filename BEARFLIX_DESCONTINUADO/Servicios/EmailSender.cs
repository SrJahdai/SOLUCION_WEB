using Microsoft.AspNetCore.Identity.UI.Services;

namespace BEARFLIX.Servicios
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Simulación de envío de correo en la consola
            Console.WriteLine($"Enviando email a: {email}");
            Console.WriteLine($"Asunto: {subject}");
            Console.WriteLine($"Mensaje: {htmlMessage}");
            return Task.CompletedTask;
        }
    }
}
