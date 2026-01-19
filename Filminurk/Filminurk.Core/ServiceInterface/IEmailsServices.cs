using Filminurk.Core.Dto;

namespace Filminurk.Core.ServiceInterface
{
    public interface IEmailsServices
    {
        void SendEmail(EmailDTO dto);
        void SendRegistrationEmail(EmailDTO dto, EmailTokenDTO token);
    }
}
