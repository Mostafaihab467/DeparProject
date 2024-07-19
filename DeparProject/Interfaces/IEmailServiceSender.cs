using DeparProject.Events;

namespace DeparProject.Interfaces
{
    public interface IEmailServiceSender
    {
        public void  HandleDepartmentCreation(DemartMentEmailService @event);
        public void  HandleDepartmentDelete(DemartMentEmailService @event);
    }
}
